using UnityEngine;



[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool stackable;
    public int maxStack = 50;

    private void OnValidate()
    {
        if (!stackable) maxStack = 1;
        else if (maxStack < 1) maxStack = 1;

        if (string.IsNullOrWhiteSpace(itemName))
        {  
            Debug.LogWarning($"{name}: Item name is empty", this);
        }

    }
}
