using DG.Tweening;
using UnityEngine;

public class HT_SceneStart : MonoBehaviour
{
    public GameObject startPanel;
    public float displayTime = 3f;

    void Start()
    {
        startPanel.SetActive(true);
        startPanel.transform.localScale = Vector3.zero;

        // Scale in panel
        startPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        // Hide after delay
        DOVirtual.DelayedCall(displayTime, () =>
        {
            startPanel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack)
                .OnComplete(() => startPanel.SetActive(false));
        });
    }
}