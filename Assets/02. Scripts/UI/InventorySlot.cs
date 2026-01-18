using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour,
    IPointerClickHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler
{
    [Header("아이템 슬룻 정보")]
    public int slotIndex;

    [Header("UI 구성 요소")]
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject highlight;
    [SerializeField] private TMP_Text stackText;

    private Inventory _inventory;
    private Equipment _equipment;

    private Canvas rootCanvas;
    private Image dragIcon;
    private RectTransform dragIconRect;


    private ItemBase currentItem =>
        slotIndex < _inventory.Items.Count ? _inventory.Items[slotIndex] : null;

    public ItemBase CurrentItem => currentItem;

    #region 생성 및 새로고침
    public void Init(Inventory inventory,Equipment equipment, int index)
    {
        _inventory = inventory;
        _equipment = equipment;
        slotIndex = index;

        rootCanvas = GetComponentInParent<Canvas>();
        Refresh();
    }

    public void Refresh()
    {
        ItemBase item = CurrentItem;

        if (item == null)
        {
            iconImage.enabled = false;
            //highlight?.SetActive(false);
            return;
        }
        
        iconImage.enabled = true;
        iconImage.sprite = item.itemBaseSO.itemIcon;
        stackText.text = item.currentStack.ToString();
    }
    #endregion

    #region 클릭
    public void OnPointerClick(PointerEventData eventData)
    {
        ItemBase item = CurrentItem;

        if (item == null) return;

        if (eventData.clickCount == 2)
        {
            if (item is EquipItemBase)
            {
                _equipment.EquipItem(item as EquipItemBase);
            }
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Select(item);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OpenContextMenu(item);
        }
        Debug.Log($"슬롯 {slotIndex} 클릭됨");
    }

    #endregion

    #region 드래그 & 드랍
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (CurrentItem == null) return;

        CreateDragIcon();
        iconImage.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIconRect == null) return;

        dragIconRect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DestroyDragIcon();
        Refresh();
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot fromSlot =
            eventData.pointerDrag?.GetComponent<InventorySlot>();

        if (fromSlot == null || fromSlot == this) return;

        _inventory.Swap(fromSlot.slotIndex, this.slotIndex);
        Refresh();
    }
    #endregion

    #region 인벤토리 액션
    private void Select(ItemBase item)
    {
        Debug.Log("아이템 선택");
        //highlight?.SetActive(true);
        // 아이템 정보 UI 호출
    }

    private void Use(ItemBase item)
    {
        // 아이템 사용 로직
        Debug.Log(item.itemBaseSO.itemName + " 사용됨");
    }

    private void OpenContextMenu(ItemBase item)
    {
        // 컨텍스트 메뉴 UI 호출
        Debug.Log(item.itemBaseSO.itemName + " 컨텍스트 메뉴 열림");
    }
    #endregion

    #region 드래그 아이콘 관리
    private void CreateDragIcon()
    {
        dragIcon = new GameObject("DragIcon").AddComponent<Image>();
        dragIcon.transform.SetParent(rootCanvas.transform, false);
        dragIcon.raycastTarget = false;
        dragIcon.sprite = iconImage.sprite;

        dragIconRect = dragIcon.rectTransform;
        dragIconRect.sizeDelta = iconImage.rectTransform.sizeDelta;
        dragIconRect.position = Input.mousePosition;
    }

    private void DestroyDragIcon()
    {
        if (dragIcon != null)
            Destroy(dragIcon.gameObject);

        dragIcon = null;
        dragIconRect = null;
    }
    #endregion

}
