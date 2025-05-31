using UnityEngine;

public class PuzzleActivity : MonoBehaviour
{
    [SerializeField] private GameObject pressE;
    [SerializeField] private GameObject puzzle;
    [SerializeField] private GameObject player;
    private bool playerInZone;
    private bool activated;

    private void Update()
    {
        if (activated || !playerInZone)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            pressE.SetActive(false);
            puzzle.SetActive(true);
            player.SetActive(false);
            activated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated || !other.CompareTag("Player"))
            return;

        pressE.SetActive(true);
        playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        pressE.SetActive(false);
        playerInZone = false;
    }
}