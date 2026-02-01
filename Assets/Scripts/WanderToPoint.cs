using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderToPoint : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private Vector2 gridBounds;

    [SerializeField]
    private float waitToWander = 5.0f;

    [SerializeField]
    private float moveDuration = 3.0f;

    [SerializeField]
    private float moveSpeed = 5.0f;

    private Rigidbody2D rb;
    private bool canMove = true;
    private bool isCurrentlyMoving = false;

    private Vector2 targetPoint;
    private Vector2 moveDirection;
    private float movementEndTime;
    private float nextMoveTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nextMoveTime = Time.time + waitToWander;
    }

    void Update()
    {
        if (!canMove)
            return;

        if (Vector2.Distance(targetPoint, (Vector2)transform.position) < 0.5f)
        {
            targetPoint = PickPoint();
        }

        // Check if it's time to start a new movement
        if (!isCurrentlyMoving && Time.time >= nextMoveTime)
        {
            StartNewMovement();
        }

        // Check if current movement should stop
        if (isCurrentlyMoving && Time.time >= movementEndTime)
        {
            StopMovement();
        }
    }

    private void FixedUpdate()
    {
        if (!canMove || !isCurrentlyMoving)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Apply velocity towards target
        rb.velocity = moveDirection * moveSpeed;
    }

    private void StartNewMovement()
    {
        targetPoint = PickPoint();
        moveDirection = (targetPoint - (Vector2)transform.position).normalized;

        isCurrentlyMoving = true;
        movementEndTime = Time.time + moveDuration;
    }

    private void StopMovement()
    {
        isCurrentlyMoving = false;
        rb.velocity = Vector2.zero;

        nextMoveTime = Time.time + waitToWander;
    }

    private Vector2 PickPoint()
    {
        Vector2 point = new Vector2(Random.Range(0, gridBounds.x), Random.Range(0, gridBounds.y));

        return point;
    }

    public void SetCanMove(bool value)
    {
        canMove = value;

        if (!canMove)
        {
            StopMovement();
        }
    }

    public void SetRbKinematic(bool isKinematic)
    {
        rb.isKinematic = isKinematic;

        if (isKinematic)
        {
            StopMovement();
        }
    }

    private void OnDisable()
    {
        // Stop movement when disabled
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    // Optional: Visualize in editor
    private void OnDrawGizmosSelected()
    {
        // Draw grid bounds
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(gridBounds / 2, gridBounds);

        // Draw current target
        if (isCurrentlyMoving && Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, targetPoint);
            Gizmos.DrawWireSphere(targetPoint, 0.5f);
        }
    }
}
