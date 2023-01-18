using UnityEngine;

public class Entity : MonoBehaviour
{
    public static int maxHealth = 10;
    public int health = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0) Destroy(gameObject);
    }

    public void Heal(int healingAmount)
    {
        health += healingAmount;
        if (health > maxHealth) health = maxHealth;
    }
}