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
    private Scrollbar _scrollbar;

    private void Awake()
    {
        _scrollbar = healthBar.GetComponent<Scrollbar>();
        _scrollbar.size = CalculateScrollbarSize();
        DeadPanel.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        currentHealth += Math.Clamp(damage,
            -currentHealth,
            maxHealth - currentHealth
        );
        _scrollbar.size = CalculateScrollbarSize();
        Die();
        Debug.Log($"Future hero has damage applied {damage}");
    }

    private float CalculateScrollbarSize() =>
        Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);

    public void Die()
    {
        DeadPanel.SetActive(true);        
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