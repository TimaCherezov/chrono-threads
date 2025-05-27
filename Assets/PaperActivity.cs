using UnityEngine;

public class PaperActivity : MonoBehaviour
{
    public GameObject paper;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        GetComponent<AudioSource>().Play();
        paper.SetActive(true);
        other.gameObject.SetActive(false);
    }
}
