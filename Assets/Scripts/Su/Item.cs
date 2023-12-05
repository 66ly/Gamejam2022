using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
// ScriptableObject
{
    [SerializeField] public string ItemName = "<ItemName>";
    [SerializeField, Multiline] public string Description = "Please add an item description";
    [SerializeField] public Sprite Icon = null;
    // [SerializeField] public GameObject Prefab = null; // The form that this object takes
    // [SerializeField] private int value = 1;

    [SerializeField] public Text pickUpText;

    bool pickUpAllowed;
    GameObject playerObject = null;
    string playerTag = "Player";


    // Update is called once per frame  
    void Update()
    {
        // if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        // {
        //     PickUp();
        // }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Cow"))
        {
            pickUpText.text = "Press 'E' to pick up.";
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
            UpdateColliderInfo(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider>().name.Equals("Cow"))
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
            UpdateColliderInfo(collision);
        }
    }

    void UpdateColliderInfo(Collider2D collision)
    {
        if (playerObject != null && collision.GetComponent<Collider>().CompareTag(playerTag))
        {
            playerObject = collision.gameObject;
        }
    }

    // void PickUp()
    // {
    //     playerObject.GetComponent<Inventory>().AddToInventory(gameObject, 1);
    //     Destroy(gameObject);
    // }

    // void AddToInventory()
    // {
    //     // GameObject inventoryUI = GameObject.FindWithTag("Inventory");
    //     // inventoryUI.gameObject.inventory.AddToInventory(gameObject, 1);
    // }
}
