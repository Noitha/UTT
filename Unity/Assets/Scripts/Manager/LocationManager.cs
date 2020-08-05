using UnityEngine;
using UnityEngine.Android;

public class LocationManager : MonoBehaviour
{
    public static LocationManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Input.location.Start();
        }
    }

    public void OnLocationPermissionAgree()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Input.location.Start();
        }
    }
}
