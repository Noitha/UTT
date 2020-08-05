using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UrbanTimeTravel.UI
{
    public class UIGroup : MonoBehaviour
    {
        #region Variables
        Component[] m_Screens = new Component[0];
        UIScreen m_PreviousScreen;
        UIScreen m_CurrentScreen;

        [SerializeField]
        UIScreen m_StartScreen;
        [SerializeField]
        float m_StartDelay = 0;


        [Header("Screen Switch Event")]
        [SerializeField]
        UnityEvent onScreenSwitch = new UnityEvent();
        #endregion

        #region Main Methods 

        private void Awake()
        {
            m_Screens = GetComponentsInChildren<UIScreen>(true);
            InitializeScreens();
        }
        // Start is called before the first frame update
        void Start()
        {
            Invoke("CallStartScreen", m_StartDelay);
        }
        #endregion

        #region Helper Methods
        public void CallStartScreen(){
            if (m_StartScreen)
            {
                SwitchScreen(m_StartScreen);
            }
        }


        public void SwitchScreen(UIScreen newScreen)
        {
            if (!newScreen)
                return;

            if (m_CurrentScreen)
            {
                m_CurrentScreen.CloseScreen();
                m_PreviousScreen = m_CurrentScreen;
            }

            m_CurrentScreen = newScreen;
            m_CurrentScreen.gameObject.SetActive(true);
            m_CurrentScreen.OpenScreen();

            if(onScreenSwitch != null)
            {
                onScreenSwitch.Invoke();
            }
        }

        public void BackToPreviousScreen()
        {
            if (!m_PreviousScreen)
            {
                CloseAllScreens();
                return;
            }

            SwitchScreen(m_PreviousScreen);
        }

        public void CloseAllScreens()
        {
            if (m_CurrentScreen)
            {
                m_CurrentScreen.CloseScreen();
                m_CurrentScreen = null;

            }

            m_PreviousScreen = null;
        }
        

        private void InitializeScreens()
        {
            foreach (var screen in m_Screens)
            {
                screen.gameObject.SetActive(true);
            }
        }
        #endregion
    }
}
