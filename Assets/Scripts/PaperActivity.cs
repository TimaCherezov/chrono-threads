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
    [SerializeField] private FadeController fadeController;
    [SerializeField] private int correctAnswer = 4;
    [SerializeField] private GameObject pressE;
    private TMP_InputField inputField;
    private bool entered;
    private bool activated;

    private void Awake()
    {
        inputField = input.GetComponent<TMP_InputField>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (activated || !other.gameObject.CompareTag("Player"))
            return;
        pressE.SetActive(true);
        entered = true;
    }

    private void Update()
    {
        if (activated || !entered || !Input.GetKeyDown(KeyCode.Q))
            return;
        pressE.SetActive(false);
        GetComponent<AudioSource>().Play();
        paper.SetActive(true);
        player.SetActive(false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        pressE.SetActive(false);
        entered = false;
    }

    public void OnClick()
    {
        if (int.TryParse(inputField.text, out var answer) && answer == correctAnswer)
        {
            correctSound.GetComponent<AudioSource>().Play();
            fadeController.StartFadeOut();
            paper.SetActive(false);
            player.SetActive(true);
            newBridge.SetActive(true);
            oldBridge.SetActive(false);
            activated = true;
            pressE.SetActive(false);
        }
        else
        {
            Debug.Log("Wrong answer, try again!");
            wrongSound.GetComponent<AudioSource>().Play();
            inputField.text = string.Empty;
        }
    }
}