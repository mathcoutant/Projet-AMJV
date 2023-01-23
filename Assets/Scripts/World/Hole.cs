using UnityEngine;

public class Hole : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Hero>()) StartCoroutine(other.gameObject.GetComponent<Hero>().Die());
    }
}
