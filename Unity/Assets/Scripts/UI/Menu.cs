using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UrbanTimeTravel.UI
{

    public class Menu : MonoBehaviour
    {
        public GameObject buttons;
        private CanvasGroup buttonCanvas;

        private Animator buttonAnimator;
        private bool isOpen = false;

        // Start is called before the first frame update
        void Start()
        {
            buttonAnimator = buttons.GetComponent<Animator>();
            buttonCanvas = buttons.GetComponent<CanvasGroup>();
        }

        public void ToggleMenu()
        {
            if (isOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }

        public void OpenMenu()
        {
            if (isOpen)
                return;

            isOpen = true;
            buttonAnimator.SetTrigger("open");
        }

        public void CloseMenu()
        {
            if (!isOpen)
                return;

            buttonAnimator.SetTrigger("close");
            isOpen = false;
        }
    
    }

}