using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static int maxHealth = 10;
    public int health = 10;

    private Material defaultMaterial;
    [SerializeField] private Material damagedMaterial;
    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        defaultMaterial = renderer.material;

    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageFlashing());
        health -= damage;
        if(health <= 0) Destroy(gameObject);
    }

    IEnumerator DamageFlashing()
    {
        renderer.material = damagedMaterial;
        yield return new WaitForSeconds(0.3f);
        renderer.material = defaultMaterial;

    }
    public void Heal(int healingAmount)
    {
        health += healingAmount;
        if (health > maxHealth) health = maxHealth;
    }
}