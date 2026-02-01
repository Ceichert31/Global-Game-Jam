using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickActor : MonoBehaviour
{
    
    private enum PICKCHARACTER
    {
        Kathy = 0,
        Hector,
        Random = -1,
    }

    [SerializeField]
    [Tooltip("Choose your character!")]
    private PICKCHARACTER character;

    // Set right character
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if ((int)character < 0) // Pick random
        {
            var count = Enum.GetNames(typeof(PICKCHARACTER)).Length;
            int final = UnityEngine.Random.Range(0, 2); // for now
            transform.GetChild(final).gameObject.SetActive(true);
            return;

        }
        
        transform.GetChild((int)character).gameObject.SetActive(true);
    }

  
}
