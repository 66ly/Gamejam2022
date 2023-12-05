using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SlotHolderUI<T> : MonoBehaviour
{
    protected Dictionary<T, int> inventoryOfType;

    protected ItemSlotUI[] itemSlots = null;
    [SerializeField] private GameObject itemUIContainerPrefab = null;

    [SerializeField] private GameObject holderObject = null;
    [SerializeField] private Transform contentParent = null;

    protected virtual void InitializeUI(Dictionary<T, int> inventory, int numSlots)
    {
        inventoryOfType = inventory;

        CreateItemSlots(numSlots);
    }

    private void CreateItemSlots(int numSlotsToCreate)
    {
        if (itemSlots != null && itemSlots.Length >= numSlotsToCreate)
            return;

        itemSlots = new ItemSlotUI[numSlotsToCreate];

        for (int i = 0; i < numSlotsToCreate; ++i)
        {
            GameObject obj = Instantiate(itemUIContainerPrefab, contentParent);

            itemSlots[i] = obj.GetComponent<ItemSlotUI>();

            obj.SetActive(false);
        }
    }

    protected void UpdateItemSlots()
    {
        int index = 0;

        foreach (KeyValuePair<T, int> kvp in inventoryOfType)
        {
            UpdateSlotAt(index, kvp.Key, kvp.Value);
            index++;
        }

        for (int i = index; i < itemSlots.Length; ++i) //Deactivate unoccupied slots
            itemSlots[i].gameObject.SetActive(false);
    }

    protected abstract void UpdateSlotAt(int _index, T itemOfType, int count);

    public void ChangeInventoryState() => holderObject.SetActive(!holderObject.activeSelf);
}