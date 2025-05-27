using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PaperActivity : MonoBehaviour
{
    [SerializeField] private GameObject paper;
    [SerializeField]private GameObject player;
    [SerializeField] private GameObject input;
    [SerializeField] private GameObject newBridge;
    [SerializeField] private GameObject oldBridge;
    [SerializeField] private GameObject correctSound;
    [SerializeField] private GameObject wrongSound;
    [SerializeField] private int correctAnswer = 1488;
    private TMP_InputField _inputField;
    private bool _entered = false;
    private bool _activated = false;

    private void Awake()
    {
        _inputField = input.GetComponent<TMP_InputField>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        _entered = true;
    }

    private void FixedUpdate()
    {
        if (_activated || !_entered || !Input.GetKeyDown(KeyCode.E))
            return;
        GetComponent<AudioSource>().Play();
        paper.SetActive(true);
        player.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        _entered = false;
    }

    public void OnClick()
    {
        if (int.TryParse(_inputField.text, out var answer) && answer == correctAnswer)
        {
            correctSound.GetComponent<AudioSource>().Play();
            // fadeout
            paper.SetActive(false);
            player.SetActive(true);
            newBridge.SetActive(true);
            oldBridge.SetActive(false);
            _activated = true;
        }
        else
        {
            Debug.Log("Wrong answer, try again!");
            wrongSound.GetComponent<AudioSource>().Play();
            _inputField.text = string.Empty;
        }
    }
}