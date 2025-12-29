using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemBaseSO _itemData;
    private ItemBase _item;

    private void OnEnable()
    {
        _item = _itemData.CreateItem();
    }

    public ItemBase GetItem()
    {
        return _item;
    }
}
