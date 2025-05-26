using System;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth = 10;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject DeadPanel;
    [SerializeField] private AudioClip damageSound; 
    private AudioSource audioSource;
    private Scrollbar scrollbar; 

    private void Awake()
    {
        scrollbar = healthBar.GetComponent<Scrollbar>();
        scrollbar.size = CalculateScrollbarSize();
        audioSource = GetComponent<AudioSource>();
        // DeadPanel.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        // review: сложно читать, лучше в одну строку
        // currentHealth = Math.Clamp(currentHealth - damage)
        audioSource.loop = false;
        audioSource.clip = damageSound;
        audioSource.Play();
        // currentHealth += Math.Clamp(damage, -currentHealth, maxHealth - currentHealth);
        scrollbar.size = CalculateScrollbarSize();
        if (currentHealth <= 0)
        {
            Die();
            Debug.Log($"Future hero has damage applied {damage}");
        }
    }

    private float CalculateScrollbarSize() =>
        Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);

    public void Die()
    {
        // DeadPanel.SetActive(true);        
        // Time.timeScale = 0.3f;
        ResetScene();
        // Invoke("ResetScene", 3f);
    }    

    private void ResetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}