using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject warriorPrefab;
    [SerializeField] private GameObject magePrefab;
    [SerializeField] private GameObject roguePrefab;
    private void Awake()
    {
        switch (CharacterSelection.classSelection)
        {
            case "Warrior":
                Instantiate(warriorPrefab, transform.position,transform.rotation);
                break;
            case "Mage":
                Instantiate(magePrefab,transform.position,transform.rotation);
                break;
            case "Rogue":
                Instantiate(roguePrefab,transform.position,transform.rotation);
                break;
            default:
                Instantiate(magePrefab, transform.position, transform.rotation);
                break;
        }

  
    }
}
