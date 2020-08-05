using DynamicAnimationSystem;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UrbanTimeTravel.UI;

namespace UI
{
    public class InteractiveMenu : UIScreen
    {
        public ARTrackedImageManager arTrackedImageManager;
        public GameObject prefab;
        public Hotspot hotspot;

        public DynamicAnimation dynamicAnimation;

        //Story about current tracked hotspot
        public Button storyButton;
        public TextMeshProUGUI storyText;

        public int currentDialogIndex = 0;

        private void Start()
        {
            storyButton.onClick.AddListener(_NextStoryPiece);
        }

        private void OnEnable()
        {
            arTrackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
        }

        private void OnDisable()
        {

            arTrackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
        }

        private void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs trackedImageEventArgs)
        {
            foreach (var trackedImage in trackedImageEventArgs.added)
            {
                hotspot = HotspotsManager.GetInstance().UnlockHotspot(trackedImage.referenceImage.name);
                storyButton.gameObject.SetActive(true);
                storyText.text = hotspot.hotspotTextMansfeldDialog[currentDialogIndex];
                if (hotspot.unlocked == false)
                    hotspot.unlocked = true;

            }

            foreach (var trackedImage in trackedImageEventArgs.updated)
            {
                if (dynamicAnimation.MansfeldCharacter == null)
                {
                    Debug.Log(trackedImage.transform.position);
                    dynamicAnimation.Tracked(trackedImage.transform.position);
                }
            }
        }

        private void _NextStoryPiece()
        {
            currentDialogIndex = ++currentDialogIndex % hotspot.hotspotTextMansfeldDialog.Length;
            storyText.text = hotspot.hotspotTextMansfeldDialog[currentDialogIndex];
        }

        public void CleanUp()
        {
            hotspot = null;
            storyButton.gameObject.SetActive(false);
            storyText.text = "";
        }
    }
}
