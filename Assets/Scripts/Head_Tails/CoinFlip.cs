using DG.Tweening;
using TMPro;
using UnityEngine;

public class CoinSpinner : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI resultText;
    public float spinTimeMin = 2f;
    public float spinTimeMax = 3f;
    private bool spinning = false;

    public System.Action<char> OnCoinResult;

    public void FlipCoin()
    {
        if (spinning) return;

        spinning = true;
        resultText.text = "";
        resultText.gameObject.SetActive(false);

        float spinTime = Random.Range(spinTimeMin, spinTimeMax);

        // DOTween rotation (continuous spin)
        transform.DORotate(new Vector3(0, 360f, 0), 0.5f, RotateMode.FastBeyond360)
                 .SetLoops(Mathf.CeilToInt(spinTime / 0.5f), LoopType.Restart)
                 .SetEase(Ease.Linear)
                 .OnComplete(() =>
                 {
                     // Decide result
                     char result = Random.value > 0.5f ? 'H' : 'T';
                     OnCoinResult?.Invoke(result);

                     // Reset rotation with a smooth tween
                     transform.DORotate(Vector3.zero, 0.3f).SetEase(Ease.OutBack);
                     spinning = false;
                 });
    }
}