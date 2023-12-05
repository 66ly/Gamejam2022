using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private InventoryUI inventoryUI = null;

    private Dictionary<Item, int> inventory = new Dictionary<Item, int>(); // <item> : <count>


    private int maxItemSlots = 5;

    public delegate void OnInventoryChange();
    public OnInventoryChange onInventoryChange;

    public Dictionary<Item, int> GetInventory => inventory;
    public int NumberHeldOf(Item i) => inventory.ContainsKey(i) ? inventory[i] : 0;
    public int GetMaxItemSlots => maxItemSlots;

    public Item? loadedItem = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory(Item item, int count)
    {
        if (!inventory.ContainsKey(item))
        {
            inventory.Add(item, count);
        }
        else
        {
            inventory[item] += count;

        }
        onInventoryChange?.Invoke();
    }

    public bool RemoveFromInventory(Item item, int count)
    {
        bool removed = false;
        if (!inventory.ContainsKey(item))
            throw new System.Exception("The dictionary doesn't contain that item. How did we get here?");

        if (inventory[item] >= count)
        {
            inventory[item] -= count;
            removed = true;
        }
        else
        {
            Debug.LogWarning($"Inventory contains no {item.name}. Setting count to zero");
            inventory[item] = 0;
        }

        onInventoryChange?.Invoke();
        return removed;
    }

    public void LoadItem(Item item)
    {
        if (inventory.ContainsKey(item) && inventory[item] > 0)
        {
            loadedItem = item;
            // playerObject.GetComponent<LoadedItemUI>().UpdateItem(item); // dbg!
        }
        else
        {
            Debug.LogWarning($"Inventory contains no {item.name}. {item.name} cannot be loaded.");
        }
    }

    public Item? DropLoadedItem()
    {
        if (!(inventory.ContainsKey(loadedItem) && inventory[loadedItem] > 0))
        {
            return null;
        }

        RemoveFromInventory(loadedItem, 1);

        onInventoryChange?.Invoke();

        return loadedItem;
    }

}

