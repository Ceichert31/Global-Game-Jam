using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;


public class LightingContols : MonoBehaviour
{
    // Controls
    private PlayerControls lightControls;

    private PlayerControls.LightingActions lightActions;

    
    // Parameters
    [SerializeField]
    Vector2 lightingBoundLeft;
    [SerializeField]
    Vector2 lightingBoundRight;
    [SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        lightControls.Enable();
    }

    private void OnDisable()
    {
        lightControls.Disable();
    }

    private void Awake()
    {
        lightControls = new PlayerControls();
        lightActions = lightControls.Lighting;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = lightingBoundLeft;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveLight();
    }

    private void MoveLight()
    {
        // Move direction that is pressed
        if (lightActions.MoveLight.ReadValue<float>() != 0)
        {
            transform.position = new Vector3(transform.position.x + (lightActions.MoveLight.ReadValue<float>() * moveSpeed), lightingBoundLeft.y);
        }

        if (transform.position.x < lightingBoundLeft.x)
        {
            transform.position = lightingBoundLeft;
        }
        else if (transform.position.x > lightingBoundRight.x)
        {
            transform.position = lightingBoundRight;
        }
        
    }

}
