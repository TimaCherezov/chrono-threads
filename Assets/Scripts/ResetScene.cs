using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] public GameObject[] targets;

    private void ResetSceneInGame()
    {
        foreach (var target in targets)
        {
            if (target != null)
            {
                var healthTarget = target.GetComponentInParent<HeroHealth>();
                if (healthTarget != null)
                {

                }
            }
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}