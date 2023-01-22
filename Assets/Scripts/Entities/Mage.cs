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
            Shoot(target);
        }
    }

    public override void Action2()
    {
        Instantiate(orbProjectile);
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

    private void Shoot(Vector3 target)
    {
        Quaternion toward = Quaternion.LookRotation(target);
        GameObject obj = Instantiate(fireProjectile, transform.position, toward);
        obj.GetComponent<ExplodeOnDestroy>().SetExplosionDamage(explodeDamage);
    }


}