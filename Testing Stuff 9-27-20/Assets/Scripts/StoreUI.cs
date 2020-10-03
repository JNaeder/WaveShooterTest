using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUI : MonoBehaviour
{
    public GameObject storeUIGameobject;
    public GameObject previewScreen;



    Store store;
    GameManager gm;
    GunManager gunM;
    UIPreviewScreen uiPre;


    Item currentSelectedItem;

    ItemManager[] itemManagers;

    private void Awake()
    {
        store = FindObjectOfType<Store>();
        gm = FindObjectOfType<GameManager>();
        gunM = FindObjectOfType<GunManager>();
        uiPre = FindObjectOfType<UIPreviewScreen>();

        itemManagers = FindObjectsOfType<ItemManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        storeUIGameobject.SetActive(false);
        

    }

    private void Update()
    {
        SetPreviewScreen();
    }

    public void ShowStoreUI()
    {
        Cursor.visible = true;
        if (store.isInStore)
        {
            storeUIGameobject.SetActive(true);
            gm.SetGameState(GameManager.GameState.buying);
        }
    }

    public void HideStoreUI()
    {
        Cursor.visible = false;
        storeUIGameobject.SetActive(false);
        gm.SetGameState(GameManager.GameState.offWave);
        currentSelectedItem = null;
        foreach (ItemManager itemManager in itemManagers)
        {
            itemManager.ResetEverything();  
        }
    }

    public void StartWave()
    {
        gm.StartWave();
        gm.SetGameState(GameManager.GameState.onWave);
        storeUIGameobject.SetActive(false);
        Cursor.visible = false;
        currentSelectedItem = null;
    }

    public void SelectItem(Item newItem) {
        currentSelectedItem = newItem;
        uiPre.ShowPreview(newItem);
    }

    public void PurchaseSelectedItem() {
        if (currentSelectedItem.gunItem != null)
        {
            if (gm.money >= currentSelectedItem.itemCost)
            {
                gm.money -= currentSelectedItem.itemCost;
                gunM.AddGun(currentSelectedItem.gunItem);
                ResetItemManagers();
                currentSelectedItem = null;
            }
            else {
                Debug.Log("Not Enough Money to buy " + currentSelectedItem.itemName);
            }
        }
    }


    void ResetItemManagers() {
        foreach (ItemManager i in itemManagers)
        {
            i.CheckGunsWithPlayer();
        }

    }

    void SetPreviewScreen() {
        if (currentSelectedItem != null)
        {
            previewScreen.SetActive(true);
        }
        else {
            previewScreen.SetActive(false);
        }

    }



}
