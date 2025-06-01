// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections;
//
// public class BackGroundMusicController : MonoBehaviour
// {
//     public static BackGroundMusicController instance;
//     private AudioSource audioSource;
//     private string currentScene = "";
//     [SerializeField] private AudioClip menu;
//     [SerializeField] public AudioClip firstScene;
//     [SerializeField] private AudioClip secondScene;
//     [SerializeField] private AudioClip thirdScene;
//
//     private Coroutine fadeCoroutine;
//
//     private void Awake()
//     {
//         audioSource = GetComponent<AudioSource>();
//         if (instance != null)
//         {
//             Destroy(gameObject);
//             return;
//         }
//
//         instance = this;
//         DontDestroyOnLoad(gameObject);
//
//         audioSource.loop = true;
//         audioSource.playOnAwake = false;
//
//         SceneManager.sceneLoaded += OnSceneLoaded;
//     }
//     
//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         if (scene.name == currentScene) return;
//     
//         currentScene = scene.name;
//     
//         switch (scene.name)
//         {
//             case "Menu":
//                 StartFadeMusic(menu, 0.5f);
//                 break;
//             case "FirstScene":
//                 StartFadeMusic(firstScene, 0.2f);
//                 break;
//             case "SecondScene":
//                 StartFadeMusic(secondScene, 0.4f);
//                 break;
//             case "ThirdScene":
//                 StartFadeMusic(thirdScene, 1f);
//                 break;
//             default:
//                 StartFadeMusic(null, 0f);
//                 break;
//         }
//     }
//
//     public void StartFadeMusic(AudioClip newClip, float targetVolume)
//     {
//         if (fadeCoroutine != null)
//             StopCoroutine(fadeCoroutine);
//
//         fadeCoroutine = StartCoroutine(FadeToNewClip(newClip, targetVolume));
//     }
//
//     private IEnumerator FadeToNewClip(AudioClip newClip, float targetVolume)
//     {
//         var fadeDuration = 1f;
//         var startVolume = audioSource.volume;
//
//         for (var t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
//         {
//             audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
//             yield return null;
//         }
//
//         audioSource.volume = 0f;
//         audioSource.Stop();
//
//         audioSource.clip = newClip;
//         if (newClip != null)
//         {
//             audioSource.Play();
//
//             for (var t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
//             {
//                 audioSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
//                 yield return null;
//             }
//
//             audioSource.volume = targetVolume;
//         }
//     }
// }
using UnityEngine;
using System.Collections;

public class SceneMusicPlayer : MonoBehaviour
{
    [Header("Music Settings")]
    [SerializeField] private AudioClip sceneMusic;
    [SerializeField] private float volume = 0.5f;
    [SerializeField] private float fadeInDuration = 1f;
    [SerializeField] private float fadeOutDuration = 1f;
    [SerializeField] private bool playOnAwake = true;

    private AudioSource audioSource;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.clip = sceneMusic;

        if (playOnAwake)
        {
            Play();
        }
    }

    public void Play()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeIn());
    }

    public void Stop()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        audioSource.volume = 0f;
        audioSource.Play();

        var elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, volume, elapsedTime / fadeInDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        audioSource.volume = volume;
        fadeCoroutine = null;
    }

    private IEnumerator FadeOut()
    {
        var startVolume = audioSource.volume;

        var elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeOutDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
        fadeCoroutine = null;
    }

    private void OnDestroy()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
    }
}