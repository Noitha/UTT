using System.Linq;
using TMPro;
using UnityEngine;

public class HotspotDetailButton : MonoBehaviour
{
#region Variables

    public Hotspot hotspot;
    public TextMeshProUGUI buttonText;
    public GameObject informationPanel;
    private TextMeshProUGUI informationText;

    #endregion

#region Main Methods

    void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

#endregion

#region Helper Methods

    public void UpdateInformationDisplayedOnButton()
    {
        string replacementText = "";
        string[] buttonTextToBeUsed;

        if (Input.location.status == LocationServiceStatus.Running)
        {
            replacementText = hotspot.distance + " meters\n";
        }
        
        if (hotspot.unlocked == false)
        {
            buttonText.color = Color.red;
            buttonTextToBeUsed = hotspot.hotspotTextForLockedButton;
        }
        else
        {
            buttonText.color = Color.green;
            buttonTextToBeUsed = hotspot.hotspotTextForUnlockedButton;
        }

        replacementText = buttonTextToBeUsed.Aggregate(replacementText, (current, text) => current + (text + "\n"));

        buttonText.text = replacementText;
    }

    public void ShowInformationPanel()
    {
        informationText = informationPanel.GetComponentInChildren<TextMeshProUGUI>();
        if (hotspot.unlocked)
        {
            string unlockedText = "";
            foreach (string s in hotspot.hotspotTextForUnlockedScreen)
            {
                unlockedText = s + "\n";
            }
            informationText.text = unlockedText;
        }else{
            string lockedText = "";
            foreach (string s in hotspot.hotspotTextForLockedScreen)
            {
                lockedText = s + "\n";
            }
            informationText.text = lockedText;
        }
        informationPanel.SetActive(true);
    }
    
#endregion
}
