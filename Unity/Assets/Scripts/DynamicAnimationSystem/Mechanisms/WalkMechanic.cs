using System.Collections;
using UnityEngine;

namespace DynamicAnimationSystem.Mechanisms
{
    public class WalkMechanic : BaseAnimationMechanic
    {
        private float _distance;
        private float _duration;
        private float _elapsedTime;
        private Vector3 _oldPosition;
        
        public WalkMechanic(DynamicAnimation dynamicAnimation) : base(dynamicAnimation) { }

        public override void GetParameterValues()
        {
            //Get the y-rotation value and convert it into a float.
            _distance = AnimationSequence.GetFloat(0);

            //Get the completion time.
            _duration = AnimationSequence.GetFloat(1);

            //Set the elapsed time to 0.
            _elapsedTime = 0f;
        }

        public override IEnumerator PlaySequence()
        {
            _oldPosition = MansfeldCharacter.GetPosition();

            var startingPosition = MansfeldCharacter.GetPosition();

            MansfeldCharacter.SetAnimationFloat(AnimationSequence.animationName, 1);
            
            yield return new WaitUntil(() =>
            {
                //Return if elapsed time has reached its completion time.
                if (_elapsedTime >= _duration)
                {
                    return true;
                }
                
                //Calculate the distance traveled by the elapsed time.
                var newDistance = Mathf.InverseLerp(0, _duration, _elapsedTime) * _distance;
                
                //Calculate the position from the starting position and the forward direction multiplied by the distance.
                var newPosition = startingPosition + MansfeldCharacter.GetForwardVector() * newDistance;
                
                //Set the new position.
                MansfeldCharacter.SetPosition(newPosition);
                
                //Increment the elapsed time.
                _elapsedTime += Time.deltaTime;
                
                return false;
            });
            
            MansfeldCharacter.SetAnimationFloat(AnimationSequence.animationName, 0);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override IEnumerator RestartSequence()
        {
            MansfeldCharacter.SetPosition(_oldPosition);
            _elapsedTime = 0f;
            
            var startingPosition = MansfeldCharacter.GetPosition();

            MansfeldCharacter.SetAnimationFloat(AnimationSequence.animationName, 1);
            
            yield return new WaitUntil(() =>
            {
                //Return if elapsed time has reached its completion time.
                if (_elapsedTime >= _duration)
                {
                    return true;
                }
                
                //Calculate the distance traveled by the elapsed time.
                var newDistance = Mathf.InverseLerp(0, _duration, _elapsedTime) * _distance;
                
                //Calculate the position from the starting position and the forward direction multiplied by the distance.
                var newPosition = startingPosition + MansfeldCharacter.GetForwardVector() * newDistance;
                
                //Set the new position.
                MansfeldCharacter.SetPosition(newPosition);
                
                //Increment the elapsed time.
                _elapsedTime += Time.deltaTime;
                
                return false;
            });
            
            MansfeldCharacter.SetAnimationFloat(AnimationSequence.animationName, 0);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override void FastForwardSequence()
        {
            MansfeldCharacter.SetPosition(_oldPosition + MansfeldCharacter.transform.forward * _distance);
            MansfeldCharacter.SetAnimationFloat(AnimationSequence.animationName, 0);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }
    }
}