using System.Collections;
using UnityEngine;

namespace DynamicAnimationSystem.Mechanisms
{
    public class TalkMechanic : BaseAnimationMechanic
    {
        private float _duration;
        
        public TalkMechanic(DynamicAnimation dynamicAnimation) : base(dynamicAnimation) { }

        public override void GetParameterValues()
        {
            _duration = AnimationSequence.GetFloat(0);
        }

        public override IEnumerator PlaySequence()
        {
            MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
            yield return new WaitForSeconds(_duration);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override IEnumerator RestartSequence()
        {
            MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
            yield return new WaitForSeconds(_duration);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override void FastForwardSequence()
        {
            DynamicAnimation.OnAnimationFinishedPlaying();
        }
    }
}