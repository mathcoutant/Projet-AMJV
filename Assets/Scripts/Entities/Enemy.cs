using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    private EnemyFactory factory;
    [SerializeField] private GameObject xpOrb;
    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<EnemyFactory>();
    }


    protected override IEnumerator Die()
    {
        factory.decreaseEnemyCounter();
        yield return new WaitForSeconds(1);
        Instantiate(xpOrb, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}