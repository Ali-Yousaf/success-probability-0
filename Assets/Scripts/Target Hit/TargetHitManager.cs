using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TinyTargetGame : MonoBehaviour
{
    [Header("UI References")]
    public Button targetButton;             // The target button
    public TextMeshProUGUI gameOverText;    // Win text
    public GameObject gameOverPanel;        // GameOver panel
    public Button restartButton;            // Restart button

    [Header("Settings")]
    public float appearDuration = 0.4f;     // How long the target stays
    public float intervalBetweenTargets = 1f; // Time before it reappears
    public Vector2 padding = new Vector2(50, 50); // Padding from screen edges

    private bool gameEnded = false;

    void Start()
    {
        targetButton.gameObject.SetActive(false);
        gameOverPanel.SetActive(false);

        targetButton.onClick.AddListener(TargetClicked);
        restartButton.onClick.AddListener(RestartGame);

        StartCoroutine(TargetLoop());
    }

    IEnumerator TargetLoop()
    {
        while (!gameEnded)
        {
            // Set random position
            Vector2 randomPos = new Vector2(
                Random.Range(padding.x, Screen.width - padding.x),
                Random.Range(padding.y, Screen.height - padding.y)
            );

            targetButton.transform.position = randomPos;
            targetButton.gameObject.SetActive(true);
            targetButton.transform.localScale = Vector3.zero;

            // Animate target popping in
            targetButton.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack);

            // Wait for appearDuration
            yield return new WaitForSeconds(appearDuration);

            if (!gameEnded)
            {
                // Animate target disappearing
                targetButton.transform.DOScale(0f, 0.15f).SetEase(Ease.InBack)
                    .OnComplete(() => targetButton.gameObject.SetActive(false));
            }

            // Wait before next appearance
            yield return new WaitForSeconds(intervalBetweenTargets);
        }
    }

    void TargetClicked()
    {
        if (gameEnded) return;

        gameEnded = true;

        // Animate click
        targetButton.transform.DOScale(1.5f, 0.2f).SetEase(Ease.OutBack)
            .OnComplete(() => targetButton.gameObject.SetActive(false));

        // Show GameOver panel
        gameOverText.text = "You Win!";
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.zero;
        gameOverPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public void RestartGame()
    {
        gameEnded = false;
        gameOverPanel.SetActive(false);
        StartCoroutine(TargetLoop());
    }
}