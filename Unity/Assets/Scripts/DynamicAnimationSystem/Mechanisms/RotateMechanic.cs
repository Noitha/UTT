using System.Collections;
using UnityEngine;

namespace DynamicAnimationSystem.Mechanisms
{
    public class RotateMechanic : BaseAnimationMechanic
    {
        private float _rotationAmount;
        private float _duration;
        private float _elapsedTime;
        private float _oldRotation;
        
        public RotateMechanic(DynamicAnimation dynamicAnimation) : base(dynamicAnimation) { }

        public override void GetParameterValues()
        {
            //Get the y-rotation value and convert it into a float.
            _rotationAmount = AnimationSequence.GetFloat(0);

            //Get the completion time.
            _duration = AnimationSequence.GetFloat(1);

            //Set the elapsed time to 0.
            _elapsedTime = 0f;
        }

        public override IEnumerator PlaySequence()
        {
            _oldRotation = MansfeldCharacter.GetRotation().y;

            //MansfeldCharacter.SetAnimationFloat(AnimationSequence.animationName, _rotationAmount > 0 ? 1 : -1);
            
            var yStartingRotation = MansfeldCharacter.GetRotation().y;
            
            yield return new WaitUntil(() =>
            {
                var percentage = Mathf.InverseLerp(0, _duration, _elapsedTime);
                var newRotation = percentage * _rotationAmount;
                MansfeldCharacter.SetRotation(new Vector3(0, yStartingRotation + newRotation, 0));
                _elapsedTime += Time.deltaTime;
                return _elapsedTime >= _duration;
            });

            //MansfeldCharacter.SetAnimationFloat(AnimationSequence.animationName, 0);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override IEnumerator RestartSequence()
        {
            MansfeldCharacter.SetRotation(new Vector3(0, _oldRotation, 0));
            _elapsedTime = 0f;
            
            var yStartingRotation = MansfeldCharacter.GetRotation().y;
            yield return new WaitUntil(() =>
            {
                var percentage = Mathf.InverseLerp(0, _duration, _elapsedTime);
                var newRotation = percentage * _rotationAmount;
                MansfeldCharacter.SetRotation(new Vector3(0, yStartingRotation + newRotation, 0));
                _elapsedTime += Time.deltaTime;
                return _elapsedTime >= _duration;
            });

            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override void FastForwardSequence()
        {
            MansfeldCharacter.SetRotation(new Vector3(0, _oldRotation, 0));
            MansfeldCharacter.SetRotation(new Vector3(0, MansfeldCharacter.GetRotation().y + _rotationAmount, 0));
            DynamicAnimation.OnAnimationFinishedPlaying();
        }
    }
}