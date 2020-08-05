using UnityEngine;
using UrbanTimeTravel.UI;

public class FirstStart : MonoBehaviour
{
    #region Variables

    public UIScreen menuScreen, onboardingScreen;
    public GameObject navigation_persistant;

    #endregion

    #region Main Methods

    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstStart"))
        {
            PlayerPrefs.SetString("FirstStart","true");
            PlayerPrefs.Save();
            menuScreen.CloseScreen();
            onboardingScreen.OpenScreen();
        }
        else
        {
            navigation_persistant.SetActive(true);
        }
    }
    
    #endregion
}
