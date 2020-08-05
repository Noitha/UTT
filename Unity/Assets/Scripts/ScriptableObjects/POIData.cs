using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace UrbanTimeTravel.UI
{
   public enum mediaType {
        IMAGE,
        VIDEO
   }

    [CreateAssetMenu(menuName = "UTT/POIData")]
    public class POIData : ScriptableObject
    {
        public string titleText;
        public string bodyText;
        public Sprite imageView;
        public mediaType mediaType;
        public VideoClip videoClip;
        public Sprite Image;
    }
}