using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSrc;
    [SerializeField] AudioClip audioClip;

    [SerializeField] private float volume = 0.5f;
    [SerializeField] private float fadeInDuration = 2f;


    void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        audioSrc.clip = audioClip;
        audioSrc.volume = 0f;
        audioSrc.Play();

        float timer = 0f;
        while (timer < fadeInDuration)
        {
            timer += Time.deltaTime;
            audioSrc.volume = Mathf.Lerp(0f, volume, timer / fadeInDuration);
            yield return null;
        }

        audioSrc.volume = volume;
    }
}
