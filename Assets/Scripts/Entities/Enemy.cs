using System;
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

    protected virtual void OnDestroy()
    {
        Instantiate(xpOrb, transform.position, transform.rotation);
        factory.decreaseEnemyCounter();
    }
}