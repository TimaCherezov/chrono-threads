using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [SerializeField] private FadeController fadeController;
    [SerializeField] private GameObject musicController;
    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene(int id)
    {
        fadeController.StartFadeIn();
        musicController.GetComponent<SceneMusicPlayer>().Stop();
        // BackGroundMusicController.StartFadeMusic
        StartCoroutine(LoadSceneWithDelay(id));
        
        // SceneManager.LoadScene(id);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void ResetCurrentScene()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        ChangeScene(sceneName);
    }
    private IEnumerator LoadSceneWithDelay(int id)
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(id);
    }
}