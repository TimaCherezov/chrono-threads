using System;
using System.Collections.Generic;
using UnityEngine;

public class CanHeadContainer : MonoBehaviour
{
    [SerializeField] private GameObject[] cans;
    [SerializeField] private List<GameObject> cansHistory;

    private void Awake()
    {
        cansHistory = new List<GameObject>(cans.Length);
        foreach (var can in cans)
        {
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
        for (var i = 0; i < cans.Length; i++)
        {
            if (cansHistory[i] != cans[i])
                return;
        }

        Debug.Log("DONE");
    }
}