using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFade : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private int nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("���������� ����������!!!");
            GetComponent<AudioSource>().Play();
            fadeController.StartFadeIn(); // ��������� ����������
            Debug.Log(SceneManager.sceneCount);
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    private IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(nextScene);
    }
}
