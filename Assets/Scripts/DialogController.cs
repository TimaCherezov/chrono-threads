using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject leftHider;
    [SerializeField] private GameObject rightHider;
    [SerializeField] private GameObject text;
    [SerializeField] private string[] replicas;
    [SerializeField] private bool[] replicasFromLeft;

    private void Awake()
    {
        if (replicasFromLeft.Length == replicas.Length)
            return;
        Debug.LogError("Replicas and replicasFromLeft arrays must have the same length.");
    }

    private int currentIndex = -1;

    public void MoveNext()
    {
        currentIndex++;
        if (currentIndex >= replicas.Length)
        {
            gameObject.SetActive(false);
            return;
        }

        var currentReplica = replicas[currentIndex];
        var isLeft = replicasFromLeft[currentIndex];

        if (isLeft)
        {
            leftHider.SetActive(false);
            rightHider.SetActive(true);
        }
        else
        {
            rightHider.SetActive(false);
            leftHider.SetActive(true);
        }

        text.GetComponent<TMP_Text>().text = currentReplica;
    }
}