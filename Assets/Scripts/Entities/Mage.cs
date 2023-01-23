using System.Collections;
using UnityEngine;

public class Mage : Hero
{

    public new static string nameClass = "Mage";
    public new static int waveReached = 0;
    public new static int timesPlayed = 0;
    public new static bool hasWon = false;

    [SerializeField] GameObject fireProjectile;
    [SerializeField] GameObject orbProjectile;
    [SerializeField] private GameObject iceWallPrefab;
    private Camera cam;
    
    private int explodeDamage = 5;
    private int orbHitCount = 5;
    private float orbKnockbackForce = 0f;
    private float iceWallSize = 1f;
    private int iceWallDamage = 0;

    public Mage()
{
    maxHealth = 20;
    health = maxHealth;
}

    protected override void Start()
{
        base.Start();
        cam = Camera.main;
    }

    public override void Action1()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 pos;
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
            Vector3 target = pos - transform.position;
            target.y = transform.position.y;
            StartCoroutine(Shoot(target));
        }
        
    }

    public override void Action2()
    {
        GameObject obj = Instantiate(orbProjectile);
        Orb orb = obj.GetComponent<Orb>();
        orb.SetHitCount(orbHitCount);
        orb.SetPushForce(orbKnockbackForce);
    }

    public override void Action3()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 pos;
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
            pos.y = transform.position.y;
            Instantiate(iceWallPrefab,pos,Quaternion.LookRotation(pos-transform.position,Vector3.up));
        }
    }

    private IEnumerator Shoot(Vector3 target)
    {        
        animator.SetBool("attack1",true);
        yield return new WaitForSeconds(0.2f);
        Quaternion toward = Quaternion.LookRotation(target);
        GameObject obj = Instantiate(fireProjectile, transform.position, toward);
        obj.GetComponent<ExplodeOnDestroy>().SetExplosionDamage(explodeDamage);
        animator.SetBool("attack1",false);
    }


    public override void ApplyUpgrade(string upgrade)
    {
        base.ApplyUpgrade(upgrade);
        switch (upgrade)
        {
            case "Fireball Damage Up":
                explodeDamage++;
                break;
            case "Knockback Orb":
                orbKnockbackForce += 50f;
                break;
            case "Resistant Orb":
                orbHitCount++;
                break;
            case "Wall Size Up":
                iceWallSize += 0.2f;
                break;
            case "Wall Damage":
                iceWallDamage++;
                break;
        }
    }
}