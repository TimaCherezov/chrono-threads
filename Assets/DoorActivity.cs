using System;
using UnityEngine;

public class DoorActivity : MonoBehaviour
{
    [SerializeField] private GameObject messageBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        GetComponent<AudioSource>().Play();
        messageBox.SetActive(true);
        other.gameObject.GetComponent<PastHero>().IsAllowedMove = false;
    }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (!other.gameObject.CompareTag("Player"))
    //         return;
    //
    //     messageBox.SetActive(false);
    // }
}