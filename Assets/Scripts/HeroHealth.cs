using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int currentHealth = 10;
    [SerializeField] private GameObject healthBar;
    private Scrollbar _scrollbar;

    private void Awake()
    {
        _scrollbar = healthBar.GetComponent<Scrollbar>();
        _scrollbar.size = CalculateScrollbarSize();
    }

    public void ApplyDamage(int damage)
    {
        currentHealth += Math.Clamp(damage,
            -currentHealth,
            maxHealth - currentHealth
        );
        _scrollbar.size = CalculateScrollbarSize();
        Debug.Log($"Future target has damage applied {damage}");
    }

    private float CalculateScrollbarSize() =>
        Convert.ToSingle(currentHealth) / Convert.ToSingle(maxHealth);
}