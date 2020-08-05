using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UrbanTimeTravel.UI
{
    public class UTTImageView : MonoBehaviour
    {
        [SerializeField]
        Image imageView;

        public void SwitchImage(Sprite img)
        {
            imageView.sprite = img;
        }
    }
}

