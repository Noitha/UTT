using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

// UNCOMMENT IF USING MULTI LANGUAGE
//using UrbanTimeTravel.Localization;

namespace UrbanTimeTravel.Utils
{
    public class UTTSceneManager : MonoBehaviour
    {
        [SerializeField] private string m_startScene = "StartMenu";

        [SerializeField] private CanvasGroup m_faderCanvasGroup;
        [SerializeField] private float m_fadeDuration = 1.0f;

        public event Action BeforeSceneUnload;
        public event Action AfterSceneUnload;

        private bool isFading;

        private IEnumerator Start()
        {
            isFading = false;
            yield return StartCoroutine(Fade(0f));
        }


        public void FadeAndLoadScene(string sceneName)
        {
            if (!isFading)
            {
                StartCoroutine(FadeAndSwitchScenes(sceneName));
            }
            else
                StartCoroutine(FadeFinish(sceneName));
        }

        private IEnumerator FadeAndSwitchScenes(string sceneName)
        {
            // fade to black and wait for that to finsih
            yield return StartCoroutine(Fade(1f));

            // call Scene Events
            BeforeSceneUnload?.Invoke();

            int oldScene = SceneManager.GetActiveScene().buildIndex;

            yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
            //unload current scene and wait for that to finish
            yield return SceneManager.UnloadSceneAsync(oldScene);

            //if this event has any subscribers, call it.
            AfterSceneUnload?.Invoke();

            yield return StartCoroutine(Fade(0f)); //fade to new scene after scene is loaded

        }

        private IEnumerator FadeFinish(string sceneName)
        {
            //wait until previous Fade is done
            yield return new WaitUntil(() => isFading == false);
            yield return StartCoroutine(FadeAndSwitchScenes(sceneName));
        }

        private IEnumerator LoadSceneAndSetActive(string sceneName)
        {
            //load over several frames
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            Scene freshScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1); // start at index 0

            //mark fresh scene as active, making it the next in the queue to be unloaded
            SceneManager.SetActiveScene(freshScene);
            
            // UNCOMMENT IF USING MULTI LANGUAGE
            //LanguageManager.instance.InitScene();

        }

        private IEnumerator Fade(float finalAlpha)
        {
            isFading = true;
            m_faderCanvasGroup.blocksRaycasts = true; // no clicking allowed while fading

            float fadeSpeed = Mathf.Abs(m_faderCanvasGroup.alpha - finalAlpha) / m_fadeDuration;

            while (!Mathf.Approximately(m_faderCanvasGroup.alpha, finalAlpha))
            {
                m_faderCanvasGroup.alpha = Mathf.MoveTowards(m_faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);
                yield return null; // wait for 1 frame
            }

            isFading = false;
            m_faderCanvasGroup.blocksRaycasts = false; // unlock again
        }
    }
}