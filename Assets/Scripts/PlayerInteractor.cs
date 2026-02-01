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
    private IInteractable heldPropData;

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

        if (!collision.gameObject.TryGetComponent(out heldPropData))
            return;

        heldProp = collision.gameObject;

        PickUpProp();
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
            heldProp.transform.DOKill();

            heldProp.transform.SetParent(null);

            //Execute interact logic, move object to hold pos
            grabTween = heldProp.transform.DOMove(targetPos, grabDuration).SetEase(grabEaseMode);

            canPickUp = false;
            Invoke(nameof(ResetPickupDelay), PickUpDelay);

            heldPropData.Drop();
        }
        finally
        {
            canInteract = false;
            isHolding = false;
            heldProp = null;
            heldPropData = null;
        }
    }

    private void PickUpProp()
    {
        if (!canPickUp)
            return;

        heldPropData.PickUp();

        canInteract = false;
        isHolding = true;

        heldProp.transform.DOKill();
        heldProp.transform.SetParent(holdPos);

        //Execute interact logic, move object to hold pos
        grabTween = heldProp
            .transform.DOLocalMove(Vector2.zero, grabDuration)
            .SetEase(grabEaseMode)
            .OnComplete(() =>
            {
                heldProp.transform.localPosition = Vector2.zero;
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
