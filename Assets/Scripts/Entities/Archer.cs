using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class Archer : Enemy
{
    [SerializeField] private GameObject projectile;
    private Camera camera;
    private float cooldown = 5f;
    private NavMeshAgent navAgent;
    private GameObject player;
    private float spread = 20f;
    private State state;


    private float t;

    // Start is called before the first frame update
    private void Start()
    {
        state = State.STATE_IDLE;
        navAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Hero>().gameObject;
        camera = Camera.main;
    }


    // Update is called once per frame
    private void Update()
    {
        if (player == null) return;
        switch (state)
        {
            case State.STATE_IDLE:
                animator.SetBool("Idle",true);
                animator.SetBool("Move",false);
                animator.SetBool("Attack",false);
                if (navAgent.enabled == false)
                {
                    state = State.STATE_IDLE;
                    break;
                }
                
                if (IsOnScreen())
                {
                    state = State.STATE_SHOOTING;
                }
                else
                {
                    state = State.STATE_MOVING;
                }

                break;
            case State.STATE_MOVING:
                animator.SetBool("Move",true);
                animator.SetBool("Idle",false);
                animator.SetBool("Attack", false);
                if (navAgent.enabled == false)
                {
                    state = State.STATE_IDLE;
                    break;
                }
                navAgent.destination = player.transform.position;
                if (IsOnScreen())
                {
                    
                    state = State.STATE_SHOOTING;
                }

                break;
            case State.STATE_SHOOTING:
                animator.SetBool("Move",false);
                animator.SetBool("Idle",false);
                animator.SetBool("Attack", false);
                if (navAgent.enabled == false)
                {
                    state = State.STATE_IDLE;
                    break;
                }
                
                
                t += Time.deltaTime;
                if (t > cooldown)
                {
                    animator.SetBool("Attack",true);
                    StartCoroutine(MultiShoot());
                    t = 0f;
                }

                navAgent.destination = transform.position;
                animator.SetBool("Idle",true);
                animator.SetBool("Move",false);
                if (!IsOnScreen())
                {
                    t = 0f;
                    state = State.STATE_MOVING;
                }

                break;


                bool IsOnScreen()
                {
                    var screenPoint = camera.WorldToScreenPoint(transform.position);
                    if (Screen.width *0.05f < screenPoint.x && screenPoint.x < Screen.width * 0.95f)
                        if (Screen.height * 0.05f < screenPoint.y && screenPoint.y < Screen.height * 0.95f)
                            return true;

                    return false;
                }
        }
    }

    private IEnumerator MultiShoot()
    {
        yield return new WaitForSeconds(0.6f);
        Vector3 toward = player.transform.position - transform.position;
        int arrowNumber = Random.Range(1, 6);
        for (var i = 0; i < arrowNumber; i++)
        {
            var mean = 0.5f + arrowNumber / 2f;
            var angle = (i + 1 - mean) * spread;
            Shoot(toward, angle);
        }
        animator.SetBool("Attack",false);
    }

    private void Shoot(Vector3 dir, float angleBias)
    {
        var toward = Quaternion.LookRotation(dir);
        var p = Instantiate(projectile, transform.position, toward * Quaternion.AngleAxis(angleBias, Vector3.up));
    }

    private enum State
    {
        STATE_SHOOTING,
        STATE_IDLE,
        STATE_MOVING
    }
}