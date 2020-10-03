using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Store : MonoBehaviour
{
    public GameObject startWaveInfo;

    GameManager gm;
    GuyController gc;
    GunManager gunM;
    StoreUI storeUI;

    [HideInInspector]
    public bool isInStore;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gc = FindObjectOfType<GuyController>();
        gunM = gc.GetComponent<GunManager>();
        storeUI = FindObjectOfType<StoreUI>();
    }

    private void Start()
    {
        gc.controls.Player.ShowStore.performed += _ => ShowStoreUI();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            startWaveInfo.SetActive(true);
            isInStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            startWaveInfo.SetActive(false);
            isInStore = false;
        }
    }

    void ShowStoreUI() {
        storeUI.ShowStoreUI();
    }
}
