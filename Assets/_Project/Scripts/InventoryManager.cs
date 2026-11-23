
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private readonly List<string> collectedItems = new();

    public bool AddItem(string itemName)
    {
        if (collectedItems.Contains(itemName))
        {
            Debug.Log($"Item already collected: {itemName}");
            return false;
        }

        collectedItems.Add(itemName);

        Debug.Log($"Added to inventory: {itemName}");
        Debug.Log($"Total items: {collectedItems.Count}");

        return true;
    }

    public bool HasItem(string itemName)
    {
        return collectedItems.Contains(itemName);
    }
}
