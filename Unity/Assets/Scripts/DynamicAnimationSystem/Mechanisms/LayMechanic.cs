using System.Collections;
using UnityEngine;

namespace DynamicAnimationSystem.Mechanisms
{
    public class LayMechanic : BaseAnimationMechanic
    {
        private float _duration;
        private float _elapsedTime;
        
        public LayMechanic(DynamicAnimation dynamicAnimation) : base(dynamicAnimation) { }

        public override void GetParameterValues()
        {
            _duration = AnimationSequence.GetFloat(0);
        }

        public override IEnumerator PlaySequence()
        {
            MansfeldCharacter.bed.SetActive(true);
            yield return new WaitUntil(() =>
            {
                //Return if elapsed time has reached its completion time.
                if (_elapsedTime >= _duration)
                {
                    return true;
                }
            
                MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
                return false;
            });
            MansfeldCharacter.bed.SetActive(false);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override IEnumerator RestartSequence()
        {
            _elapsedTime = 0f;
            MansfeldCharacter.bed.SetActive(true);
            yield return new WaitUntil(() =>
            {
                //Return if elapsed time has reached its completion time.
                if (_elapsedTime >= _duration)
                {
                    return true;
                }
            
                MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
                return false;
            });
            MansfeldCharacter.bed.SetActive(false);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override void FastForwardSequence()
        {
            MansfeldCharacter.bed.SetActive(false);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }
    }
}