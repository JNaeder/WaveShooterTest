using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunUI : MonoBehaviour
{
    public TextMeshProUGUI gunNameText;
    public TextMeshProUGUI ammoText;
    public Image gunImage;

    GuyShooting gs;

    private void Awake()
    {
        gs = FindObjectOfType<GuyShooting>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        
    }

    void UpdateUI() {
        gunNameText.text = gs.currentGun.gunName;
        gunImage.sprite = gs.currentGun.gunImage;
        ammoText.text = "Ammo: " + gs.currentGun.currentClip + "/" + gs.currentGun.currentAmmo;


    }
}
