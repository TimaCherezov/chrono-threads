using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;
    public int totalPuzzles = 9;
    private int completedPuzzles;
    public GameObject Player;

    void Awake()
    {
        Instance = this;
    }

    public void PuzzleCompleted()
    {
        completedPuzzles++;
        if (completedPuzzles == totalPuzzles)
        {
            GetComponent<AudioSource>().Play();
            Player.SetActive(true);
        }
    }
}