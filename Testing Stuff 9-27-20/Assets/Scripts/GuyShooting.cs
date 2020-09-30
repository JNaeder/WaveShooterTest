using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyShooting : MonoBehaviour
{
    public GameObject crosshairPrefab;
    public Transform gunHolder;

    public Gun currentGun;


    bool isShooting;
    float shootTime;


    GuyController gc;
    GameManager gm;
    Camera mainCam;

    GameObject crosshair;

    private void Awake()
    {
        gc = GetComponent<GuyController>();
        gm = FindObjectOfType<GameManager>();
        mainCam = Camera.main;

        gc.controls.Player.Shoot.performed += _ => SetShootBool(true);
        gc.controls.Player.Shoot.canceled += _ => SetShootBool(false);
        gc.controls.Player.Reload.performed += _ => Reload();


    }
    // Start is called before the first frame update
    void Start()
    {
        crosshair = Instantiate(crosshairPrefab, MousePosition(), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {

        if (gm.currentGameState == GameManager.GameState.offWave || gm.currentGameState == GameManager.GameState.onWave) {
            UpdateCrosshair();
            RotateGun();
            if (isShooting) {
                Shooting();
            }
        }



    }

    public void ChangeGunObject(GunObject theGun) {
        GameObject oldGun = gunHolder.GetComponentInChildren<GunObject>().gameObject;
        if (oldGun != null) {
            Destroy(oldGun);
        }
        if (MousePosition().x > transform.position.x)
        {
            GameObject newGun = Instantiate(theGun.gameObject, gunHolder.position, gunHolder.rotation) as GameObject;
            newGun.transform.parent = gunHolder;
        }
        else if (MousePosition().x < transform.position.x) {
            GameObject newGun = Instantiate(theGun.gameObject, gunHolder.position, gunHolder.rotation) as GameObject;
            newGun.transform.localScale = new Vector3(1, -1, 1);
            newGun.transform.parent = gunHolder;
        }
        
    }

    void UpdateCrosshair()
    {
        crosshair.transform.position = MousePosition();
    }

    void Shooting() {
        // If Gun is Continuous
        if (currentGun.fireMethod == Gun.FireMethod.continuous)
        {
            float newRate = 1 / currentGun.fireRate;
            if (Time.time > shootTime + newRate && currentGun.currentClip > 0)
            {
                Shoot();
                shootTime = Time.time;
            }
        }
        // If Gun In Single Shot
        else if (currentGun.fireMethod == Gun.FireMethod.single || currentGun.fireMethod == Gun.FireMethod.multiShot) {
            if (currentGun.currentClip > 0)
            {
                Shoot();
                SetShootBool(false);
            }
        }
    }

    void Shoot() {


        if (currentGun.fireMethod == Gun.FireMethod.single || currentGun.fireMethod == Gun.FireMethod.continuous)
        {
            GameObject bullet = Instantiate(currentGun.bulletPrefab, MuzzlePos(), FireMethods.BulletRotation(MousePosition(), MuzzlePos())) as GameObject;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.up * currentGun.bulletSpeed;
            StatTracker.shotsFired++;
            currentGun.currentClip--;
        }
        else if (currentGun.fireMethod == Gun.FireMethod.multiShot) {
            for (int i = 0; i < currentGun.multiShotNumberOfShots; i++)
            {
                GameObject bullet = Instantiate(currentGun.bulletPrefab, MuzzlePos(), FireMethods.MultiBulletRotation(MousePosition(), MuzzlePos(), currentGun.multiShotSpreadAngle, currentGun.multiShotNumberOfShots, i)) as GameObject;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.velocity = bullet.transform.up * currentGun.bulletSpeed;
                StatTracker.shotsFired++;
                currentGun.currentClip--;
            }


        }
        
    }

    void Reload()
    {
        if (currentGun.currentAmmo > 0)
        {
            if (currentGun.currentClip < currentGun.clipCapcity)
            {
                int leftInChamber = currentGun.currentClip;
                if (currentGun.currentAmmo >= currentGun.clipCapcity)
                {
                    currentGun.currentClip = currentGun.clipCapcity;
                    currentGun.currentAmmo -= currentGun.clipCapcity - leftInChamber;
                }
                else
                {
                    currentGun.currentClip = currentGun.currentAmmo;
                    currentGun.currentAmmo = 0;
                }
                Debug.Log("Reloaded " + currentGun.gunName);
            }
            else
            {
                Debug.Log("Already Full Clip");
            }
        }
        else {
            Debug.Log("Out Of Ammo");
        }
    }

    void RotateGun() {
        Vector3 diff = MousePosition() - new Vector2(transform.position.x, transform.position.y);
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        if (MousePosition().x > transform.position.x)
        {
            gunHolder.localScale = new Vector3(1, 1, 1);
            gunHolder.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
        else if (MousePosition().x < transform.position.x) {
            gunHolder.localScale = new Vector3(1, -1, 1);
            gunHolder.rotation = Quaternion.Euler(0f, 0f, rot_z);
        }
    }

    Vector2 MousePosition() {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(gc.controls.Player.MousePosition.ReadValue<Vector2>());
        return mousePos;
    }
    void SetShootBool(bool newBool) {
        isShooting = newBool;
        shootTime = Time.time;
    }

    Vector3 MuzzlePos() {
        GunObject currentGunObject = gunHolder.GetComponentInChildren<GunObject>();
        Vector3 newPos = currentGunObject.muzzle.position;
        return newPos;
    }

    

    



}
