using System;
using JetBrains.Annotations;
using UnityEngine;

public class CanCommunicator : MonoBehaviour
{
    [CanBeNull] private CanStateContainer target;

    private void Update()
    {
        if (target is not null &&
            Input.GetKeyDown(KeyCode.E))
        {
            target.SetState(!target.IsActive);
            Debug.Log("CHANGED");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<CanStateContainer>(out var can))
            return;
        Debug.Log("TURNED ON");
        target = can;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(typeof(CanStateContainer), out _))
            return;
        Debug.Log("TURNED OFF");
        target = null;
    }
}