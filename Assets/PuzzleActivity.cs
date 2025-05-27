using UnityEngine;

public class PuzzleActivity : MonoBehaviour
{
    [SerializeField] private GameObject puzzle;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        puzzle.SetActive(true);
        
        other.gameObject.SetActive(false);
    }
}
