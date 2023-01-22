using UnityEngine;
using UnityEngine.AI;

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
        player = GameObject.FindObjectOfType<Hero>().gameObject;
        camera = Camera.main;
    }


    // Update is called once per frame
    private void Update()
    {
        switch (state)
        {
            case State.STATE_IDLE:
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
                
                if (navAgent.enabled == false)
                {
                    state = State.STATE_IDLE;
                    break;
                }
                
                
                t += Time.deltaTime;
                if (t > cooldown)
                {
                    MultiShoot();
                    t = 0f;
                }

                navAgent.destination = transform.position;
                if (!IsOnScreen())
                {
                    t = 0f;
                    state = State.STATE_MOVING;
                }

                break;


                bool IsOnScreen()
                {
                    var screenPoint = camera.WorldToScreenPoint(transform.position);
                    if (100 < screenPoint.x && screenPoint.x < 1800)
                        if (100 < screenPoint.y && screenPoint.y < 800)
                            return true;

                    return false;
                }
        }
    }

    private void MultiShoot()
    {
        var toward = player.transform.position - transform.position;
        var arrowNumber = Random.Range(1, 6);
        for (var i = 0; i < arrowNumber; i++)
        {
            var mean = 0.5f + arrowNumber / 2f;
            var angle = (i + 1 - mean) * spread;
            Shoot(toward, angle);
        }
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