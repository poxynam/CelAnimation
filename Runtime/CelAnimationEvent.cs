using UnityEngine;

namespace CelAnimation
{
    [System.Serializable]
    public class CelAnimationEvent
    {
        [SerializeField]
        private bool _sendEvent;

        [SerializeField]
        private string _eventName;

        public void Invoke(GameObject go)
        {
            if (_sendEvent && !string.IsNullOrEmpty(_eventName))
            {
                go.SendMessage(_eventName);
            }
        }
    }
}
