using System.Security.Cryptography;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public int slotIndex;

    private Inventory _inventory;

    public void Init(Inventory inventory, int index)
    {
        _inventory = inventory;
        slotIndex = index;
        Refresh();
    }

    public void Refresh()
    {
        ItemBase item = _inventory.Items[slotIndex];

        if (item != null)
        {
            SetSlot();
        }
        else
        {
            ClearSlot();
        }
    }

    public void ClearSlot()
    {
        //아이템 슬룻 비우기
    }

    public void SetSlot()
    {
        // 아이템 정보 받아와서 슬룻 채우기
    }
}
