using System;
using UnityEngine;

public class DoorActivity : MonoBehaviour
{
    [SerializeField] private GameObject messageBox;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        GetComponent<AudioSource>().Play();
        messageBox.SetActive(true);
        other.gameObject.GetComponent<PastHero>().IsAllowedMove = false;
    }
}