using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    private List<Entity> entities = new List<Entity>();

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(TickAOE());
    }

    private void OnTriggerEnter(Collider other)
    {
        entities.Add(other.gameObject.GetComponent<Entity>());
    }


    private void OnTriggerExit(Collider other)
    {
        entities.Remove(other.gameObject.GetComponent<Entity>());
    }

    private IEnumerator TickAOE()
    {
        for (var i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1);

            var copy = new List<Entity>(entities);
            foreach (var entity in copy)
                if (entity.CompareTag("Player"))
                    entity.TakeDamage(1);
                else if (entity.CompareTag("healableEnnemy")) entity.Heal(1);
        }
        Destroy(gameObject);
    }
}