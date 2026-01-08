using System.Collections.Generic;
using UnityEngine;

public class Equipment
{
    public CharacterModel owner;

    /// <summary>
    /// 케릭터의 아이템 착용 슬룻을 저장하는 Dictionary
    /// 장착 아이템의 부위를 Key로 사용하고, 장착된 아이템을 Value로 사용
    /// </summary>
    public Dictionary<ItemEnums.EquipItemType, EquipItemBase> equipItems = new Dictionary<ItemEnums.EquipItemType, EquipItemBase>()
    {
        { ItemEnums.EquipItemType.Head, null },
        { ItemEnums.EquipItemType.Body, null },
        { ItemEnums.EquipItemType.Pants, null },
        { ItemEnums.EquipItemType.Gloves, null },
        { ItemEnums.EquipItemType.Shoes, null }
    };

    public Equipment(CharacterModel owner)
    {
        this.owner = owner;
        return;
    }

    /// <summary>
    /// 아이템을 착용할 때 호출하는 메서드
    /// </summary>
    /// <param name="item">착용할 아이템</param>
    public void EquipItem(EquipItemBase item)
    {
        // 1. 아이템이 없는 경우
        if (item == null)
        {
            Debug.LogWarning("EquipItem: item is null");
            return;
        }

        // 2. 이미 장착된 아이템이 있는 경우
        if (equipItems[item.itemBaseSO.equipItemType] != null)
        {
            UnequipItem(item.itemBaseSO.equipItemType);
        }

        // 3. 아이템 장착 로직
        equipItems[item.itemBaseSO.equipItemType] = item;
    }

    /// <summary>
    /// 아이템을 착용 해체할 때 호출하는 메서드
    /// </summary>
    /// <param name="equipItemType">착용 해체 할 아이템 부위</param>
    public void UnequipItem(ItemEnums.EquipItemType equipItemType)
    {
        // 1. 해당 부위에 장착된 아이템이 없는 경우
        if (equipItems[equipItemType] == null)
        {
            Debug.LogWarning("UnequipItem: no item equipped in this slot");
            return;
        }

        // 2. 아이템 제거 로직
        owner.Inventory.AddItem(equipItems[equipItemType]);
        equipItems[equipItemType] = null;
    }
}
