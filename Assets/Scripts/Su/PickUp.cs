using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickUp : MonoBehaviour
{
    [SerializeField] private Item itemPickedUp = null;
    // [SerializeField] private Image overheadImage = null;
    [SerializeField] private int numberPickedUp = 1;
    [SerializeField] string playerTag = "Player";


    // private void onMouseDown()
    // {
    //     FindObjectOfType<Inventory>().AddToInventory(itemPickedUp, 1);
    //     Destroy(gameObject);

    //     //dbg! add to loadUI
    // }


    private void Start()
    {
        SetImage(itemThatGetsPickedUp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag))
            AddToInventoryAndDestroyThis(collision.gameObject);
    }

    private void AddToInventoryAndDestroyThis(GameObject playerObject)
    {
        playerObject.GetComponent<Inventory>().AddToInventory(itemThatGetsPickedUp, 1);
        Destroy(gameObject);
    }

    public void SetItem(Item item, int amount = 1)
    {
        // SetImage(item);
        numberPickedUp = amount;
        itemPickedUp = item;
    }

    // private void SetImage(Item item)
    // {
    //     if (item != null && overheadImage != null)
    //         overheadImage.sprite = item.Icon;
    // }
}
