using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UrbanTimeTravel.UI;

#if UNITY_ANDROID
    using UnityEngine.Android;
#elif UNITY_IOS
    using UnityEngine.IOS;
#endif

public class LocationPermission : UIScreen
{

    #region Variables
    
    public Button permissionButton;
    public TextMeshProUGUI locationPermissionTMP;
    public Button skipButton;

    #endregion

    #region Main Methods

    void Start()
    {
        permissionButton.onClick.AddListener(GrantLocationPermission);
        skipButton.onClick.AddListener(SkipLocationScreen);
        
        permissionButton.onClick.AddListener(GrantLocationPermission);
        skipButton.onClick.AddListener(SkipLocationScreen);
        
        #if UNITY_ANDROID
            locationPermissionTMP.text = Permission.HasUserAuthorizedPermission(Permission.FineLocation)? "Permission Granted": "Grant Permission";
        #elif UNITY_IOS
                
        #endif
    }

    #endregion

    #region Helper Methods

    private void GrantLocationPermission()
    {
        #if UNITY_ANDROID
            if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                SkipLocationScreen();
            }
            else
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                LocationManager.instance.OnLocationPermissionAgree();
            }
        #elif UNITY_IOS
                
        #elif UNITY_EDITOR
            SkipLocationScreen();
        #endif
    }
            
    private void OnApplicationFocus(bool hasFocus)
    {
        #if UNITY_ANDROID
                locationPermissionTMP.text = Permission.HasUserAuthorizedPermission(Permission.FineLocation)? "Permission Granted": "Grant Permission";
        #elif UNITY_IOS
                
        #endif
    }

    private void SkipLocationScreen()
    {
        CloseScreen();
    }

#endregion
}