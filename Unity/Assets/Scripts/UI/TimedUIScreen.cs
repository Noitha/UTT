using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UrbanTimeTravel.UI
{
    public class TimedUIScreen : UIScreen
    {
        #region Variables
        [Header("Timed Screen Properties")]
        [SerializeField]
        float m_WaitTime = 2f;
        [SerializeField]
        UnityEvent onTimeComplete = new UnityEvent();

        private float startTime;
        Coroutine waitForTime;

        #endregion

        #region Helper Methods
        public override void OpenScreen()
        {
            base.OpenScreen();

            startTime = Time.time;
            waitForTime = StartCoroutine(WaitForTime());
        }

        public override void CloseScreen()
        {
            base.CloseScreen();

            StopCoroutine(waitForTime);
        }

        private IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(m_WaitTime);

            if(onTimeComplete != null)
            {
                onTimeComplete.Invoke();
            }
        }
        #endregion 

    }
}

