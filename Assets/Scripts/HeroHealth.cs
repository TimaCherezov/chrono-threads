using System;
using System.Collections;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 15;
    [SerializeField] private int currentHealth = 15;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject DeadPanel;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private FadeController fadeController;
    [SerializeField] private GameObject musicController;
    [SerializeField] private AudioSource audioSourceForEnd;
    private AudioSource audioSource;
    private Scrollbar scrollbar;
    private bool isDead = false;
    public bool godMode = false;

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
        currentHealth += Math.Clamp(godMode ? 0 : damage, -currentHealth, maxHealth - currentHealth);        //currentHealth = currentHealth + damage >= 0 ? currentHealth + damage : 0;
        healthBar.fillAmount = CalculateScrollbarSize();
        Debug.Log("The player takes damage!");
        if (currentHealth <= 0 && gameObject.tag == "Player")
        {
            Die();
            Debug.Log($"Future hero has damage applied {damage}");
        }
        if (currentHealth <= 0 && gameObject.tag == "Boss" && !isDead)
        {
            isDead = true;
            audioSourceForEnd.PlayOneShot(audioSourceForEnd.clip);
            fadeController?.StartFadeIn();
            musicController.GetComponent<AudioSource>().Stop();
            StartCoroutine(LoadSceneWithDelay(4, 2.5f));
        }
        
    }

    private float CalculateScrollbarSize()
    {
        return Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);
    }

    public void Die()
    {
        Destroy(gameObject);
        DeadPanel.SetActive(true);
        Time.timeScale = 0.3f;
        ResetScene();
    }

    private void ResetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator LoadSceneWithDelay(int sceneIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneIndex);
    }
    
}