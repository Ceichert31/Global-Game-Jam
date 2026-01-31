using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightingContols : MonoBehaviour
{
    // Parameters
    [SerializeField]
    Vector2 lightingBoundLeft;
    [SerializeField]
    Vector2 lightingBoundRight;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.position = lightingBoundLeft;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
