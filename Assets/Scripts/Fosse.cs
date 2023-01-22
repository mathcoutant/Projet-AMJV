using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fosse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    function OnCollisionEnter(Collision collision)
 {
     if( collision.gameObject.tag == "Player" )
     {
        collision.gameObject.health = 0;
        
     }
 }
}
