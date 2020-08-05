using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.UI;
using UrbanTimeTravel.UI;

public class HotspotButtonManager : UIScreen
{
#region Variables

    public GameObject hotspotButtonPrefab;
    public List<Button> hotspotButtonList;
    public GameObject informationPanel;
    public GameObject ButtonContainer;
    private HotspotsManager hotspotsManager;
    
#endregion

#region Main Methods

    private void Start()
    {
        hotspotsManager = HotspotsManager.GetInstance();

        float buttonHeight = hotspotButtonPrefab.GetComponent<RectTransform>().rect.height;
        ButtonContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(720,hotspotsManager.hotspots.Count*buttonHeight);
        
        for (int index = 0; index < hotspotsManager.hotspots.Count; index++)
        {
            Button newButton = Instantiate(hotspotButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Button>();
            hotspotButtonList.Add(newButton);
            newButton.transform.SetParent(ButtonContainer.transform);
            newButton.transform.localPosition = new Vector3(0,-index * buttonHeight,0);
            newButton.transform.localScale = Vector3.one;
            var detailButton = newButton.GetComponent<HotspotDetailButton>();
            detailButton.informationPanel = informationPanel;
        }
    }

#endregion

#region Helper Methods

    public void StartLocationUpdates()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            InvokeRepeating(nameof(UpdateButtons),.2f,10f);
        }
    }

    public void StopLocationUpdates()
    {
        if (Input.location.status == LocationServiceStatus.Running)
        {
            CancelInvoke(nameof(UpdateButtons));
        }
    }

    public void UpdateButtons()
    {
            if(Input.location.status == LocationServiceStatus.Running)
                hotspotsManager.OrderHotspotsByDistance();
            
            for (int index = 0; index < hotspotButtonList.Count; index++)
            {
                HotspotDetailButton tmp_hotspotDetailButton = hotspotButtonList[index].GetComponent<HotspotDetailButton>();
                tmp_hotspotDetailButton.hotspot = hotspotsManager.hotspots[index];
                tmp_hotspotDetailButton.UpdateInformationDisplayedOnButton();
            }
    }

#endregion
}
