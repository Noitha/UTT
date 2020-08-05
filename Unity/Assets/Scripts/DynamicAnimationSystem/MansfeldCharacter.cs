using UnityEngine;

namespace DynamicAnimationSystem
{
    public class MansfeldCharacter : MonoBehaviour
    {
        private Animator _animator;
        private string _lastAnimation;

        public GameObject chair;
        public GameObject bed;

        public Camera frontCamera;
        public Camera backCamera;
        
        
        public void Initialize()
        {
            //Get the animator component.
            _lastAnimation = "";
            _animator = GetComponent<Animator>();
            frontCamera.enabled = true;
            backCamera.enabled = false;
        }

        public void SwitchCamera()
        {
            if (frontCamera.isActiveAndEnabled)
            {
                backCamera.enabled = true;
                frontCamera.enabled = false;
                return;
            }

            frontCamera.enabled = true;
            backCamera.enabled = false;
        }
        
        public Vector3 GetPosition()
        {
            return transform.position;
        }
        public Vector3 GetRotation()
        {
            return transform.eulerAngles;
        }
        public void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
        public void SetRotation(Vector3 newRotation)
        {
            transform.eulerAngles = newRotation;
        }
        public Vector3 GetForwardVector()
        {
            return transform.forward;
        }
        
        public void SetAnimationBool(string parameterName)
        {
            _animator.SetBool(parameterName, true);
            _lastAnimation = parameterName;
        }
        public void SetAnimationFloat(string parameterName, float value)
        {
            _animator.SetFloat(parameterName, value);
        }
        public void ExitCurrentAnimation()
        {
            if (_lastAnimation != "")
            {
                _animator.SetBool(_lastAnimation, false);
            }
        }
    }
}