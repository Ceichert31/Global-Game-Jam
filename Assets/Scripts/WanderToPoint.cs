using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderToPoint : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private Vector2 gridBoundsMax;

    [SerializeField]
    private Vector2 gridBoundsMin;

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

    public Vector2 getMoveDirection()
    {
        return moveDirection;
    }

    public bool getMove()
    {
        return isCurrentlyMoving;
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
        Vector2 point = new Vector2(
            Random.Range(gridBoundsMin.x, gridBoundsMax.x),
            Random.Range(gridBoundsMin.y, gridBoundsMax.y)
        );

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
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Calculate center and size
        Vector2 center = (gridBoundsMin + gridBoundsMax) / 2f;
        Vector2 size = gridBoundsMax - gridBoundsMin;

        // Draw bounds rectangle
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(center, size);

        // Draw current movement target
        if (Application.isPlaying && isCurrentlyMoving)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, targetPoint);
            Gizmos.DrawSphere(targetPoint, 0.3f);
        }
    }
}
