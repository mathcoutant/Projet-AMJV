using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispawnAfterTime : MonoBehaviour
{
    [SerializeField] float timer;
    void Awake()
    {
        StartCoroutine(DispawnTimer());
    }

    IEnumerator DispawnTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}
