using System.Collections;

namespace DynamicAnimationSystem
{
    public interface IAnimationMechanic
    {
        void GetParameterValues();
        IEnumerator PlaySequence();
        IEnumerator RestartSequence();
        void FastForwardSequence();
    }
}