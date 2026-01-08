using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    [Header("인벤토리 설정")]
    public int inventorySlotSize = 30;
    private Inventory inventory;
    public Inventory Inventory => inventory;

    public Item testItem;

    private void Awake()
    {
        inventory = new Inventory(this, inventorySlotSize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.AddItem(testItem.GetItem());
        }
    }
}
