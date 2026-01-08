using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] InventorySlot slotPrefab;
    [SerializeField] Transform slotParent;

    private Inventory inventory;
    private List<InventorySlot> slots = new();

    public void Init(Inventory inventory)
    {
        this.inventory = inventory;

        for (int i = 0; i < inventory.slotSize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, slotParent);
            slot.Init(inventory, i);
            slots.Add(slot);
        }

        BindInventoryEvents();
        RefreshAll();
    }

    private void BindInventoryEvents()
    {
        inventory.OnAddItemInventory += OnInvenytoryChange;
        inventory.OnRemoveItemInventory += OnInvenytoryChange;
    }

    private void OnInvenytoryChange(ItemBase _)
    {
        RefreshAll();
    }

    private void RefreshAll()
    {
        foreach (var slot in slots)
            slot.Refresh();
    }
}
