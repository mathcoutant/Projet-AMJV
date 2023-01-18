using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidB;
    public float speed = 20f;

    // actions options
    private bool canDoAction1 = true;
    public float cooldownAction1 = 0.3f;
    private bool canDoAction2 = true;
    public float cooldownAction2 = 1f;
    private bool canDoAction3 = true;
    public float cooldownAction3 = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        rigidB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
        RotatePlayerToMouse();

        if (Input.GetKeyDown(KeyCode.Mouse0) && canDoAction1)
        {
            canDoAction1 = false;
            // Do action 1
            StartCoroutine(Cooldown(1));
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDoAction2)
        {
            canDoAction2 = false;
            // Do action 2
            StartCoroutine(Cooldown(2));
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDoAction3)
        {
            canDoAction3 = false;
            // Do action 3
            StartCoroutine(Cooldown(3));
        }
    }

    void MovePlayer()
    {
        float hSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float vSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        rigidB.velocity = new Vector3(hSpeed, 0, -vSpeed);
    }
    void RotatePlayerToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
        
        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;

        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler( new Vector3(0, angle, 0));
    }
    
    IEnumerator Cooldown(int numAction)
    {
        switch (numAction)
        {
            case 1:
                yield return new WaitForSeconds(cooldownAction1);
                canDoAction1 = true;
                break;
            case 2:
                yield return new WaitForSeconds(cooldownAction2);
                canDoAction2 = true;
                break;
            case 3:
                yield return new WaitForSeconds(cooldownAction3);
                canDoAction3 = true;
                break;
        }

    }
}