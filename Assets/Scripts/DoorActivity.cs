using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public GameObject pressE;
    public GameObject messageBox;

    private GameObject playerInZone;

    void Update()
    {
        if (playerInZone != null && Input.GetKeyDown(KeyCode.Q))
        {
            pressE.SetActive(false);
            GetComponent<AudioSource>().Play();
            messageBox.SetActive(true);
            playerInZone.GetComponent<PastHero>().IsAllowedMove = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        pressE.SetActive(true);
        playerInZone = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        pressE.SetActive(false);
        playerInZone = null;
    }
}