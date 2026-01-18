using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class EquipmentSlot : MonoBehaviour,
    IPointerClickHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler
{
    [Header("장비 슬롯 정보")]
    public ItemEnums.EquipItemType equipType;

    [Header("UI 구성 요소")]
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject highlight;

    private Inventory _inventory;
    private Equipment _equipment;

    private EquipItemBase equipItem =>
        _equipment.equipItems.ContainsKey(equipType) ? _equipment.equipItems[equipType] : null;

    private Canvas rootCanvas;
    private Image dragIcon;
    private RectTransform dragIconRect;

    public void Init(Inventory inventory, Equipment equipment,  ItemEnums.EquipItemType equipType)
    {
        _inventory = inventory;
        _equipment = equipment;
        this.equipType = equipType;

        rootCanvas = GetComponentInParent<Canvas>();
        Refresh();
    }

    public void Refresh()
    {
        EquipItemBase item = equipItem;

        if (item == null)
        {
            iconImage.enabled = false;
            return;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = item.itemBaseSO.itemIcon;
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        ItemBase item = equipItem;

        if (eventData.clickCount == 2)
        {
            Debug.Log("착용 해제 : " + item.itemBaseSO.itemName);
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("좌클릭 ; " + item.itemBaseSO.itemName);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("우클릭 : " + item.itemBaseSO.itemName);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (equipItem == null) return;

        iconImage.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIconRect == null) return;

        dragIconRect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot fromSlot =
            eventData.pointerDrag?.GetComponent<InventorySlot>();

        if (fromSlot == null) return;

        if (fromSlot.CurrentItem is EquipItemBase equipItem && equipItem.itemBaseSO.equipItemType == equipType)
        {
            _equipment.EquipItem(equipItem);
        }
    }
}

