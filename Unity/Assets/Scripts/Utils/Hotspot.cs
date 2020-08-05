public class Hotspot
{
#region Varbiables
    
    public bool unlocked;
    public float latitude, longitude;
    public string hotspotName;
    
    public string[] hotspotTextForLockedButton;
    public string[] hotspotTextForUnlockedButton;
    
    public string[] hotspotTextForLockedScreen;
    public string[] hotspotTextForUnlockedScreen;
    
    public string[] hotspotTextMansfeldDialog;

    public double distance;

#endregion
    
    

    public Hotspot(HotspotJson hotspotJson)
    {
        this.unlocked = false;
        this.latitude = hotspotJson.latitude;
        this.longitude = hotspotJson.longitude;
        this.hotspotName = hotspotJson.hotspotName;
        hotspotTextForLockedButton = hotspotJson.hotspotTextForLockedButton.Split('\n');;
        hotspotTextForUnlockedButton = hotspotJson.hotspotTextForUnlockedButton.Split('\n');;
        hotspotTextForLockedScreen = hotspotJson.hotspotTextForLockedScreen.Split('\n');;
        hotspotTextForUnlockedScreen = hotspotJson.hotspotTextForUnlockedScreen.Split('\n');;
        hotspotTextMansfeldDialog = hotspotJson.hotspotTextMansfeldDialog.Split('\n');
    }

    public void CalculateDistance(float latitude, float longitude)
    {
        double R = 6371e3; // metres
        double radians1 = latitude * System.Math.PI/180;
        double radians2 = this.latitude * System.Math.PI/180;
        double deltaRadians = (this.latitude-latitude) * System.Math.PI/180;
        double deltaLongitude = (this.longitude-longitude) * System.Math.PI/180;

        double a = System.Math.Sin(deltaRadians/2) * System.Math.Sin(deltaRadians/2) +
                   System.Math.Cos(radians1) * System.Math.Cos(radians2) *
                   System.Math.Sin(deltaLongitude/2) * System.Math.Sin(deltaLongitude/2);
        double c = 2 * System.Math.Atan2(System.Math.Sqrt(a), System.Math.Sqrt(1-a));

        distance = R * c; //result in meters
    }
}
