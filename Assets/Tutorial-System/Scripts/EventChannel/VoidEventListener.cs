using UnityEngine;
using UnityEngine.Events;

namespace Tutorial_System.Scripts.EventChannel
{
    public class VoidEventListener : MonoBehaviour
    {
        [SerializeField] private VoidEventChannel eventChannel;
    
        [SerializeField] private UnityEvent response;

        private void OnEnable()
        {
            if (eventChannel == null) return;
            eventChannel.OnEventCalled.AddListener(OnEventCalled);
        }

        private void OnDisable()
        {
            if (eventChannel == null) return;
            eventChannel.OnEventCalled.RemoveListener(OnEventCalled);
        }
    
        private void OnEventCalled() => response?.Invoke();
    }
}
