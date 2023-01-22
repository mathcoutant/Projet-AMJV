﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : Enemy
{
    private float cooldown = 1f;
    private NavMeshAgent navAgent;
    private GameObject player;
    private Collider playerCollider;
    [SerializeField] GameObject swordHitPoint;
    private bool canAttack = true;
    private float radius = 0.5f;
    private State state = State.STATE_IDLE;
    
    private enum State
    {
        STATE_ATTACKING,
        STATE_IDLE,
        STATE_MOVING
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        switch (state)
        {
            case State.STATE_IDLE:
                if (navAgent.enabled)
                {
                    navAgent.destination = player.transform.position;
                    state = State.STATE_MOVING;
                }

                break;
            case State.STATE_MOVING:
                if ((player.transform.position - transform.position).magnitude < 2f)
                {
                    navAgent.destination = transform.position;
                    if (canAttack)
                    {
                        state = State.STATE_ATTACKING;
                    }
                }
                else navAgent.destination = player.transform.position;
                break;
            case State.STATE_ATTACKING:
                navAgent.destination = transform.position;
                canAttack = false;
                StartCoroutine(Attack());
                state = State.STATE_IDLE;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(swordHitPoint.transform.position,radius);
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.7f);
        if (Physics.OverlapSphere(swordHitPoint.transform.position, radius).Contains(playerCollider))
        {
            player.GetComponent<Entity>().TakeDamage(5);
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return  new WaitForSeconds(1);
        canAttack = true;
    }
}