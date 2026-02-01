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
            transform.DOMove(PickPoint(), moveSpeed).SetEase(moveEase);
            moveTimer = Time.time + waitToWander;
        }
    }

    // Pick random point based on the grid size
    Vector2 PickPoint()
    {
        // Return random number based off min/max
        Vector2 point = new Vector2(Random.Range(0, gridBounds.x), Random.Range(0, gridBounds.y));
        return point;
    }

    public void SetCanMove(bool canMove) => this.canMove = canMove;
}
