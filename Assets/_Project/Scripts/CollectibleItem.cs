
using UnityEngine;

public class CollectibleItem : MonoBehaviour, IInteractable
{
    [Header("Item Information")]
    [SerializeField] private string itemName;

    [TextArea(2, 5)]
    [SerializeField] private string description;

    private InventoryManager inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<InventoryManager>();
    }

    public void Interact()
    {
        if (inventory == null)
        {
            Debug.LogError("InventoryManager not found!");
            return;
        }

        bool added = inventory.AddItem(itemName);

        if (!added)
            return;

        Debug.Log(description);

        gameObject.SetActive(false);
    }
}
