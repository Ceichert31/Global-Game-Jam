using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static AudioPitcherSO;

public class Prop : MonoBehaviour, IInteractable, IProp
{
    [Header("Prop Settings")]
    [Tooltip("How long before the prop starts moving again")]
    [SerializeField]
    private RangedFloat changeStateTime;

    [Tooltip("How long the prop is moving for")]
    [SerializeField]
    private RangedFloat activeStateTime;

    private bool isMoving;

    private float changeStateTimer;

    private Tweener movementTween;

    private const float updateTweenTick = 1f;
    private float updateTweenTickTimer;

    [Header("Animation Settings")]
    [SerializeField]
    private float shakeDuration = 3;

    [SerializeField]
    private float shakeIntensity = 0.5f;

    [SerializeField]
    private Ease shakeEaseMode = Ease.InOutElastic;

    private bool isHeld;

    public void Start()
    {
        UpdateTimer();
        updateTweenTickTimer = Time.time + updateTweenTick;
    }

    void Update()
    {
        if (isHeld)
            return;

        //Timer runs out
        if (changeStateTimer < Time.time)
        {
            isMoving = true;
            Invoke(
                nameof(ResetActiveStatus),
                Random.Range(activeStateTime.Min, activeStateTime.Max)
            );
            UpdateTimer();
        }

        if (updateTweenTickTimer < Time.time && isMoving)
        {
            updateTweenTickTimer = Time.time + updateTweenTick;

            DOTween.CompleteAll();

            //Cache current tween
            movementTween = transform
                .DOShakePosition(shakeDuration, shakeIntensity)
                .SetEase(shakeEaseMode)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void ResetActiveStatus() => isMoving = false;

    private void UpdateTimer()
    {
        changeStateTimer = Random.Range(changeStateTime.Min, changeStateTime.Max) + Time.time;
    }

    /// <summary>
    /// Checks the props current status
    /// </summary>
    /// <returns>Whether the prop is active or not</returns>
    public bool GetPropStatus()
    {
        return isMoving;
    }

    public void PickUp()
    {
        //Kill all tweens
        movementTween?.Kill();
        isHeld = true;
    }

    public void Drop()
    {
        isHeld = false;
    }
}

public interface IInteractable
{
    public void PickUp();
    public void Drop();
}

public interface IProp
{
    public bool GetPropStatus();
}
