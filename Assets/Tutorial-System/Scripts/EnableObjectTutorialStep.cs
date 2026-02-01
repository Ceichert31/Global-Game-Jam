using System;
using DG.Tweening;
using Tutorial_System.Scripts;
using UnityEngine;

public class EnableObjectTutorialStep : MonoBehaviour, ITutorialStep
{
    [Header("References")]
    [SerializeField] private GameObject objectToEnable;

    [Header("Display Settings")]
    [Tooltip("How long of a delay there should be before opening the next tutorial")]
    [SerializeField] private float timeBeforeOpen;

    [Header("Animation Settings")]
    [Tooltip("The speed at which the tutorial window opens")]
    [SerializeField] private float tutorialWindowOpenSpeed;

    [Tooltip("The easing function used when opening the tutorial window")]
    [SerializeField] private Ease tutorialWindowOpenEase;

    [Tooltip("The speed at which the tutorial window closes")]
    [SerializeField] private float tutorialWindowCloseSpeed;

    [Tooltip("The easing function used when closing the tutorial window")]
    [SerializeField] private Ease tutorialWindowCloseEase;

    public bool IsComplete => _isComplete;

    public EventHandler<EventArgs> OnComplete { get; set; }

    [Tooltip("Determines whether completing this tutorial step will advance to the next step")]
    public bool CanAdvanceTutorial { get => _canAdvanceTutorial; set => _canAdvanceTutorial = value; }

    [SerializeField]
    private bool _canAdvanceTutorial;

    private bool _isComplete;

    public void EnterStep()
    {
        transform.DOKill();
        transform.DOScaleY(1, tutorialWindowOpenSpeed).SetEase(tutorialWindowOpenEase);
    }

    /// <summary>
    /// Checks if the tutorial step has been complete, completes it if it hasn't
    /// </summary>
    public void CheckComplete()
    {
        if (IsComplete) return;

        //If y scale is 0, that means it is minimized and we shouldn't do anything
        if (transform.localScale.y == 0) return;

        _isComplete = true;
        OnComplete?.Invoke(this, EventArgs.Empty);
    }

    public void ExitStep()
    {
        transform.DOKill();
        transform.DOScaleY(0, tutorialWindowCloseSpeed).SetEase(tutorialWindowCloseEase);
        objectToEnable.SetActive(true);
    }
}
