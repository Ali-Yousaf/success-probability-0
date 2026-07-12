using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class SpinWheel : MonoBehaviour
{
    [Header("Wheel")]
    [SerializeField] private RectTransform wheel;

    [Header("Spin Settings")]
    [SerializeField] private float spinDuration = 4f;
    [SerializeField] private int minSpins = 5;
    [SerializeField] private int maxSpins = 10;

    [Header("Winning Range")]
    [SerializeField] private float winMinAngle = -1.7f;
    [SerializeField] private float winMaxAngle = 1.7f;

    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private bool isSpinning;
    private bool gameEnded;

    void Start()
    {
        ResetGame();
    }

    public void Spin()
    {
        if (isSpinning || gameEnded)
            return;

        isSpinning = true;

        int fullRotations = Random.Range(minSpins, maxSpins + 1);
        float randomAngle = Random.Range(0f, 360f);

        float targetRotation = -(fullRotations * 360f + randomAngle);

        wheel.DORotate(
                new Vector3(0, 0, targetRotation),
                spinDuration,
                RotateMode.FastBeyond360)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                CheckResult();

                // Reset wheel for next play
                wheel.localRotation = Quaternion.identity;

                isSpinning = false;
            });
    }

    private void CheckResult()
    {
        float angle = wheel.localEulerAngles.z;

        if (angle > 180f)
            angle -= 360f;

        Debug.Log("Final Angle: " + angle);

        if (angle >= winMinAngle && angle <= winMaxAngle)
            YouWin();
        else
            YouLose();
    }

    private void YouWin()
    {
        gameEnded = true;
        ShowGameOver("You Win!");
    }

    private void YouLose()
    {
        gameEnded = true;
        ShowGameOver("You Lose!");
    }

    private void ShowGameOver(string result)
    {
        gameOverText.text = result;

        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.zero;
        gameOverPanel.transform.DOScale(Vector3.one, 0.5f)
            .SetEase(Ease.OutBack);
    }

    public void RestartGame()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        isSpinning = false;
        gameEnded = false;

        wheel.localRotation = Quaternion.identity;

        gameOverPanel.SetActive(false);
        gameOverPanel.transform.localScale = Vector3.one;
    }
}