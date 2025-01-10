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
        //Geeft de speler max health als je begint met het spel.

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
        //Geeft 10 damage als je op Q drukt.

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10f);
        }

        //Geeft de speler 10 health terug als je op E drukt.

        if (Input.GetKeyDown(KeyCode.E))
        {
            Heal(10f);
        }

        //Restart de game als je dood gaat en een death screen krijgt.

        if (deathScreen.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void TakeDamage(float damage) 
    { 
        //Zorgt ervoor dat als je damage krijgt dat het van de healthbar af gaat.
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null) 
        {
            healthBar.value = currentHealth;
        }
        //Als de speler minder dan 0 health heeft gaat de speler dood.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount) 
    { 
        //Zorgt ervoor dat als je healed dat je health bij je healthbar krijgt.
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    private void Die() 
    {
        //Zorgt ervoor dat als je dood bent dat het de deathScreen activeert en de tijd van de spel op stop zet.
        Debug.Log("Player is dead!");
        if (deathScreen != null) 
        {
            deathScreen.SetActive(true);
        }
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        //Zorgt ervoor dat als je de spel restart dat de tijd weer doorgaat en dat de scene wordt terug geladen.
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
