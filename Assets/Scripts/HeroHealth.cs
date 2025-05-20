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
    private Scrollbar _scrollbar; // review: не по кодстайлу

    private void Awake()
    {
        _scrollbar = healthBar.GetComponent<Scrollbar>();
        _scrollbar.size = CalculateScrollbarSize();
    }

    public void ApplyDamage(int damage)
    {
        // review: сложно читать, лучше в одну строку
        // currentHealth = Math.Clamp(currentHealth - damage)
        currentHealth += Math.Clamp(damage,
            -currentHealth,
            maxHealth - currentHealth
        );
        _scrollbar.size = CalculateScrollbarSize();
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
        ResetScene();
    }    

    private void ResetScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}