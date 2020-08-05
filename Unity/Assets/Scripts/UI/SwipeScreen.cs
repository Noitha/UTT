using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UrbanTimeTravel.UI
{
    [RequireComponent(typeof(UIGroup))]
    public class SwipeScreen : MonoBehaviour
    {
        #region Variables
        [SerializeField] GameObject paginationContainer;
        [SerializeField] Sprite activePageSprite;
        [SerializeField] Sprite inactivePageSprite;

        UIGroup mainGroup;
        UIScreen[] screens = new UIScreen[0];

        float screenWidth;
        int currentScreen = 0;

        Vector2 touchStart;
        Vector2 touchPos;
        #endregion

        #region Main Methods
        private void Awake()
        {
            screens = GetComponentsInChildren<UIScreen>(true);
            mainGroup = GetComponent<UIGroup>();

            screenWidth = (float)Screen.width / 2.0f;
        }

        private void Update()
        {
            CheckTouchPhase();
        }
        #endregion

        #region Helper Methods
        private void CheckTouchPhase()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchStart = touch.position;
                        break;
                    case TouchPhase.Moved:
                        touchPos = touch.position;
                        break;
                    case TouchPhase.Ended:
                        touchPos = touch.position;
                        EvalSwipe();
                        break;
                    default:
                        break;
                }
            }
        }

        private void EvalSwipe()
        {
            Vector2 delta = touchStart - touchPos;
            if(Mathf.Abs(delta.x) > screenWidth * 0.15f)
            {
                if(delta.x < 0)
                {
                    SwipeLeft();
                }
                else
                {
                    SwipeRight();
                }
            }
        }

        private void SwipeLeft()
        {
            int nextScreen = Mathf.Max(currentScreen-1, 0);
            SwitchToScreen(nextScreen);

        }

        private void SwipeRight()
        {
            int nextScreen = Mathf.Min(currentScreen+1, screens.Length-1);
            SwitchToScreen(nextScreen);
        }

        private void SwitchToScreen(int nextScreen)
        {
            if (nextScreen == currentScreen)
                return;

            currentScreen = nextScreen;
            mainGroup.SwitchScreen(screens[nextScreen]);

            UpdateProgressDisplay();
        }

        private void UpdateProgressDisplay()
        {
            Image[] bullets = paginationContainer.GetComponentsInChildren<Image>();
            for(int i = 0; i < bullets.Length; i++)
            {
                if(i == currentScreen)
                {
                    bullets[i].sprite = activePageSprite;
                }
                else
                {
                    bullets[i].sprite = inactivePageSprite;
                }
            }
        }
        #endregion
    }
}

