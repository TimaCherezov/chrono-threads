using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackGroundMusicController : MonoBehaviour
{
    private static BackGroundMusicController instance;
    private AudioSource audioSource;
    private string currentScene = "";
    [SerializeField] private AudioClip menu;
    [SerializeField] private AudioClip firstScene;
    [SerializeField] private AudioClip secondScene;
    [SerializeField] private AudioClip thirdScene;

    private Coroutine fadeCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource.loop = true;
        audioSource.playOnAwake = false;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == currentScene) return;

        currentScene = scene.name;

        switch (scene.name)
        {
            case "Menu":
                StartFadeMusic(menu, 0.5f);
                break;
            case "FirstScene":
                StartFadeMusic(firstScene, 0.2f);
                break;
            case "SecondScene":
                StartFadeMusic(secondScene, 0.4f);
                break;
            case "ThirdScene":
                StartFadeMusic(thirdScene, 0.6f);
                break;
            default:
                StartFadeMusic(null, 0f);
                break;
        }
    }

    private void StartFadeMusic(AudioClip newClip, float targetVolume)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeToNewClip(newClip, targetVolume));
    }

    private IEnumerator FadeToNewClip(AudioClip newClip, float targetVolume)
    {
        var fadeDuration = 1f;

        for (var t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0f;
        audioSource.Stop();

        audioSource.clip = newClip;
        if (newClip != null)
        {
            audioSource.Play();

            for (var t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
            {
                audioSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
                yield return null;
            }

            audioSource.volume = targetVolume;
        }
    }
}