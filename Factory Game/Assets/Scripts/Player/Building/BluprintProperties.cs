using UnityEngine;

[CreateAssetMenu(fileName = "BluprintProperties", menuName = "Scriptable Objects/BluprintProperties")]
public class BluprintProperties : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite image;

    public int buildCost;
}
