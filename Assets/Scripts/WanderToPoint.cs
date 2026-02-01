using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WanderToPoint : MonoBehaviour
{
    [SerializeField]
    Vector2 gridBounds;

    [SerializeField]
    float waitToWander = 5.0f;

    [SerializeField]
    float moveSpeed = 5.0f;

    [SerializeField]
    Ease moveEase = Ease.Linear;

    private float moveTimer;

    [SerializeField]
    private bool canMove = true;
    private Tweener moveTween;

    void Start()
    {
        moveTimer = Time.time + waitToWander;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
            return;

        // If has light, then don't move
        // IF LIGHT THEN RETURN

        if (moveTimer < Time.time)
        {
            MoveToRandomPoint();
            moveTimer = Time.time + waitToWander;
        }
    }

    // Pick random point based on the grid size
    Vector2 PickPoint()
    {
        // Return random number based off min/max
        return new Vector2(Random.Range(0, gridBounds.x), Random.Range(0, gridBounds.y));
    }

    private void MoveToRandomPoint()
    {
        moveTween?.Kill();

        Vector2 targetPoint = PickPoint();
        moveTween = transform.DOMove(targetPoint, moveSpeed).SetEase(moveEase);
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
        if (!canMove)
        {
            moveTween?.Kill();
        }
    }

    private void OnDestroy()
    {
        moveTween?.Kill();
        transform.DOKill();
    }
}
