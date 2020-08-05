using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UrbanTimeTravel.AV;

// UNCOMMENT IF USING MULTI LANGUAGE
//using UrbanTimeTravel.Localization;

namespace UrbanTimeTravel.UI
{
    public class InfoPanel : MonoBehaviour
    {
        [Header("Input Items")]
        [SerializeField]
        TextMeshProUGUI TitleInput;

        [SerializeField]
        TextMeshProUGUI BodyInput;

        [SerializeField]
        Image LocationImage;

        [Header("UI Group")]
        [SerializeField]
        UIGroup MainUI;

        [SerializeField]
        UIScreen ImageScreen;

        [SerializeField]
        UIScreen VideoScreen;

        POIData currentPOI = null;

        public void UpdateInfoPanel(POIData poi)
        {
            //TitleInput.text = LanguageManager.instance.GetTranslation(poi.titleText);
            //BodyInput.text = LanguageManager.instance.GetTranslation(poi.bodyText);

            TitleInput.text = poi.titleText;
            BodyInput.text = poi.bodyText;
            LocationImage.sprite = poi.imageView;

            UIScreen screen = GetComponent<UIScreen>();
            MainUI.SwitchScreen(screen);
            currentPOI = poi;
        }

        public void PlayMedia()
        {
            if (currentPOI == null)
                return;

            switch (currentPOI.mediaType)
            {
                case mediaType.IMAGE:
                    UTTImageView i = ImageScreen.gameObject.GetComponent<UTTImageView>();
                    i.SwitchImage(currentPOI.imageView);
                    MainUI.SwitchScreen(ImageScreen);
                    break;
                case mediaType.VIDEO:
                    UTTVideoPlayer v = VideoScreen.gameObject.GetComponent<UTTVideoPlayer>();
                    v.SwitchClip(currentPOI.videoClip);
                    MainUI.SwitchScreen(VideoScreen);
                    break;
                default:
                    break;
            }
        }
    }
}