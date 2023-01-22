using System;
using System.Collections;
using System.Linq;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.AI;

public class Liche : Enemy
{
    [SerializeField] private GameObject areaOfEffect;
    private GameObject[] allies;
    bool canAttack = true;
    private float cooldown = 10;
    private NavMeshAgent navAgent;
    private Transform player;
    [SerializeField] Transform target;
    private float range;
    private State state = State.STATE_IDLE;

    // Start is called before the first frame update
    private void Start()
    {
        range  = areaOfEffect.GetComponent<CapsuleCollider>().radius * areaOfEffect.transform.localScale.x;
        player = GameObject.FindWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(GetAllies());
    }

    // Update is called once per frame
    private void Update()
    {
        switch (state)
        {
            case State.STATE_IDLE:
                if (navAgent.enabled == false)
                {
                    state = State.STATE_IDLE;
                    break;
                }
                navAgent.destination = transform.position;
                if (canAttack && InRange(player)) state = State.STATE_ATTACKING;
                if (target != null)
                {
                    if (InRange(target) && canAttack)
                    {
                        state = State.STATE_ATTACKING;
                        break;
                    }
                }
                state = State.STATE_SEEKING;
                break;

            case State.STATE_SEEKING:
                if (navAgent.enabled == false)
                {
                    state = State.STATE_IDLE;
                    break;
                }
                AcquireTarget();
                if (target != null)
                {
                    navAgent.destination = target.position;
                }
                if (InRange(target) || InRange(player))
                {
                    state = State.STATE_IDLE;
                }
                break;

            case State.STATE_ATTACKING:
                Attack();
                state = State.STATE_IDLE;
                break;
        }
    }

    private void Attack()
    {
        Vector3 pos = transform.position + Vector3.down;
        Instantiate(areaOfEffect, pos, transform.rotation, transform);
        canAttack = false;
        StartCoroutine(Cooldown());
    }
    private bool InRange(Transform target)
    {
        return (this.transform.position - target.position).magnitude < range*0.8f;
    }

    IEnumerator GetAllies()
    {
        while (true)
        {
            allies = GameObject.FindGameObjectsWithTag("healableEnnemy");
            yield return new WaitForSeconds(2);
        }
    }

    private void AcquireTarget()
    {
        float minHealth = Mathf.Infinity;
        target = null;
        foreach (GameObject ally in allies)
        {
            float h = ally.GetComponent<Entity>().health;
            if (h < minHealth)
            {
                minHealth = h;
                target = ally.transform;
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(10);
        canAttack = true;
    }

    private enum State
    {
        STATE_IDLE,
        STATE_SEEKING,
        STATE_ATTACKING
    }
}