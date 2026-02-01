using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropDetector : MonoBehaviour
{
    [SerializeField]
    private int backdropLayer;

    private WanderToPoint wander;

    private void Start()
    {
        wander = GetComponentInParent<WanderToPoint>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != backdropLayer)
            return;

        wander.SetCanMove(false);
    }
}
