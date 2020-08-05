using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

namespace UrbanTimeTravel.AV {
    public class UTTVideoPlayer : MonoBehaviour
    {
        bool paused = false;
        [SerializeField]
        VideoPlayer player;
       

        public void SwitchClip(VideoClip video)
        {
            player.clip = video;
        }

        public void TogglePause()
        {
            if (!paused)
            {
                player.Pause();
                paused = true;
            }
            else
            {
                player.Play();
                paused = false;
            }
        }
    }
}