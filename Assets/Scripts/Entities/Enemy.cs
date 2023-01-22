using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Enemy : Entity
{
    private EnemyFactory factory;
    protected override void Awake()
    {
        base.Awake();
        factory = FindObjectOfType<EnemyFactory>();
    }

    protected virtual void OnDestroy()
    {
        factory.decreaseEnemyCounter();
    }
}