using System;
using System.Collections.Generic;
using Tutorial_System.Scripts;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [Tooltip("The list that contains all tutorial steps")]
    [SerializeField]
    private List<GameObject> tutorialSteps;

    private int _currentStep = 0;

    private ITutorialStep _currentStepLogic;

    private void Start()
    {
        //Try to get interface
        if (!tutorialSteps[_currentStep].TryGetComponent(out _currentStepLogic)) return;
        
        //Do entry logic
        _currentStepLogic.EnterStep();
        _currentStepLogic.OnComplete += AdvanceStep;
    }

    /// <summary>
    /// Calls exit logic for current step and moves to next step
    /// </summary>
    /// <remarks>
    /// Called by event
    /// </remarks>
    private void AdvanceStep(object sender, EventArgs args)
    {
        if (_currentStep > tutorialSteps.Count - 1)
            return;

        if (_currentStepLogic != null)
        {
            //Check current step null, do exit logic 
            _currentStepLogic.ExitStep();

            //Return if step is marked as not advanceable
            if (!_currentStepLogic.CanAdvanceTutorial)
            {
                _currentStepLogic.CanAdvanceTutorial = true;
                return;
            }
        }

        //Advance to next step
        _currentStep++;
        if (_currentStep > tutorialSteps.Count - 1)
            return;

        //Try to get interface
        if (!tutorialSteps[_currentStep].TryGetComponent(out _currentStepLogic)) return;
        
        //Do entry logic
        _currentStepLogic.EnterStep();
        _currentStepLogic.OnComplete += AdvanceStep;
    }

    /// <summary>
    /// Loads the next tutorial step manually
    /// </summary>
    /// <remarks>
    /// This is called by event channels
    /// </remarks>
    public void LoadNextTutorial()
    {
        AdvanceStep(this, EventArgs.Empty);
    }
}
