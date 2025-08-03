using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health, IDataPersistence
{
    [SerializeField] private bool immune;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider levelBar;
    [SerializeField] private Slider staminaBar;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI levelText = null;
    protected float tempHP = 100f;
    protected float currentLevel = 0f;
    protected float currentStamina = 100f;
    protected int levelUp = 1;
    public static bool gameover = false;

    public void LoadData(GameData data)
    {
        tempHP = data.health;
        currentLevel = data.level;
        levelUp = data.levelUp;
        transform.position = new Vector3(data.playerPosition.x, data.playerPosition.y, data.playerPosition.z);
    }

    public void SaveData(ref GameData data)
    {
        if (this == null) return;
        data.health = currentHealth;
        data.level = currentLevel;
        data.levelUp = levelUp;
        data.playerPosition.x = transform.position.x;
        data.playerPosition.y = transform.position.y;
        data.playerPosition.z = transform.position.z;
    }

    protected override void Start()
    {
        base.Start();
        immune = false;    
        currentHealth = tempHP;
        healthBar.value = currentHealth / totalHealth;
        levelBar.value = currentLevel / totalHealth;
        staminaBar.value = currentStamina / totalHealth;
        levelText.text = "Level " + levelUp;
    }

    void Update()
    {
        Stamina();
        if (gameover)
        {
            gameOverScreen.SetActive(true);            
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Application.Quit();
                Debug.Log("Quit");
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        if (immune) return;
        healthBar.value = currentHealth / totalHealth;
        animator.SetTrigger("Hit");
    }

    protected override void Die()
    {
        base.Die();
        animator.SetTrigger("Death");        
        foreach (Component component in GetComponentsInChildren<Component>())
        {
            if (component.GetType() != typeof(SkinnedMeshRenderer) && component.GetType() != typeof(Transform)
                && component.GetType() != typeof(Animator) && component.GetType() != typeof(PlayerHealth))
            {
                Destroy(component);
            }
        }
        foreach (Enemy enemy in FindObjectsOfType<Enemy>())
        {
            enemy.LostPlayer(gameObject);
        }
        gameover = true;
    }

    public void Heal(float hp)
    {
        currentHealth = Mathf.Min(currentHealth + hp, totalHealth);
        healthBar.value = currentHealth / totalHealth;
    }

    public void Level(float xp)
    {
        currentLevel = Mathf.Min(currentLevel + (xp / levelUp), totalHealth);
        levelBar.value = currentLevel / totalHealth;
        if (levelBar.value.Equals(1))
        {
            levelUp++;
            levelText.text = "Level " + levelUp;
            levelBar.value = 0;
            currentLevel = 0;
        }        
    }
    public void Stamina()
    {
        if(animator.GetFloat("Speed") > 3f)
        {
            currentStamina = Mathf.Max(currentStamina - 1, 0f);
        }
        else
        {
            currentStamina = Mathf.Min(currentStamina + 1, totalHealth);            
        }
        staminaBar.value = currentStamina / totalHealth;        
    }
}
