using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public List<Gun> gunInventory = new List<Gun>();

    GuyShooting gs;
    GuyController gc;

    private void Awake()
    {
        gs = GetComponent<GuyShooting>();
        gc = GetComponent<GuyController>();
    }


    // Start is called before the first frame update
    void Start()
    {
        gc.controls.Player.ChangeWeapon.performed += _ => ChangeToNextWeapon();


        foreach (Gun g in gunInventory)
        {
            g.currentAmmo = g.startAmmo;
            g.currentClip = g.clipCapcity;
        }

        SetCurrentGun(0);
    }



    public void AddGun(Gun newGun) {
            gunInventory.Add(newGun);
            newGun.currentAmmo = newGun.startAmmo;
            newGun.currentClip = newGun.clipCapcity;
            ChangeToNewestGun();
    }

    void ChangeToNextWeapon() {
        SetCurrentGun(CurrentGunIndex() + 1);
    }

    void ChangeToNewestGun()
    {
        SetCurrentGun(gunInventory.Count - 1);
    }

    void SetCurrentGun(int gunIndex) {
        gs.currentGun = gunInventory[gunIndex];

    }


    int CurrentGunIndex() {
        int gunIndex = 0;
        for (int i = 0; i < gunInventory.Count; i++)
        {
            if (gs.currentGun == gunInventory[i]) {
                gunIndex = i;
                if (gunIndex >= gunInventory.Count - 1) {
                    gunIndex = -1;
                }
            }
        }
        return gunIndex;
    }


    public bool GunExists(Gun thisGun) {
        bool doesExist = false;
        foreach (Gun g in gunInventory)
        {
            if (g == thisGun) {
                doesExist = true;
            }
        }
        return doesExist;
    }
}
