using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    private Inventory inventory;
    public Inventory Inventory => inventory;

    public Item testItem;

    private void Awake()
    {
        inventory = new Inventory(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.AddItem(testItem.GetItem());
        }
    }
}
