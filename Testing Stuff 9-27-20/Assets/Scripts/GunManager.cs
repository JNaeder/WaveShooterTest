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
            g.currentAmmo = g.maxAmmo;
        }

        SetCurrentGun(0);
    }



    public void AddGun(Gun newGun) {
        gunInventory.Add(newGun);

    }

    void ChangeToNextWeapon() {
        Debug.Log("Change Weapon");
        SetCurrentGun(CurrentGunIndex() + 1);

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
                Debug.Log("Current Gun Index is " + gunIndex);

                if (gunIndex >= gunInventory.Count - 1) {
                    gunIndex = -1;
                }
            }
        }
        return gunIndex;
    }
}
