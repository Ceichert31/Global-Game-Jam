using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Windows;

public class LightingContols : MonoBehaviour
{
    // Controls
    private PlayerControls lightControls;

    private PlayerControls.LightingActions lightActions;

    // Parameters
    [SerializeField]
    float lightingBoundLeft;

    [SerializeField]
    float lightingBoundRight;

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
        //transform.position = lightingBoundLeft;
        // Set to center stage
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveLight();
    }

    private void MoveLight()
    {
        // Move direction that is pressed
        //if (lightActions.MoveLight.ReadValue<float>() != 0)
        //{
        //    //transform.position = new Vector3(
        //    //    transform.position.x + (lightActions.MoveLight.ReadValue<float>() * moveSpeed),
        //    //    lightingBoundLeft.y
        //    //);
        //}

        if (lightActions.MoveLight.ReadValue<float>() != 0)
        {
            transform.Rotate(
                new Vector3(
                    0.0f,
                    0.0f,
                    -lightActions.MoveLight.ReadValue<float>() * Time.deltaTime * moveSpeed
                ),
                Space.World
            );

            // Get current rotation in -180 to 180 range
            float currentZ = transform.eulerAngles.z;
            if (currentZ > 180f)
                currentZ -= 360f;

            // Calculate and clamp new rotation
            float newZ = Mathf.Clamp(
                currentZ - lightActions.MoveLight.ReadValue<float>() * Time.deltaTime * moveSpeed,
                lightingBoundRight,
                lightingBoundLeft
            );

            transform.rotation = Quaternion.Euler(0, 0, newZ);
        }
    }
}

/**
 * Setting up local rotation around pivot
 */
