using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public int totalPuzzles = 9;
    private int completedPuzzles;
    [SerializeField] private GameObject player;
    [SerializeField] private FadeController fadeController;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private GameObject trigger;

    public void PuzzleCompleted()
    {
        completedPuzzles++;
        Debug.Log(completedPuzzles);
        if (completedPuzzles == totalPuzzles)
        {
            Debug.Log("completed");
            GetComponent<AudioSource>().Play();
            fadeController.StartFadeOut();
            puzzle.SetActive(false);
            trigger.SetActive(false);
            player.SetActive(true);
        }
    }
}