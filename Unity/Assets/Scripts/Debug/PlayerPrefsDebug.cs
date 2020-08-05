using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsDebug : MonoBehaviour
{
    [Tooltip("Delete all Player prefs for testing purposes")]
    public bool reset;
    void Awake()
    {
        if(reset)
            PlayerPrefs.DeleteAll();
    }
}
