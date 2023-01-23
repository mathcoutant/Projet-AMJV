using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Rogue : Hero
{
    public new static string nameClass = "Rogue";
    public new static int waveReached = 0;
    public new static int timesPlayed = 0;
    public new static bool hasWon = false;

    [SerializeField] private GameObject daggerHitPosition;
    [SerializeField] private GameObject dagger;
    [SerializeField] private float hitRadius;
    [SerializeField] private float smokeBombRadius;
    private ParticleSystem particleSystem;
    private Camera cam;
    
    private int poisonDamage = 0;
    private float daggerSpeed = 5f;
    private int daggerDamage = 2;
    private float stunDuration = 2f;

    public Rogue()
    {
        maxHealth = 20;
        health = maxHealth;
    }
    
    protected override void Start()
    {
        base.Start();
        cam = Camera.main;
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public override void Action1()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(daggerHitPosition.transform.position,hitRadius,mask);
        foreach (Collider col in colliders)
        {
            Entity entity = col.gameObject.GetComponent<Entity>();
            entity.TakeDamage(3);
            StartCoroutine(ApplyPoison(entity));
            col.gameObject.GetComponent<Rigidbody>().AddForce((col.transform.position - transform.position)*100f);
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
            Vector3 target = pos - transform.position;
            target.y = transform.position.y;
            Shoot(target);
        }
    }

    public override void Action3()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position,smokeBombRadius,mask);
        foreach (Collider col in colliders)
        {
            Entity entity = col.gameObject.GetComponent<Entity>();
            StartCoroutine(ApplyStun(entity));
        }
    }

    IEnumerator ApplyPoison(Entity entity)
    {
        for (int i = 0; i < poisonDamage; i++)
        {
            yield return new WaitForSeconds(1);
            if (!entity) break;
            entity.TakeDamage(1);
        }
    }

    IEnumerator ApplyStun(Entity entity)
    {
        NavMeshAgent navAgent = entity.GetComponent<NavMeshAgent>();
        navAgent.enabled = false;
        yield return new WaitForSeconds(stunDuration);
        navAgent.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(daggerHitPosition.transform.position,hitRadius);
    }
    
    private void Shoot(Vector3 target)
    {
        Quaternion toward = Quaternion.LookRotation(target);
        GameObject obj = Instantiate(dagger, transform.position, toward);
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.SetSpeed(daggerSpeed);
        projectile.SetDamage(daggerDamage);
    }

    public override void ApplyUpgrade(string upgrade)
    {
        base.ApplyUpgrade(upgrade);
        switch (upgrade)
        {
            case "Poisoned Knife":
                poisonDamage += 2;
                break;
            case "Throwing Knife Speed Up":
                daggerSpeed += 1f;
                break;
            case "Throwing Knife Damage Up":
                daggerDamage++;
                break;
            case "More Confusion":
                stunDuration += 0.5f;
                break;
            case "More Smoke":
                smokeBombRadius += 1f;
                break;
        }
    }

}