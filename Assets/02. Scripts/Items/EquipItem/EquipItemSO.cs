using UnityEngine;

[CreateAssetMenu(fileName = "New EquipItem", menuName = "Item/EquipItemSO")]
public class EquipItemSO : ItemBaseSO
{
    public ItemEnums.EquipItemType equipItemType;

    public override ItemBase CreateItem(int stack)
    {
        return new EquipItemBase(this, stack);
    }
}
