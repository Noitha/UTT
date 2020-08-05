using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Manager
{
public class HotspotsManager : MonoBehaviour
{

#region Variables

    private static HotspotsManager instance;
    private float currentLongitude = 0, currentLatitude = 0;
    public List<Hotspot> hotspots = new List<Hotspot>();

#endregion

#region Main Methods

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadHotspotsFromJsonFile();
        }
        else
        {
            Destroy(this);
        }
    }
    
#endregion
    
#region Helper Methods

    public void OrderHotspotsByDistance()
    {
        foreach (Hotspot hotspot in hotspots)
        {
            hotspot.CalculateDistance(Input.location.lastData.latitude, Input.location.lastData.longitude);
        }
        hotspots = hotspots.OrderBy(Hotspot => Hotspot.distance).ToList();
    }

    public static HotspotsManager GetInstance()
    {
        return instance;
    }

    public Hotspot UnlockHotspot(string trackedImageName)
    {
        foreach (Hotspot hotspot in hotspots)
        {
            Debug.Log(hotspot.hotspotName + " - - - - " + trackedImageName);
            if (hotspot.hotspotName.Equals(trackedImageName))
            {
                return hotspot;
            }
        }
        return null;
    }

    private void LoadHotspotsFromJsonFile()
    {
        string jsonFile = (Resources.Load("hotspots") as TextAsset).ToString();
        
        HotspotsJson hotspotsJson = JsonUtility.FromJson<HotspotsJson>(jsonFile);

        foreach (HotspotJson hotspot in hotspotsJson.hotspots)
        {
            Hotspot hs = new Hotspot(hotspot);
            hotspots.Add(hs);
        }
    }
    
#endregion
}
}

