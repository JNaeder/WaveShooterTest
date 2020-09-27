using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Store : MonoBehaviour
{
    public GameObject startWaveButton;
    public GameObject StoreUI;

    GameManager gm;
    GuyController gc;

    bool isInStore;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        gc = FindObjectOfType<GuyController>();

        
    }

    private void Start()
    {
        gc.controls.Player.ShowStore.performed += _ => ShowStoreUI();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player") {
            startWaveButton.SetActive(true);
            GuyController gc = collision.gameObject.GetComponent<GuyController>();
            isInStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            startWaveButton.SetActive(false);
            GuyController gc = collision.gameObject.GetComponent<GuyController>();
            isInStore = false;
        }
    }


    public void ShowStoreUI() {
        Cursor.visible = true;
        if (isInStore)
        {
            StoreUI.SetActive(true);
            gm.SetGameState(GameManager.GameState.buying);
        }
    }

    public void HideStoreUI() {
        Cursor.visible = false;
        StoreUI.SetActive(false);
        gm.SetGameState(GameManager.GameState.offWave);
    }
}
