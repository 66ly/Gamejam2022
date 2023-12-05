using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class InventoryUI : SlotHolderUI<Item>
{
    private Inventory inventory = null;
    // [SerializeField] private ActionOnClickItemUI itemAction;

    bool show;

    void Start()
    {
        show = false;
        if (inventory)
            InitializeUI(inventory.GetInventory, inventory.GetMaxItemSlots);
    }

    // Update is called once per frame
    void Update()
    {
        // Press Tab to open and close inventory panel
        if (show == false && Input.GetKey(KeyCode.Tab))
        {
            gameObject.SetActive(true);
            show = true;
        }
        else if (show == true && Input.GetKey(KeyCode.Tab))
        {
            gameObject.SetActive(false);
            show = false;
        }
    }

    protected override void InitializeUI(Dictionary<Item, int> inventory, int numSlots)
    {
        base.InitializeUI(inventory, numSlots);
        this.inventory.onInventoryChange += UpdateItemSlots;
    }


    protected override void UpdateSlotAt(int _index, Item item, int count)
    {
        if (count == 0)
            itemSlots[_index].gameObject.SetActive(false);
        else
        {
            itemSlots[_index].gameObject.SetActive(true);
            // UnityAction buttonAction = itemAction.Action(item, 1, inventory); // ??
            // itemSlots[_index].UpdateSlotUI(item, count, buttonAction);
            // itemSlots[_index].onClickRemove += () => DropItem(item);// ??
        }
    }

    public void ItemClicked(Item item)
    {
        inventory.LoadItem(item);
    }
}
