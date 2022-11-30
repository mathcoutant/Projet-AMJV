using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorWithClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        changeColor(randomColor());
    }

    private Color randomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1);
    }

    private void changeColor(UnityEngine.Color newcol)
    {
        gameObject.GetComponent<Renderer>().material.color = newcol;
    }
}
