using ShooterPrototype.Events.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace ShooterPrototype.Events
{
    public class IntEventListener : MonoBehaviour
    {
        [SerializeField] private IntEventChannel channel;
        public UnityEvent<int> onEventRaised;

        private void OnEnable()
        {
            if (channel != null)
                channel.OnEventRaised += Respond;
        }

        public void OnDisable()
        {
            if (channel != null)
                channel.OnEventRaised -= Respond;
        }

        private void Respond(int arg)
        {
            onEventRaised?.Invoke(arg);
        }
    }
}
