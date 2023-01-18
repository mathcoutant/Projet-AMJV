using UnityEngine;

public class Entity : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;

    public void TakeDamage(int value)
    {
        health -= value;
        if (health < 0 ) { health = 0; }
    }

    public void GainHealth(int value)
    {
        health += value;
        if (health > maxHealth) { health = maxHealth; }
    }

}
