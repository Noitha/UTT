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

public class CameraPermission : UIScreen
{
#region Variables

    public Button permissionButton;
    public TextMeshProUGUI cameraPermissionTMP;
    public Button skipButton;
    public UIScreen previousScreen;

#endregion

#region Main Methods

    void Start()
    {
        permissionButton.onClick.AddListener(GrantCameraPermission);
        skipButton.onClick.AddListener(SkipCameraScreen);
            
    #if UNITY_ANDROID
        cameraPermissionTMP.text = Permission.HasUserAuthorizedPermission(Permission.Camera)? "Permission Granted": "Grant Permission";
    #elif UNITY_IOS
            cameraPermissionTMP.text = Application.HasUserAuthorization(UserAuthorization.WebCam)? "Permission Granted": "Grant Permission";
    #endif
            
    }

#endregion

#region Helper Methods

    private void GrantCameraPermission()
    {
    #if UNITY_ANDROID
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            SkipCameraScreen();
        }
        else
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
    #elif UNITY_IOS
                if(Application.HasUserAuthorization(UserAuthorization.WebCam)){
                    SkipCameraScreen();
                }else{
                    Application.RequestuserAuthorization(UserAuthorization.WebCam);
                }
    #elif UNITY_EDITOR
                SkipCameraScreen();
    #endif
    }

    private void OnApplicationFocus(bool hasFocus)
    {
    #if UNITY_ANDROID
        cameraPermissionTMP.text = Permission.HasUserAuthorizedPermission(Permission.Camera)? "Permission Granted": "Grant Permission";
    #elif UNITY_IOS
            cameraPermissionTMP.text = Application.HasUserAuthorization(UserAuthorization.WebCam)? "Permission Granted": "Grant Permission";
    #endif
    }

    private void SkipCameraScreen()
    {
        CloseScreen();
        previousScreen.OpenScreen();
    }

    public void SetPreviousScreen(UIScreen previousScreen)
    {
        this.previousScreen = previousScreen;
    }

#endregion
}
