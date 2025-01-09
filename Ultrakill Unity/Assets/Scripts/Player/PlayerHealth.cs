using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider healthBar;
    public GameObject deathScreen; 
    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
        {     
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (deathScreen != null) 
        { 
            deathScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(10f);
        }

        if (deathScreen.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void TakeDamage(float damage) 
    { 
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null) 
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount) 
    { 
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void Die() 
    {
        Debug.Log("Player is dead!");
        if (deathScreen != null) 
        {
            deathScreen.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
