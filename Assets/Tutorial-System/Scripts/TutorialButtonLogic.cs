using Tutorial_System.Scripts.EventChannel;
using UnityEngine;

public class TutorialButtonLogic : MonoBehaviour
{
    [SerializeField] private VoidEventChannel advanceTutorialChannel;
    public void AdvanceTutorial()
    {
        advanceTutorialChannel.CallEvent();
    }
}
