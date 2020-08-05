using System.Collections;

namespace DynamicAnimationSystem
{
    public abstract class BaseAnimationMechanic : IAnimationMechanic
    {
        protected readonly DynamicAnimation DynamicAnimation;
        protected readonly AnimationSequence AnimationSequence;
        protected MansfeldCharacter MansfeldCharacter;
    
        protected BaseAnimationMechanic(DynamicAnimation dynamicAnimation)
        {
            DynamicAnimation = dynamicAnimation;
            AnimationSequence = DynamicAnimation.GetCurrentAnimationSequence();
            MansfeldCharacter = DynamicAnimation.MansfeldCharacter;
        }
    
        public abstract void GetParameterValues();

        public abstract IEnumerator PlaySequence();

        public abstract IEnumerator RestartSequence();

        public abstract void FastForwardSequence();
    }
}