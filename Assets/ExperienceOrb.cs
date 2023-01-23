using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{

    [SerializeField] Transform player;

    [SerializeField] private float attractionSpeed;
    [SerializeField] private float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.position - transform.position;
        float speed = Mathf.Clamp(1 / dir.sqrMagnitude * attractionSpeed, 0, maxSpeed); 
        transform.Translate(dir * (speed * Time.deltaTime),Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<Hero>().IncrementXP();
            Destroy(gameObject);
        }
    }
}
