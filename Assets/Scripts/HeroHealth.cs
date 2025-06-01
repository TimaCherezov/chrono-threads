using System;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth = 10;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject DeadPanel;
    [SerializeField] private AudioClip damageSound;
    private AudioSource audioSource;
    private Scrollbar scrollbar;

    private void Awake()
    {
        if (healthBar == null)
        {
            Debug.LogError("HealthBar не назначен в инспекторе!");
        }
        healthBar.fillAmount = CalculateScrollbarSize();
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
<<<<<<< Updated upstream
        currentHealth += Math.Clamp(damage,
                    -currentHealth,
                    maxHealth - currentHealth
                );        //currentHealth = currentHealth + damage >= 0 ? currentHealth + damage : 0;
        scrollbar.size = CalculateScrollbarSize();
=======
        currentHealth += Math.Clamp(damage, -currentHealth, maxHealth - currentHealth);
        //currentHealth = currentHealth + damage >= 0 ? currentHealth + damage : 0;
        healthBar.fillAmount = CalculateScrollbarSize();
>>>>>>> Stashed changes
        Debug.Log("The player takes damage!");
        if (currentHealth <= 0 && gameObject.tag == "Player")
        {
            Die();
            Debug.Log($"Future hero has damage applied {damage}");
        }
        if (currentHealth <= 0 && gameObject.tag == "Boss")
        {
            Destroy(gameObject);
            Destroy(scrollbar.gameObject.transform.parent.gameObject);
        }
    }

    private float CalculateScrollbarSize()
    {
        return Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);
    }

    public void Die()
    {
        DeadPanel.SetActive(true);        
        Time.timeScale = 0.3f;
        ResetScene();
    }

    private void ResetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}