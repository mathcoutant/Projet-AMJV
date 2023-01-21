using System;
using System.Linq;
using UnityEditor.Media;
using UnityEngine;
using UnityEngine.AI;

public class Liche : Enemy
{
    [SerializeField] private GameObject areaOfEffect;
    private GameObject[] allies;
    private bool canAttack = true;
    private float cooldown = 10;
    private Collider player;
    private CapsuleCollider AOEcollider;
    private State state = State.STATE_IDLE;

    // Start is called before the first frame update
    private void Start()
    {
        AOEcollider = areaOfEffect.GetComponent<CapsuleCollider>();
        player = GameObject.FindWithTag("Player").GetComponent<Collider>();
        GameObject.FindGameObjectsWithTag("healableEnnemy");
    }

    // Update is called once per frame
    private void Update()
    {
        switch (state)
        {
            case State.STATE_IDLE:
                if (canAttack && PlayerInRange())
                {
                    state = State.STATE_ATTACKING;
                }
                break;

            case State.STATE_SEEKING:
                
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
        Instantiate(areaOfEffect, pos, transform.rotation);
        canAttack = false;
    }
    private bool PlayerInRange()
    {
        return Physics.OverlapSphere(transform.position,AOEcollider.radius * areaOfEffect.transform.localScale.x)
            .Contains(player);
    }
    

    private enum State
    {
        STATE_IDLE,
        STATE_SEEKING,
        STATE_ATTACKING
    }
}