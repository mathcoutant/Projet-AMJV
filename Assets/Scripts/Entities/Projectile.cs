using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    
    private void Update()
    {
        rigidbody.velocity = speed * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Entity entity = other.GetComponent<Entity>();
        if(entity) entity.TakeDamage(damage);
        Destroy(gameObject);
    }
}