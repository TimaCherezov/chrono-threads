using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CanHeadContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] cans;
    [SerializeField] private List<GameObject> cansHistory;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private FadeController fadeController;
    [SerializeField] private AudioClip correctSound;
    [SerializeField] private AudioClip wrongSound; 
    private AudioSource audioSource; 


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        cansHistory = new List<GameObject>(cans.Length);
        foreach (var can in cans)
        {
            // review: разве не должна происходить отписка в какой-то момент?
            can.GetComponent<CanStateContainer>()
                .onStateChange.AddListener(() => OnCanStateChanged(can));

            Debug.Log("ADDED");
        }
    }

    void OnCanStateChanged(GameObject can)
    {
        Debug.Log("ON STATE CHENED");
        if (can.GetComponent<CanStateContainer>().IsActive)
        {
            cansHistory.Add(can);
            CheckSequenceCorrectness();
        }
        else
            cansHistory.Remove(can);
    }

    void CheckSequenceCorrectness()
    {
        if (cansHistory.Count != cans.Length)
            return;
        if (cansHistory.SequenceEqual(cans))
        {
        {
            Debug.Log("DONE");
            audioSource.PlayOneShot(correctSound);
            fadeController.StartFadeOut();
            ShowObject();
        }
        else
        {
            Debug.Log("WRONG");
            cansHistory.Clear();
            audioSource.PlayOneShot(wrongSound);
            foreach (var can in cans)
            {
                can.GetComponent<CanStateContainer>().SetState(false);
            }
        }
    }

    void ShowObject()
    {
        if (targetObject != null)
            targetObject.SetActive(true);
    }
}
