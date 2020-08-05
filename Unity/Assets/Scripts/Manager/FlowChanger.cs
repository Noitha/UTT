using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UrbanTimeTravel.Utils
{
    public class FlowChanger : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            PersistentObject._this.gameObject.GetComponent<UTTSceneManager>().FadeAndLoadScene(sceneName);
        }
    }
}
