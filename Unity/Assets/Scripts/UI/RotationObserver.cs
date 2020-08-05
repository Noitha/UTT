using UnityEngine;

namespace UrbanTimeTravel.MobileUtils
{
    public class RotationObserver : MonoBehaviour
    {
        DeviceOrientation m_PreviousOrientation;
        DeviceOrientation m_CurrentOrientation;

        //[SerializeField]
        Component[] m_ObservedImages = new Component[0];
        
        private void Start()
        {
            m_PreviousOrientation = Input.deviceOrientation;
            m_ObservedImages = GetComponentsInChildren<RotatorObject>(true);
        }

        private void Update()
        {
            CheckOrientationChange();
        }
        
        private void CheckOrientationChange()
        {
            m_CurrentOrientation = Input.deviceOrientation;

            if (!m_PreviousOrientation.Equals(m_CurrentOrientation))
            {
                foreach (var img in m_ObservedImages)
                {
                    RotatorObject rotator = (RotatorObject)img;
                    rotator.FlipScreen(m_PreviousOrientation, m_CurrentOrientation);
                }
            }

            m_PreviousOrientation = m_CurrentOrientation;
        }
    }
}