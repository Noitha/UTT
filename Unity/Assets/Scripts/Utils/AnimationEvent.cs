using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UrbanTimeTravel.Utils
{
    /// <summary>
    /// Makes Unity's unwieldy Animation Interface more useful
    /// </summary>
    public class AnimationEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent onFrameCalled = new UnityEvent();

        public void TriggerEvent()
        {
            if (onFrameCalled != null)
                onFrameCalled.Invoke();
        }
    }
}
