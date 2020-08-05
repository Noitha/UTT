[System.Serializable]
public class HotspotJson
{
    public float latitude, longitude;
    public string hotspotName;
    
    public string hotspotTextForLockedButton;
    public string hotspotTextForUnlockedButton;
    
    public string hotspotTextForLockedScreen;
    public string hotspotTextForUnlockedScreen;
    
    public string hotspotTextMansfeldDialog;
}

[System.Serializable]
public class HotspotsJson
{
    public HotspotJson[] hotspots;
}