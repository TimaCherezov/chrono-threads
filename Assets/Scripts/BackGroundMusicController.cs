using UnityEngine;

public class BackGroundMusicController : MonoBehaviour
{
    private static BackGroundMusicController instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
