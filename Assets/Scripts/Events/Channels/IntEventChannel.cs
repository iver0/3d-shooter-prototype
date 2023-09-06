using System;
using UnityEngine;

namespace ShooterPrototype.Events.Channels
{
    [CreateAssetMenu(fileName = "IntEvent_Channel", menuName = "Events/Int Event Channel")]
    public class IntEventChannel : ScriptableObject
    {
        public event Action<int> OnEventRaised;

        public void RaiseEvent(int arg)
        {
            OnEventRaised?.Invoke(arg);
        }
    }
}
