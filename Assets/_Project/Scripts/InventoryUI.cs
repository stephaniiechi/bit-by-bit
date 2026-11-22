
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TMP_Text inventoryText;
    [SerializeField] private InventoryManager inventory;

    private bool isOpen;

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current == null)
            return;

        if (Keyboard.current.iKey.wasPressedThisFrame)
            ToggleInventory();
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        if (isOpen)
            RefreshInventory();
    }

    private void RefreshInventory()
    {
        if (inventory.CollectedItems.Count == 0)
        {
            inventoryText.text = "INVENTORY\n\nNo items collected.";
            return;
        }

        StringBuilder text = new StringBuilder();
        text.AppendLine("INVENTORY");
        text.AppendLine();

        foreach (string item in inventory.CollectedItems)
            text.AppendLine($"• {item}");

        inventoryText.text = text.ToString();
    }
}
