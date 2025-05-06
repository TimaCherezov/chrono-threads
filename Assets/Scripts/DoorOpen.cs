using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFade : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("���������� ����������!!!");
            fadeController.StartFadeIn();  // ��������� ����������
            Debug.Log(SceneManager.sceneCount);
            SceneManager.LoadScene(1);    
        }
    }
}