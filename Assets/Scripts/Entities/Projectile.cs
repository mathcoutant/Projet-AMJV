using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        rigidbody.velocity = speed * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}