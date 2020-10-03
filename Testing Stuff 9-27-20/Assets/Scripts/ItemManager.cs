using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();
    public GameObject shopItemPrefab;
    public Transform shopParent;
    public TMP_Dropdown[] filterDropDowns;
    public GameObject[] filterOptions;


    GunManager gunM;

    List<Item> tempList;

    int currentSortIndex = 0;

    private void Awake()
    {
        gunM = FindObjectOfType<GunManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        tempList = new List<Item>(itemList);
        tempList.Sort(SortPrice);
        PopulateList(tempList);
        HideAllFilterOptions();

    }

    void PopulateList(List<Item> newList) {
        for (int i = 0; i < newList.Count; i++)
        {
            GameObject newShopItem = Instantiate(shopItemPrefab, transform.position, Quaternion.identity) as GameObject;
            newShopItem.transform.SetParent(shopParent);
            ShopItem itemItem = newShopItem.GetComponent<ShopItem>();
            itemItem.item = newList[i];
        }
        CheckGunsWithPlayer();
    }

    public void CheckGunsWithPlayer() {
        ShopItem[] shopItems = shopParent.GetComponentsInChildren<ShopItem>();
        for (int i = 0; i < shopItems.Length; i++)
        {
            for (int j = 0; j < gunM.gunInventory.Count; j++)
            {
                if (shopItems[i].item.gunItem == gunM.gunInventory[j]) {
                   Button newButton = shopItems[i].GetComponent<Button>();
                    newButton.interactable = false;
                    Image[] imagesInPrefab = shopItems[i].GetComponentsInChildren<Image>();
                    foreach (Image x in imagesInPrefab)
                    {
                        x.color = new Color(0.5f, 0.5f, 0.5f, 0.2f);
                    }
                }
            }
        }
    }

    void DestroyCurrentList() {
        ShopItem[] shopitems = shopParent.GetComponentsInChildren<ShopItem>();
        foreach (ShopItem s in shopitems)
        {
            Destroy(s.gameObject);
        }
    }

    public void ResetEverything() {
        ChooseFilterOption(0);
        DestroyCurrentList();
        tempList = new List<Item>(itemList);
        UnFilterItems();
        SetSortValue(0);
    }



    //------- Sort Stuff------------------------

    public void SetSortValue(int newVal)
    {
        DestroyCurrentList();
        if (newVal == 0)
        {
            tempList.Sort(SortPrice);
        }
        else if (newVal == 1)
        {
            tempList.Sort(SortReloadTime);
        }
        else if (newVal == 2)
        {
            tempList.Sort(SortFireRate);
        }
        PopulateList(tempList);
        currentSortIndex = newVal;
    }



    int SortPrice(Item a, Item b) {
        if (a.itemCost < b.itemCost)
        {return -1;}
        else if (a.itemCost > b.itemCost) {return 1;}
        return 0;
    }

    int SortReloadTime(Item a, Item b)
    {
        if (a.gunItem.reloadTime < b.gunItem.reloadTime) { return -1; }
        else if (a.gunItem.reloadTime > b.gunItem.reloadTime) { return 1; }
        return 0;
    }

    int SortFireRate(Item a, Item b) {
        if (a.gunItem.fireRate < b.gunItem.fireRate) { return -1;}
        else if (a.gunItem.fireRate > b.gunItem.fireRate) { return 1;}
        return 0;
    }



    //------- Filter Stuff ----------------------------

    public void ChooseFilterOption(int newOption)
    {
        if (newOption == 0) { UnFilterItems(); }
        else {ShowFilterOptions(newOption - 1); }
    }

    void ShowFilterOptions(int newVal) {
        HideAllFilterOptions();
        filterOptions[newVal].SetActive(true);
    }

    void HideAllFilterOptions() {
        for (int i = 0; i < filterOptions.Length; i++)
        {
            filterOptions[i].SetActive(false);
        }

    }

    public void FilterItemsByFireMethod(int newVal) {
        DestroyCurrentList();
        tempList = new List<Item>();
        Gun.FireMethod tempFireMethod = Gun.FireMethod.Single;
        if (newVal == 1) { tempFireMethod = Gun.FireMethod.Single; }
        else if (newVal == 2) { tempFireMethod = Gun.FireMethod.Automatic; }
        else if (newVal == 3) { tempFireMethod = Gun.FireMethod.Burst; }
        else if (newVal == 4) { tempFireMethod = Gun.FireMethod.Steady; }
        else { UnFilterItems();}
            foreach (Item item in itemList)
            {
                if (item.gunItem.fireMethod == tempFireMethod)
                {
                    tempList.Add(item);
                }
            }
        SetSortValue(currentSortIndex);
    }


    public void FilterItemsByAmmoType(int newVal) {
        DestroyCurrentList();
        tempList = new List<Item>();
        Bullet.AmmoType newAmmoType = Bullet.AmmoType.Earth;
        if (newVal == 1) { newAmmoType = Bullet.AmmoType.Earth; }
        else if (newVal == 2) { newAmmoType = Bullet.AmmoType.Energy; }
        else { UnFilterItems(); }
        foreach (Item item in itemList)
        {
            Bullet.AmmoType newBullet = item.gunItem.bulletPrefab.GetComponent<Bullet>().bulletAmmoType;
            if (newBullet == newAmmoType) {
                tempList.Add(item);
            }
        }
        SetSortValue(currentSortIndex);
    }

    public void UnFilterItems() {
        HideAllFilterOptions();
        DestroyCurrentList();
        tempList = new List<Item>(itemList);
        SetSortValue(currentSortIndex);
        foreach (TMP_Dropdown d in filterDropDowns){d.value = 0;}
    }





    
}
