using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]
    private int interactLayer = 3;

    [SerializeField]
    private Transform holdPos;

    [Header("Animation Settings")]
    [SerializeField]
    private float grabDuration = 0.3f;

    [SerializeField]
    private Ease grabEaseMode = Ease.InOutElastic;

    private GameObject heldProp;

    private Tweener grabTween;

    private bool canInteract;

    private bool isHolding;

    [SerializeField]
    private Grid grid;

    private const float InteractWindow = 0.1f;

    private const float PickUpDelay = 0.15f;

    private bool canPickUp = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canInteract)
            return;

        if (collision.gameObject.layer != interactLayer)
            return;

        if (!collision.gameObject.TryGetComponent(out IInteractable interact))
            return;

        PickUpProp(collision.gameObject);

        interact.Interact();
    }

    /// <summary>
    /// Drops the currently held prop
    /// </summary>
    private void DropCurrentProp()
    {
        try
        {
            if (!isHolding)
                return;

            if (heldProp == null)
                return;

            //Handle dropping logic
            //Will need grid here

            var closetTile = grid.GetClosestTile(
                new Vector2(transform.position.x, transform.position.y)
            );

            if (closetTile == null)
                return;

            Vector2 targetPos = (Vector2)closetTile;

            //MoveToPosition(heldProp, null, targetPos);
            grabTween?.Kill();

            heldProp.transform.SetParent(null);

            Debug.Log($"End Position: {targetPos}");

            //Execute interact logic, move object to hold pos
            grabTween = heldProp.transform.DOMove(targetPos, grabDuration).SetEase(grabEaseMode);
        }
        finally
        {
            canPickUp = false;
            Invoke(nameof(ResetPickupDelay), PickUpDelay);

            canInteract = false;
            isHolding = false;
            heldProp = null;
        }
    }

    private void PickUpProp(GameObject obj)
    {
        if (!canPickUp)
            return;

        canInteract = false;
        isHolding = true;
        heldProp = obj;

        MoveToPosition(obj, holdPos, Vector2.zero);
    }

    private void MoveToPosition(GameObject obj, Transform parent, Vector2 endPosition)
    {
        grabTween?.Kill();

        obj.transform.SetParent(parent);

        Debug.Log($"End Position: {endPosition}");

        //Execute interact logic, move object to hold pos
        grabTween = obj
            .transform.DOLocalMove(endPosition, grabDuration)
            .SetEase(grabEaseMode)
            .OnComplete(() =>
            {
                obj.transform.localPosition = endPosition;
            });
    }

    private void ResetPickupDelay() => canPickUp = true;

    public void FlagInteract()
    {
        if (canInteract)
            return;

        DropCurrentProp();

        canInteract = true;
        Invoke(nameof(ResetInteractFlag), InteractWindow);
    }

    private void ResetInteractFlag() => canInteract = false;
}
