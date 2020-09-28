using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public float itemCost;
    public Sprite itemImage;
    public Gun gunItem;

}
