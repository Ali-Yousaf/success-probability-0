using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinGameManager : MonoBehaviour
{
    public CoinSpinner coin;
    public Image[] placeholders; // 10 placeholders
    public Sprite circleEmpty, circleGreen, circleRed;

    public TMP_Text resultText; 
    public GameObject gameOverPanel; 
    public TMP_Text gameOverText;

    private int currentIndex = 0;
    private bool gameEnded = false;

    void Start()
    {
        coin.OnCoinResult += HandleResult;
        ResetGame();
    }

    public void SpinButtonPressed()
    {
        if (!gameEnded)
        {
            resultText.gameObject.SetActive(false);
            coin.FlipCoin();
        }
    }

    void HandleResult(char result)
    {
        resultText.text = result.ToString();
        resultText.gameObject.SetActive(true);

        if (result == 'H')
        {
            placeholders[currentIndex].sprite = circleGreen;
            AnimatePlaceholder(placeholders[currentIndex]);
            currentIndex++;

            if (currentIndex >= placeholders.Length)
                GameWin();
        }
        else
        {
            placeholders[currentIndex].sprite = circleRed;
            AnimatePlaceholder(placeholders[currentIndex]);
            StartCoroutine(FailRoutine());
        }
    }

    IEnumerator FailRoutine()
    {
        yield return new WaitForSeconds(1f);
        GameLose();
    }

    void GameWin()
    {
        gameEnded = true;
        ShowGameOver("You Win!", currentIndex);
    }

    void GameLose()
    {
        gameEnded = true;
        ShowGameOver("You Lose!", currentIndex);
    }

    void ShowGameOver(string resultTextString, int headsCount)
    {
        float probability = Mathf.Pow(0.5f, headsCount) * 100f; // percentage
        string probText = $"Probability: {probability:F5}%";
        
        if(headsCount == 0)
            probText = $"Oops";

        gameOverText.text = $"{resultTextString}\n\nHeads: {headsCount}\n\n{probText}";

        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.zero;
        gameOverPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    void AnimatePlaceholder(Image img)
    {
        img.transform.localScale = Vector3.zero;
        img.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void RestartGame()
    {
        ResetGame();
    }

    void ResetGame()
    {
        currentIndex = 0;
        gameEnded = false;
        resultText.text = "";
        resultText.gameObject.SetActive(false);

        foreach (Image img in placeholders)
        {
            img.sprite = circleEmpty;
            img.transform.localScale = Vector3.one;
        }

        gameOverPanel.SetActive(false);
        gameOverPanel.transform.localScale = Vector3.one;
    }
}