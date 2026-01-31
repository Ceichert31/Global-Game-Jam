using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioPitcherSO;

public class Prop : MonoBehaviour, IInteractable, IProp
{
    [Tooltip("How long before the prop starts moving again")]
    [SerializeField]
    private RangedFloat changeStateTime;

    [Tooltip("How long the prop is moving for")]
    [SerializeField]
    private RangedFloat activeStateTime;

    private bool isMoving;

    private float changeStateTimer;

    public void Start()
    {
        UpdateTimer();
    }

    void Update()
    {
        //Timer runs out
        if (changeStateTimer <= Time.time)
        {
            isMoving = true;
            Invoke(
                nameof(ResetActiveStatus),
                Random.Range(activeStateTime.Min, activeStateTime.Max)
            );
            UpdateTimer();
        }
        //Do random movement action while isMoving is true (shaking, jumping, rotating)
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

    public void Interact()
    {
        //Move prop to holding position
    }
}

public interface IInteractable
{
    public void Interact();
}

public interface IProp
{
    public bool GetPropStatus();
}
