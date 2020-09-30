using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    public GameObject shopItemPrefab;
    public Transform shopParent;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject newShopItem = Instantiate(shopItemPrefab, transform.position, Quaternion.identity) as GameObject;
            newShopItem.transform.SetParent(shopParent);
            ShopItem itemItem = newShopItem.GetComponent<ShopItem>();
            itemItem.item = itemList[i];

        }



    }

    
}
