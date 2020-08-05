using System.Collections;
using UnityEngine;

namespace DynamicAnimationSystem.Mechanisms
{
    public class SitMechanic : BaseAnimationMechanic
    {
        private float _duration;
        
        public SitMechanic(DynamicAnimation dynamicAnimation) : base(dynamicAnimation) { }

        public override void GetParameterValues()
        {
            //Get the duration.
            _duration = AnimationSequence.GetFloat(0);
        }

        public override IEnumerator PlaySequence()
        {
            MansfeldCharacter.chair.SetActive(true);
            MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
            yield return new WaitForSeconds(_duration);
            MansfeldCharacter.chair.SetActive(false);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override IEnumerator RestartSequence()
        {
            MansfeldCharacter.chair.SetActive(true);
            MansfeldCharacter.SetAnimationBool(AnimationSequence.animationName);
            yield return new WaitForSeconds(_duration);
            MansfeldCharacter.chair.SetActive(false);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }

        public override void FastForwardSequence()
        {
            MansfeldCharacter.chair.SetActive(false);
            DynamicAnimation.OnAnimationFinishedPlaying();
        }
    }
}