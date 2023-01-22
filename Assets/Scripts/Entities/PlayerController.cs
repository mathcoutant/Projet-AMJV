using UnityEngine;
using System.Collections;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidB;
    private InGameCanvas inGameCanvas;
    public float speed = 20f;
    private Hero hero;

    // actions options
    [SerializeField] bool canDoAction1 = true;
    public float cooldownAction1 = 0.3f;
    [SerializeField] bool canDoAction2 = true;
    public float cooldownAction2 = 1f;
    [SerializeField] bool canDoAction3 = true;
    public float cooldownAction3 = 1f;

    // Start is called before the first frame update
    private void Start()
    {
        hero = gameObject.GetComponent<Hero>();
        rigidB = gameObject.GetComponent<Rigidbody>();
        inGameCanvas = GameObject.Find("InGameCanvas").GetComponent<InGameCanvas>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canDoAction1)
        {
            canDoAction1 = false;
            hero.Action1();
            StartCoroutine(Cooldown(1));
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDoAction2)
        {
            canDoAction2 = false;
            hero.Action2();
            StartCoroutine(Cooldown(2));
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDoAction3)
        {
            canDoAction3 = false;
            hero.Action3();
            StartCoroutine(Cooldown(3));
        }
    }

    private void FixedUpdate()
    {
        HandleMoveInput();
        RotatePlayerToMouse();
    }

    void HandleMoveInput()
    {
        float hSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float vSpeed = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 velocity = new Vector3(hSpeed, 0, -vSpeed);
        hero.Move(velocity);
    }
    void RotatePlayerToMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
        
        mousePos.x = mousePos.x - objPos.x;
        mousePos.y = mousePos.y - objPos.y;

        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler( new Vector3(0, angle, 0));
        hero.Rotate(rotation);
    }
    
    IEnumerator Cooldown(int numAction)
    {
        switch (numAction)
        {
            case 1:
                inGameCanvas.DisplayCooldown(1, cooldownAction1);
                yield return new WaitForSeconds(cooldownAction1);
                canDoAction1 = true;
                break;
            case 2:
                inGameCanvas.DisplayCooldown(2, cooldownAction2);
                yield return new WaitForSeconds(cooldownAction2);
                canDoAction2 = true;
                break;
            case 3:
                inGameCanvas.DisplayCooldown(3, cooldownAction3);
                yield return new WaitForSeconds(cooldownAction3);
                canDoAction3 = true;
                break;
        }

    }
}
