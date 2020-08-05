using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UrbanTimeTravel.Utils
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class AppVersion : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            string vString = "App Version : " + Application.version;
            GetComponent<TextMeshProUGUI>().text = vString;
        }
    }
}

