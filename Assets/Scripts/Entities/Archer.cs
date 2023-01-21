using System;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;
using Mathf = UnityEngine.Mathf;

public class Archer : Enemy
{
    
    private float t = 0f;
    private GameObject player;
    private Renderer renderer;
    private Camera camera;
    private State state;
    private NavMeshAgent navAgent;
    private float cooldown = 5f;
    private float spread = 20f;
    [SerializeField] private GameObject projectile;
    private enum State
    {
        
        STATE_SHOOTING,
        STATE_IDLE,
        STATE_MOVING,

    }
    // Start is called before the first frame update
    void Awake()
    {
        state = State.STATE_IDLE;
        renderer = GetComponent<Renderer>();
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        camera = Camera.main;
    }
    
    

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.STATE_IDLE:
                if (IsOnScreen())
                {
                    //Debug.Log("IDLE -> SHOOTING");
                    state = State.STATE_SHOOTING;
                }
                else
                {
                    //Debug.Log("IDLE -> MOVING");
                    state = State.STATE_MOVING;
                }

                break;
            case State.STATE_MOVING:
                navAgent.destination = player.transform.position;
                if (IsOnScreen())
                {
                    //Debug.Log("MOVING -> SHOOTING");
                    state = State.STATE_SHOOTING;
                }

                break;
            case State.STATE_SHOOTING:
                t += Time.deltaTime;
                if (t > cooldown)
                {
                    MultiShoot();
                    t = 0f;
                }
                navAgent.destination = transform.position;
                if (! IsOnScreen())
                {
                    t = 0f;
                    state = State.STATE_MOVING;
                }
                break;


                bool IsOnScreen()
                {
                    Vector3 screenPoint = camera.WorldToScreenPoint(transform.position);
                    if ( 100 < screenPoint.x && screenPoint.x < 1800)
                        if (100 < screenPoint.y && screenPoint.y < 800)
                        {
                            return true;
                        }

                    return false;
                }
        }
    }

    void MultiShoot()
    {
        //Debug.Log("MultiShooting");
        Vector3 toward = (player.transform.position - transform.position);
        int arrowNumber = Random.Range(1, 6);
        //Debug.Log("Arrow Count: "+ arrowNumber);
        for (int i = 0; i < arrowNumber; i++)
        {
            float mean = 0.5f + arrowNumber / 2f;
            float angle = ((i + 1) - mean) * spread;
            Shoot(toward,angle);
        }
    }

    void Shoot(Vector3 dir, float angleBias)
    {
        //Debug.Log("Shoot: " + dir + " " + angleBias);
        Quaternion toward = Quaternion.LookRotation(dir);
        GameObject p = Instantiate(projectile, transform.position,toward * Quaternion.AngleAxis(angleBias,Vector3.up));
    }
}
