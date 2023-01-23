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
    protected Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        factory = FindObjectOfType<EnemyFactory>();
    }


    public override IEnumerator Die()
    {
        factory.decreaseEnemyCounter();
        animator.SetBool("Die",true);
        yield return new WaitForSeconds(1);
        Instantiate(xpOrb, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}