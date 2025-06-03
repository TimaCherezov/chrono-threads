using System;
using UnityEngine;
using UnityEngine.Events;

public class CanStateContainer : MonoBehaviour
{
    [SerializeField] public bool IsActive;
    public UnityEvent onStateChange;


    [SerializeField] private GameObject onBar;
    [SerializeField] private GameObject offBar;


    public void SetState(bool state)
    {
        IsActive = state;
        onBar.SetActive(state);
        offBar.SetActive(!state);
        onStateChange.Invoke();
        GetComponent<AudioSource>().Play();
    }
}