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

    private Tweener grabTween;

    private bool canInteract;

    private bool isHolding;

    private const float InteractWindow = 0.1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canInteract)
            return;

        if (collision.gameObject.layer != interactLayer)
            return;

        if (!collision.gameObject.TryGetComponent(out IInteractable interact))
            return;

        canInteract = false;

        collision.transform.SetParent(holdPos);

        grabTween?.Kill();

        //Execute interact logic, move object to hold pos
        grabTween = collision
            .transform.DOLocalMove(Vector2.zero, grabDuration)
            .SetEase(grabEaseMode);

        interact.Interact();
    }

    public void FlagInteract()
    {
        if (canInteract)
            return;

        canInteract = true;
        Invoke(nameof(ResetInteractFlag), InteractWindow);
    }

    private void ResetInteractFlag() => canInteract = false;
}
