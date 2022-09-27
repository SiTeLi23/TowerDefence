using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    public Slider healthBar;

    public int moneyOnDeath = 50;
    public float totalHealth;

    private void Start()
    {
        healthBar.maxValue = totalHealth;
        healthBar.value = totalHealth;

        LevelManager.instance.ActiveEnemies.Add(this);
    }

    // Update is called once per frame
    private void Update()
    {
        healthBar.transform.rotation = Camera.main.transform.rotation;
    }

    public void TakeDamage(float damageAmount)
    {
        totalHealth -= damageAmount;
        if (totalHealth <= 0)
        {
            totalHealth = 0;
            Destroy(gameObject);
            MoneyManager.instance.GiveMoney(moneyOnDeath);
            LevelManager.instance.ActiveEnemies.Remove(this);
        }

        healthBar.value = totalHealth;
        healthBar.gameObject.SetActive(true);
    }
}