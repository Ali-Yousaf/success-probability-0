using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberGuessManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI randomNumberText;  
    public TMP_InputField inputField;          
    public Button submitButton;               
    public Button restartButton;              
    public GameObject gameOverPanel;          
    public TextMeshProUGUI gameOverText;     

    private string targetNumber;              

    void Start()
    {
        GenerateRandomNumber();
        submitButton.onClick.AddListener(CheckGuess);
        restartButton.onClick.AddListener(RestartGame);
        gameOverPanel.SetActive(false);
    }

    void GenerateRandomNumber()
    {
        targetNumber = "";
        for (int i = 0; i < 7; i++)
        {
            targetNumber += Random.Range(0, 10).ToString();
        }

        randomNumberText.text = "Random Number: ? ? ? ? ? ? ?";
        print(targetNumber);
    }

    void CheckGuess()
    {
        string guess = inputField.text;

        if (guess.Length != 7)
        {
            inputField.transform.DOShakePosition(0.5f, 10f, 20);
            return;
        }

        randomNumberText.text = "Random Number: ";

        // Reveal digits one by one
        for (int i = 0; i < 7; i++)
        {
            int index = i;
            DOVirtual.DelayedCall(i * 0.2f, () =>
            {
                randomNumberText.text += targetNumber[index] + " ";
                randomNumberText.transform.DOScale(1.1f, 0.1f).OnComplete(() =>
                    randomNumberText.transform.DOScale(1f, 0.1f)
                );
            });
        }

        // Disable submit button
        submitButton.interactable = false;

        // Show GameOver panel after all digits revealed
        float totalRevealTime = 7 * 0.2f + 0.2f; // add small delay for last digit
        DOVirtual.DelayedCall(totalRevealTime, () =>
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.localScale = Vector3.zero;
            gameOverPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
            bool isWin = guess == targetNumber;
            gameOverText.text = (isWin ? "You Win!" : "You Lose!") + "\n";
        });
    }

    public void RestartGame()
    {
        inputField.text = "";
        submitButton.interactable = true;
        gameOverPanel.SetActive(false);
        gameOverPanel.transform.localScale = Vector3.one;
        GenerateRandomNumber();
    }
}