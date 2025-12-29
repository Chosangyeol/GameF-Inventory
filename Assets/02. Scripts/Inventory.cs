using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private CharacterModel owner;

    private List<ItemBase> items;
    public List<ItemBase> Items => items;

    /// <summary>
    /// 인벤토리에 아이템이 추가된 후 호출되는 이벤트
    /// EX) UI 업데이트
    /// </summary>
    public event Action<ItemBase> OnAddItemInventory;

    /// <summary>
    /// 인벤토리에 아이템이 제거된 후 호출되는 이벤트
    /// EX) UI 업데이트
    /// </summary>
    public event Action<ItemBase> OnRemoveItemInventory;

    public Inventory(CharacterModel model)
    {
        owner = model;
        items = new List<ItemBase>();
        return;
    }

    /// <summary>
    /// 인벤토리에 아이템을 추가하는 함수
    /// </summary>
    /// <param name="item">인벤토리에 추가할 아이템</param>
    public void AddItem(ItemBase item)
    {
        // 1. 아이템이 스택이 불가능한 경우
        if (!item.itemBaseSO.stackable)
        {
            items.Add(item);
            item.OnAddInventory();
            OnAddItemInventory?.Invoke(item);
            return;
        }

        // 2. 아이템이 스택이 가능한 경우
        for (int i = 0; i < items.Count; i++)
        {
            ItemBase nowItem = items[i];

            if (nowItem.itemBaseSO.itemID != item.itemBaseSO.itemID)
                continue;

            if (nowItem.currentStack >= nowItem.maxStack)
                continue;

            int leftStack = nowItem.maxStack - nowItem.currentStack;
            int addStack = Mathf.Min(leftStack, item.currentStack);

            nowItem.currentStack += addStack;
            item.currentStack -= addStack;

            if (item.currentStack <= 0)
                return;
        }

        // 3. 남은 스택을 새로운 아이템으로 추가하는 경우
        while (item.currentStack > 0)
        {
            int addStack = Mathf.Min(item.maxStack, item.currentStack);

            items.Add(item);
            item.OnAddInventory();
            OnAddItemInventory?.Invoke(item);

            item.currentStack -= addStack;
        }   
    }

    /// <summary>
    /// 인벤토리에 있는 아이템의 지속효과를 작동시키는 함수
    /// 대부분의 상황에서 delta는 Time.deltaTime이 들어감
    /// </summary>
    /// <param name="delta"></param>
    public void UpdateItem(float delta)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].OnUpdateInventory(delta);
        }
    }

    /// <summary>
    /// 인벤토리에 아이템을 제거하는 함수
    /// </summary>
    /// <param name="item">인벤토리에서 제거할 아이템</param>
    public void RemoveItem(ItemBase item)
    {
        items.Remove(item);
        item.OnRemoveInventory();
        OnRemoveItemInventory?.Invoke(item);
    }
}
