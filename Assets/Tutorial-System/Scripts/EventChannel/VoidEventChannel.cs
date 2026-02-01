using UnityEngine;
using UnityEngine.Events;

namespace Tutorial_System.Scripts.EventChannel
{
    [CreateAssetMenu(fileName = "EventChannel", menuName = "EventChannel/Void Event Channel")]
    public class VoidEventChannel : ScriptableObject
    {
        [HideInInspector] public UnityEvent OnEventCalled;
        public void CallEvent() => OnEventCalled?.Invoke();
    }
}
