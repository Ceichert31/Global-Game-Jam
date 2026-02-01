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

    [Tooltip("How long the object shakes before moving")]
    [SerializeField]
    private float moveDelay = 0.7f;

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

    private const float DropDelay = 0.2f;

    // Breaking
    [Header("Breaking Settings")]
    [Tooltip("How long does the prop have until it turns into rubbish")]
    [SerializeField]
    private RangedFloat lifetime;
    private float lifetimeTimer;

    [SerializeField]
    private GameObject rubbish;

    [Header("Audio")]
    [SerializeField]
    AudioPitcherSO drops;

    [SerializeField]
    AudioPitcherSO pickups;
    AudioSource source;

    private WanderToPoint wander;

    public void Start()
    {
        UpdateTimer();
        updateTweenTickTimer = Time.time + updateTweenTick;

        lifetimeTimer = Time.time + Random.Range(lifetime.Min, lifetime.Max);
        Debug.Log(lifetimeTimer);

        source = GetComponent<AudioSource>();
        wander = GetComponent<WanderToPoint>();
    }

    void Update()
    {
        if (isHeld)
            return;

        //Timer runs out
        if (changeStateTimer < Time.time)
        {
            ShakeObject();
            Invoke(nameof(EnableMovingState), moveDelay);
        }

        if (lifetimeTimer < Time.time)
        {
            // cue breaking and turning into rubbish
            transform.DOKill();
            Instantiate(rubbish, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void EnableMovingState()
    {
        isMoving = true;
        Invoke(nameof(ResetActiveStatus), Random.Range(activeStateTime.Min, activeStateTime.Max));
        wander.SetCanMove(true);
        UpdateTimer();
    }

    private void ShakeObject()
    {
        //Shake action
        if (updateTweenTickTimer < Time.time)
        {
            updateTweenTickTimer = Time.time + updateTweenTick;

            transform.DOKill();

            //Cache current tween
            movementTween = transform
                .DOShakePosition(shakeDuration, shakeIntensity)
                .SetEase(shakeEaseMode)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void ResetActiveStatus()
    {
        isMoving = false;
        wander.SetCanMove(false);
    }

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
        transform.DOKill();
        isHeld = true;
        pickups.Play(source);
    }

    public void Drop()
    {
        Invoke(nameof(ResetIsHeld), DropDelay);
        drops.Play(source);
    }

    private void ResetIsHeld() => isHeld = false;

    public void setRubbish(GameObject rub)
    {
        rubbish = rub;
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
