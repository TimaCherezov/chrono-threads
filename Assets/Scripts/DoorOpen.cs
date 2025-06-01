using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFade : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private int nextScene;
    [SerializeField] private GameObject musicController;

    private bool hasTriggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true; 
            Debug.Log("Переход к следующей сцене...");
            GetComponent<AudioSource>()?.Play();
            fadeController?.StartFadeIn();
            musicController.GetComponent<AudioSource>().Stop();
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(nextScene);
    }
}