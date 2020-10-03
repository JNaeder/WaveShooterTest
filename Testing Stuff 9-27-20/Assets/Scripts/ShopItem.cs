using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    GameManager gm;
    GuyController gc;
    GuyShooting gs;
    GunManager gunM;
    StoreUI storeUI;

    public Item item;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCost;



    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gc = FindObjectOfType<GuyController>();
        storeUI = FindObjectOfType<StoreUI>();
        gs = gc.GetComponent<GuyShooting>();
        gunM = gc.GetComponent<GunManager>();

        SetUpItem();
        
    }

    void SetUpItem() {
        itemImage.sprite = item.itemImage;
        itemName.text = item.itemName;
        itemCost.text = "$" + item.itemCost;

    }

    public void AddAmmo(int newAmmo) {
        if (gm.money >= item.itemCost)
        {
            gs.currentGun.currentAmmo += newAmmo;
            gm.money -= item.itemCost;
        }
        else
        {
            Debug.Log("Not Enough Money");
        }
    }

    public void AddGun() {
        if (gm.money >= item.itemCost)
        {
            if (!gunM.GunExists(item.gunItem))
            {
                gunM.AddGun(item.gunItem);
                gm.money -= item.itemCost;
            }
            else
            {
                Debug.Log("Gun Already In Inventory");
            }
        }
        else {
            Debug.Log("Not Enough Money");
        }
    }

    public void SelectThisItem() {
        storeUI.SelectItem(item);
    }


    
    
}
