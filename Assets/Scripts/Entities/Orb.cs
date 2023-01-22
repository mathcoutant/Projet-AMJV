using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] private float distance;
    private Transform target;
    private float force = 50f;
    private int remainingHit = 5;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        var v = Quaternion.AngleAxis(Time.time * speed * -10, Vector3.up) * new Vector3(distance,0,0);
        transform.position = target.position + v;

    }



    private void OnTriggerEnter(Collider other)
    {

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy)
        {
            enemy.TakeDamage(2);
            other.GetComponent<Rigidbody>().AddForce((other.transform.position - target.position).normalized * force);
            remainingHit--;
            if (remainingHit <= 0) Destroy(gameObject);
        }
    }
}
