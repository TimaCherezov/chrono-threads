using System;
using UnityEngine;

public class DoorActivity : MonoBehaviour
{
    [SerializeField] private GameObject messageBox;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        var sc = other.gameObject.GetComponent<PastHero>();
        sc.DisableAnimations();
        messageBox.SetActive(true);
        other.gameObject.GetComponent<PastHero>().IsAllowedMove = false;
    }
}