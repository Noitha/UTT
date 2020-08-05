using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public RawImage mapImage;
    public Image userLocationImage;
    public TextMeshProUGUI userLocationText;

    public Button changeMapButton;
    
    
    
    public List<MapData> maps = new List<MapData>();
    private int _currentMap;
    
    private void Awake()
    {
        _currentMap = 0;
        
        changeMapButton.onClick.AddListener(delegate
        {
            if (_currentMap + 1 >= maps.Count)
            {
                _currentMap = -1;
            }
            
            mapImage.texture = maps[++_currentMap].map;
            UpdateUserLocationOnMap();
        });
        
        
        
    }
    
    /// <summary>
    /// Activate the map location when entering the menu.
    /// </summary>
    public void Activate()
    {
        Input.location.Start();
        InvokeRepeating(nameof(UpdateUserLocationOnMap), 1, 1);
    }

    /// <summary>
    /// Deactivate the map location when leaving the menu.
    /// </summary>
    public void Deactivate()
    {
        Input.location.Stop();
        CancelInvoke(nameof(UpdateUserLocationOnMap));
    }
    
    /// <summary>
    /// Update the image on the map.
    /// </summary>
    private void UpdateUserLocationOnMap()
    {
        //Return if location service does not run.
        if (Input.location.status != LocationServiceStatus.Running)
        {
            return;
        }

        //Get the latest location data from the user.
        var userLocation = Input.location.lastData;

        //Display the user location.
        userLocationText.text = "Lat: " + userLocation.latitude + "; Lon: " + userLocation.longitude;

        //Get current map data.
        var currentMapData = maps[_currentMap];

        //Calculate the percentage.
        var percentage = new Vector2
        (
            Mathf.InverseLerp(currentMapData.bottomLeft.y, currentMapData.topRight.y, userLocation.longitude),
            Mathf.InverseLerp(currentMapData.bottomLeft.x, currentMapData.topRight.x, userLocation.latitude)
        );

        if (Math.Abs(percentage.x) < .001f || Math.Abs(percentage.y) < .001f)
        {
            userLocationImage.gameObject.SetActive(false);
        }
        else
        {
            userLocationImage.gameObject.SetActive(true);
        }
        
        //Get the size of the map.
        var imageSize = mapImage.rectTransform.rect.size;
        
        //Multiply the percentage with the size of the image.
        userLocationImage.rectTransform.anchoredPosition = new Vector2(percentage.x * imageSize.x, percentage.y * imageSize.y);
    }
}