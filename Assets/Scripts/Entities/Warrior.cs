using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Warrior : Hero
{
    public new static string nameClass = "Warrior";
    public new static int waveReached = 0;
    public new static int timesPlayed = 0;
    public new static bool hasWon = false;

    [SerializeField] private GameObject swordHitPosition;
    [SerializeField] private float hitRadius;
    [SerializeField] private float swirlRadius;
    private ParticleSystem particleSystem;
    private Camera cam;
    [SerializeField] private float poundRadius;
    [SerializeField] private float poundKnockback;

    private int swordDamage = 5;
    private int jumpDamage = 2;
    private int spinDamage = 1;

    protected override void Start() {
        base.Start();
        cam = Camera.main;
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public Warrior()
    {
        maxHealth = 50;
        health = maxHealth;
    }
    
    public override void Action1()
    {
        StartCoroutine(SwordAttack());
    }

    private IEnumerator SwordAttack()
    {
        animator.SetBool("attack1",true);
        state = HeroState.STATE_STUN;
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("attack1", false);
        state = HeroState.STATE_MOVE;
        
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(swordHitPosition.transform.position,hitRadius,mask);
        foreach (Collider col in colliders)
        {
            col.gameObject.GetComponent<Entity>().TakeDamage(swordDamage);
            col.gameObject.GetComponent<Rigidbody>().AddForce((col.transform.position - transform.position)*200f);
        }
    }
    public override void Action2()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 pos;
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
            StartCoroutine(JumpAttack(pos));

        }

    }

    private IEnumerator JumpAttack(Vector3 pos)
    {
        state = HeroState.STATE_STUN;
        animator.SetBool("attack2",true);
        rigidbody.velocity = SolveInitialVelocityForJump(transform.position, pos); 
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("attack2",false);
        
    }
    public override void Action3()
    {
        StartCoroutine(SwirlAttack());
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(swordHitPosition.transform.position,hitRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,swirlRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,poundRadius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Floor"))
        {
            state = HeroState.STATE_MOVE;
            particleSystem.Play();
            DoPoundDamage();
        }
    }

    IEnumerator SwirlAttack()
    {
        animator.SetBool("attack3",true);
        speed *= 1.5f;
        for(int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.5f);
            DoSwirlDamage();
        }
        speed /= 1.5f;
        animator.SetBool("attack3",false);
    }

    private void DoSwirlDamage()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, swirlRadius,mask);
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<Enemy>().TakeDamage(1);
        }

    }

    private void DoPoundDamage()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, poundRadius, mask);
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<Enemy>().TakeDamage(jumpDamage);
            collider.GetComponent<Rigidbody>().AddExplosionForce(poundKnockback,transform.position,poundRadius);
        }
    }

    public override void ApplyUpgrade(string upgrade)
    {
        base.ApplyUpgrade(upgrade);
        switch (upgrade)
        {
            case "Sword Damage Up":
                swordDamage++;
                break;
            case "Jump Damage Up":
                jumpDamage++;
                break;
            case "Spin Damage Up":
                spinDamage++;
                break;
            case "Jump range up":
                //TODO ajouter la range pour le jump
                break;
            case "Extended damage zone":
                swirlRadius += 1f;
                break;
        }
    }

    private Vector3 SolveInitialVelocityForJump(Vector3 initialPosition, Vector3 finalPosition)
    {
        Vector3 velocity = (initialPosition - finalPosition) - Physics.gravity / 2;
        velocity.x = -velocity.x;
        velocity.z = -velocity.z;
        return velocity;
    }
}