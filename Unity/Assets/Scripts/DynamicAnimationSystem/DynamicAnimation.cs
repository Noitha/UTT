using System;
using System.Collections;
using DynamicAnimationSystem.Mechanisms;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace DynamicAnimationSystem
{
    public class DynamicAnimation : MonoBehaviour
    {
        #region Variables

        public bool testPC;
        public bool isWebBuild;

        [Header("Command Buttons")] public Button nextSequenceButton;
        public Button fastForwardSequenceButton;
        public Button restartSequenceButton;
        public Button pauseSequenceButton;
        public Button resumeSequenceButton;
        public Button changeCameraPerspective;

        [Header("Test Animation File")] 
        public TextAsset animationFile;
        
        [Header("Loaded Animation File From Server")]
        private TextAsset _loadedAnimationFile;

        [Header("Mansfeld Character Prefab")] public MansfeldCharacter mansfeldCharacterPrefab;

        //Current instance of the mansfeld character after being spawned.
        public MansfeldCharacter MansfeldCharacter { get; private set; }

        //Current hotspot animation, containing all the data. 
        private HotspotAnimation _currentHotspotAnimation;
        private int _currentSequenceIndex;
        private Coroutine _currentCoroutine;
        private Vector3 _trackedPosition;
        private int _lastPlayedSequenceIndex;
        private BaseAnimationMechanic _lastPlayedMechanic;
        
        #endregion

        #region Unity Methods

        private void Start()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
                WebGLInput.captureAllKeyboardInput = false;
#endif
            AddButtonEvents();

            if (isWebBuild)
            {
                ShowNextSequenceButton(false);
                ShowFastForwardButton(false);
                ShowRestartSequenceButton(false);
                ShowResumeSequenceButton(false);
                ShowPauseSequenceButton(false);
                return;
            }
            
            if (testPC)
            {
                StartCoroutine(OnTracked());
            }
        }

        #endregion
        
        #region Animation Commands

        /// <summary>
        /// PlaySequence the current animation sequence. 
        /// </summary>
        /// <param name="animationPlayMode"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void Play(AnimationPlayMode animationPlayMode)
        {
            ConfigurePlayButtons();
            
            var currentAnimationSequence = GetCurrentAnimationSequence();

            if (currentAnimationSequence == null)
            {
                return;
            }
            
            if (_currentSequenceIndex != _lastPlayedSequenceIndex || _lastPlayedMechanic == null)
            {
                BaseAnimationMechanic animationMechanic;
                
                switch (currentAnimationSequence.GetAnimationMechanic())
                {
                    case AnimationMechanic.Spawn: animationMechanic = new SpawnMechanic(this); break;
                    case AnimationMechanic.Rotate: animationMechanic = new RotateMechanic(this); break;
                    case AnimationMechanic.Walk: animationMechanic = new WalkMechanic(this); break;
                    case AnimationMechanic.Talk: animationMechanic = new TalkMechanic(this); break;
                    case AnimationMechanic.Sit: animationMechanic = new SitMechanic(this); break;
                    case AnimationMechanic.Lay: animationMechanic = new LayMechanic(this); break;
                    default: throw new ArgumentOutOfRangeException();
                }

                _lastPlayedMechanic = animationMechanic;
                _lastPlayedMechanic.GetParameterValues();
                _lastPlayedSequenceIndex = _currentSequenceIndex;
            }
            
            switch (animationPlayMode)
            {
                case AnimationPlayMode.Normal: 
                    _currentCoroutine = StartCoroutine(_lastPlayedMechanic.PlaySequence()); 
                    break;
                
                case AnimationPlayMode.Restart: 
                    _currentCoroutine = StartCoroutine(_lastPlayedMechanic.RestartSequence()); 
                    break;
                
                case AnimationPlayMode.FastForward: 
                    _lastPlayedMechanic.FastForwardSequence(); 
                    break;
                
                default: throw new ArgumentOutOfRangeException(nameof(animationPlayMode), animationPlayMode, null);
            }
        }

        private void ChangeCameraPerspective()
        {
            if (MansfeldCharacter != null)
            {
                MansfeldCharacter.SwitchCamera();
            }
        }
        
        
        /// <summary>
        /// Pause the current sequence.
        /// </summary>
        private void Pause()
        {
            Time.timeScale = 0;
            ShowPauseSequenceButton(false);
            ShowResumeSequenceButton(true);
        }

        /// <summary>
        /// Resume the current sequence.
        /// </summary>
        private void Resume()
        {
            Time.timeScale = 1;
            ShowResumeSequenceButton(false);
            ShowPauseSequenceButton(true);
        }

        /// <summary>
        /// Fast forward the current sequence.
        /// </summary>
        private void FastForwardCurrentSequence()
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }

            Play(AnimationPlayMode.FastForward);
        }

        /// <summary>
        /// RestartSequence the current sequence. 
        /// </summary>
        private void RestartCurrentSequence()
        {
            Play(AnimationPlayMode.Restart);
        }

        /// <summary>
        /// Go to the next sequence.
        /// </summary>
        private void GoToNextSequence()
        {
            if (_currentSequenceIndex + 1 >= _currentHotspotAnimation.hotspotAnimations.Length)
            {
                //Debug.Log("All sequences have been played.");
                return;
            }
            
            _currentSequenceIndex++;

            Play(AnimationPlayMode.Normal);
        }

        #endregion

        #region Command Button Display

        private void ShowPauseSequenceButton(bool status)
        {
            pauseSequenceButton.interactable = status;
        }

    

        private void ShowResumeSequenceButton(bool status)
        {
            resumeSequenceButton.interactable = status;
        }

   

        private void ShowRestartSequenceButton(bool status)
        {
            restartSequenceButton.interactable = status;
        }
        

        private void ShowNextSequenceButton(bool status)
        {
            nextSequenceButton.interactable = status;
        }
        

        private void ShowFastForwardButton(bool status)
        {
            fastForwardSequenceButton.interactable = status;
        }

        private void ConfigurePlayButtons()
        {
            ShowRestartSequenceButton(false);
            ShowNextSequenceButton(false);
            ShowFastForwardButton(true);
            ShowPauseSequenceButton(true);
        }

        #endregion
        
        public void RemoveCharacter()
        {
            foreach (var mansfeld in FindObjectsOfType<MansfeldCharacter>())
            {
                Destroy(mansfeld.gameObject);
            }
            
            MansfeldCharacter = null;
        }
        public void Tracked(Vector3 position)
        {
            if (MansfeldCharacter != null)
            {
                Destroy(MansfeldCharacter.gameObject);
            }

            _trackedPosition = position;
            StartCoroutine(OnTracked());
        }

        /// <summary>
        /// Get fired once an image has been tracked.
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnTracked()
        {
            //Set the current sequence index to -1.
            _currentSequenceIndex = -1;
            _currentCoroutine = null;

            ShowNextSequenceButton(true);
            ShowFastForwardButton(false);
            ShowRestartSequenceButton(false);
            ShowPauseSequenceButton(false);
            ShowResumeSequenceButton(false);

            yield return StartCoroutine(LoadAnimationPropertiesFromServer());

            if (_loadedAnimationFile.text == "")
            {
                //Debug.Log("Warning, file is empty");
                _loadedAnimationFile = animationFile; //yield break;
            }

            _currentHotspotAnimation = JsonConvert.DeserializeObject<HotspotAnimation>(_loadedAnimationFile.text);

            GoToNextSequence();
        }

        /// <summary>
        /// Method that add events to the buttons.
        /// </summary>
        private void AddButtonEvents()
        {
            nextSequenceButton.onClick.AddListener(GoToNextSequence);
            fastForwardSequenceButton.onClick.AddListener(FastForwardCurrentSequence);
            restartSequenceButton.onClick.AddListener(RestartCurrentSequence);
            pauseSequenceButton.onClick.AddListener(Pause);
            resumeSequenceButton.onClick.AddListener(Resume);
            changeCameraPerspective.onClick.AddListener(ChangeCameraPerspective);
        }
        public void OnAnimationFinishedPlaying()
        {
            MansfeldCharacter.ExitCurrentAnimation();
            ShowEndOfSequenceCommands();
        }
        
        private void ShowEndOfSequenceCommands()
        {
            ShowFastForwardButton(false);
            ShowNextSequenceButton(true);
            ShowRestartSequenceButton(true);
        }
        
        public AnimationSequence GetCurrentAnimationSequence()
        {
            return _currentHotspotAnimation?.hotspotAnimations[_currentSequenceIndex];
        }
        
        public void SpawnMansfeld(Vector3 offset, Vector3 rotation)
        {
            MansfeldCharacter = Instantiate(mansfeldCharacterPrefab, _trackedPosition + offset, Quaternion.Euler(rotation));
            MansfeldCharacter.Initialize();
        }
        
        /// <summary>
        /// Load the corresponding animation file from the server.
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadAnimationPropertiesFromServer()
        {
            /*var form = new WWWForm();
            form.AddField("Hotspot", 1);

            var webRequest = UnityWebRequest.Post("", form);
            yield return webRequest.SendWebRequest();
            var content = webRequest.downloadHandler.text;*/

            _loadedAnimationFile = new TextAsset("");
            yield break;
        }

        public void WebJsonAnimationLoader(string animationJson)
        {
            if (_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            
            RemoveCharacter();
            _currentSequenceIndex = -1;
            _currentCoroutine = null;
            _currentHotspotAnimation = JsonConvert.DeserializeObject<HotspotAnimation>(animationJson);
            GoToNextSequence();
        }
    }
}