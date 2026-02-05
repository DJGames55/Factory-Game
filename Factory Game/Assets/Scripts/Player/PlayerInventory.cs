using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "Scriptable Objects/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{
     public List<InventorySlot> slots = new List<InventorySlot>();
    public int maxSlots = 20;
}

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int amount;
}
