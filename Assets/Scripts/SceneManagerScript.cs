using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void ResetCurrentScene()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        ChangeScene(sceneName);
    }
}
