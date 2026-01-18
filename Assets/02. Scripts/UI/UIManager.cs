using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryUI inventoryUI;

    void Start()
    {
        CharacterModel character = FindAnyObjectByType<CharacterModel>();

        inventoryUI.Init(character.Inventory, character.Equipment);
        inventoryUI.gameObject.SetActive(false);
    }
}
