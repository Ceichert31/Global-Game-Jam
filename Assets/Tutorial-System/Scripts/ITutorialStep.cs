using System;

namespace Tutorial_System.Scripts
{
    /// <summary>
    /// Interface that is used as blueprint for all tutorial steps
    /// </summary>
    public interface ITutorialStep
    {
        /// <summary>
        /// If true prevents the tutorial from being played again
        /// </summary>
        public bool IsComplete { get;}

        /// <summary>
        /// If true the next tutorial plays right after
        /// </summary>
        public bool CanAdvanceTutorial { get; set; }
        
        public EventHandler<EventArgs> OnComplete { get; set; }
    
        /// <summary>
        /// Entry logic for this tutorial step
        /// </summary>
        public void EnterStep();
    
        /// <summary>
        /// Check if the step is complete
        /// </summary>
        public void CheckComplete();
    
        /// <summary>
        /// Exit logic for this tutorial step
        /// </summary>
        public void ExitStep();
    }
}