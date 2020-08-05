using UnityEngine;

[CreateAssetMenu(menuName = "Map/ Create new map", fileName = "New map")]
public class MapData : ScriptableObject
{
    public Texture map;
    public Vector2 bottomLeft;
    public Vector2 topRight;
}