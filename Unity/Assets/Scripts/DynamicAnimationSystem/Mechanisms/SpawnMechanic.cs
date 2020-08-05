using System.Collections;
using UnityEngine;

namespace DynamicAnimationSystem.Mechanisms
{
    public class SpawnMechanic : BaseAnimationMechanic
    {
        private Vector3 _offset;
        private float _rotation;
        private float _duration;
        private float _elapsedTime;
        
        public SpawnMechanic(DynamicAnimation dynamicAnimation) : base(dynamicAnimation) { }
        
        public override void GetParameterValues()
        {
            //Get the offset value.
            _offset = AnimationSequence.GetVector3(0);
            
            //Get the rotation value.
            _rotation =  AnimationSequence.GetFloat(1);
            
            //Get the completion time value.
            _duration = AnimationSequence.GetFloat(2);
        }
        
        public override IEnumerator PlaySequence()
        {
            DynamicAnimation.SpawnMansfeld(_offset, new Vector3(0, _rotation, 0));
            MansfeldCharacter = DynamicAnimation.MansfeldCharacter;
            
            _elapsedTime = 0;
            MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
            
            yield return new WaitUntil(() =>
            {
                _elapsedTime += Time.deltaTime;
                return _elapsedTime >= _duration;
            });
            
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override IEnumerator RestartSequence()
        {
            _elapsedTime = 0;
            DynamicAnimation.MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
            
            yield return new WaitUntil(() =>
            {
                _elapsedTime += Time.deltaTime;
                return _elapsedTime >= _duration;
            });
            
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override void FastForwardSequence()
        {
            DynamicAnimation.OnAnimationFinishedPlaying();
        }
    }
}