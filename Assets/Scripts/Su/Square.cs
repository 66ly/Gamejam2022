using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum SquareColor
{
    Blue,
    Red,
    Yello,
    Green
}


public class Square : Item
{
    SquareColor color;
    int shape = 0; // Once more components are added, replaced it with the item shape data.

    void Start()
    {
        ItemName = "Square";
        color = SquareColor.Blue;
        pickUpText.gameObject.SetActive(true);
    }

    void Update() { }

}

