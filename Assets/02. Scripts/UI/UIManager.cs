using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryUI inventoryUI;

    void Start()
    {
        inventoryUI.Init(FindAnyObjectByType<CharacterModel>().Inventory);
        inventoryUI.gameObject.SetActive(false);
    }
}
