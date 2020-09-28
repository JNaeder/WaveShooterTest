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

    public Item item;
    public Image itemImage;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemCost;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gc = FindObjectOfType<GuyController>();
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
        }
    }

    public void AddGun() {
        gunM.AddGun(item.gunItem);
    }

    public void SpendMoney() {
        if (gm.money >= item.itemCost)
        {
            gm.money -= item.itemCost;
        }
        else {
            Debug.Log("Not enough Money!");

        }

    }
}
