using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace UrbanTimeTravel.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Animator))]
    public class UIScreen : MonoBehaviour
    {
        #region Variables

        [Header("Screen Events")]
        public UnityEvent onScreenOpen = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();

        Animator animator;
        #endregion

        #region Helper Methods
        public virtual void OpenScreen()
        {
            
            if (onScreenOpen != null)
            {
                onScreenOpen.Invoke();
            }

            HandleAnimator("open");
        }

        public virtual void CloseScreen()
        {

            if (onScreenClose != null)
            {
                onScreenClose.Invoke();
            }

            HandleAnimator("close");
        }

        private void HandleAnimator(string triggerName)
        {
            animator = gameObject.GetComponent<Animator>();

            if (animator)
            {
                animator.SetTrigger(triggerName);
            }
        }
        #endregion

    }
}
