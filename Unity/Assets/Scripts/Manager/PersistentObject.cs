using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    public static PersistentObject _this;

    private void Awake()
    {
        if(_this == null)
        {
            DontDestroyOnLoad(gameObject);
            _this = this;
        }
        else if(_this != this)
        {
            Destroy(gameObject);
            Debug.Log("Cleaned up persistent objects");
        }
    }
}
