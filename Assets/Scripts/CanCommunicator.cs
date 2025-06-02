using System;
using JetBrains.Annotations;
using UnityEngine;

public class CanCommunicator : MonoBehaviour
{
    [CanBeNull] private CanStateContainer target;
    [SerializeField] private GameObject pressE;

    private void Update()
    {
        if (target is not null &&
            Input.GetKeyDown(KeyCode.M))
        {
            pressE.SetActive(false);
            target.SetState(!target.IsActive);
            Debug.Log("CHANGED");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<CanStateContainer>(out var can))
            return;
        pressE.SetActive(true);
        Debug.Log("TURNED ON");
        target = can;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(typeof(CanStateContainer), out _))
            return;
        pressE.SetActive(false);
        Debug.Log("TURNED OFF");
        target = null;
    }
}