using System;
using UnityEngine;
using UnityEngine.Events;

public class CanStateContainer : MonoBehaviour
{
    [SerializeField] private bool isActive;
    public bool IsActive => isActive;
    public UnityEvent onStateChange;


    [SerializeField] private GameObject onBar;
    [SerializeField] private GameObject offBar;


    public void SetState(bool state)
    {
        isActive = state;
        onBar.SetActive(state);
        offBar.SetActive(!state);
        onStateChange.Invoke();
    }
}