using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDestroy : MonoBehaviour
{

    [SerializeField] int damage;
    [SerializeField] float force;
    // Start is called before the first frame update

    public void SetExplosionDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetExplosionForce(float force)
    {
        this.force = force;
    }
    
    

    private void OnDestroy()
    {
        LayerMask mask = LayerMask.GetMask("Enemy");
        Collider[] colliders =  Physics.OverlapSphere(transform.position, 0.5f,mask);
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<Enemy>().TakeDamage(damage);
            collider.GetComponent<Rigidbody>().AddExplosionForce(force,transform.position,3f);
        }
    }
}
