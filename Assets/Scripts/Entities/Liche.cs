using System.Collections;
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
        if (player == null) return;
        switch (state)
        {
            case State.STATE_IDLE:
                animator.SetBool("Idle",true);
                animator.SetBool("Move",false);
                animator.SetBool("Attack",false);
                if (navAgent.enabled == false)
                {
                    state = State.STATE_IDLE;
                    break;
                }
                navAgent.destination = transform.position;
                if (canAttack && InRange(player)) state = State.STATE_ATTACKING;
                if (target != null)
                {
                    if (InRange(target))
                    {
                        if (canAttack)
                        {
                            state = State.STATE_ATTACKING;
                            break;
                        }
                        else
                        {
                            state = State.STATE_IDLE;
                            break;
                        }
                    }
                }
                state = State.STATE_SEEKING;
                break;

            case State.STATE_SEEKING:
                animator.SetBool("Move",true);
                animator.SetBool("Idle",false);
                animator.SetBool("Attack", false);
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
                animator.SetBool("Move",false);
                animator.SetBool("Idle",false);
                Attack();
                state = State.STATE_IDLE;
                break;
        }
    }

    private void Attack()
    {
        animator.SetBool("Attack",true);
        navAgent.speed = 1f;
        Vector3 pos = transform.position + Vector3.down;
        Instantiate(areaOfEffect, pos, transform.rotation);
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
            allies = GameObject.FindGameObjectsWithTag("HealableEnemy");
            yield return new WaitForSeconds(2);
        }
    }

    private void AcquireTarget()
    {
        float minHealth = Mathf.Infinity;
        target = null;
        foreach (GameObject ally in allies)
        {
            if (!ally) break;
            float h = ally.GetComponent<Entity>().health;
            if (h < minHealth)
            {
                minHealth = h;
                target = ally.transform;
            }
        }

        if (target == null) target = player;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(5);
        navAgent.speed = 2f;
        yield return new WaitForSeconds(5);
        canAttack = true;
    }

    private enum State
    {
        STATE_IDLE,
        STATE_SEEKING,
        STATE_ATTACKING
    }
}