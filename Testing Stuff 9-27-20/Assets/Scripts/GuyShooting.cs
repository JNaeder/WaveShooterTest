using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyShooting : MonoBehaviour
{
    public GameObject crosshairPrefab;
    public Transform gunHolder;

    public Gun currentGun;

    public GameObject reloadingBar;
    public Transform reloadingBarFront;


    bool isShooting;
    float shootTime;
    bool isReloading;
    float reloadTime;
    bool steadyHasStarded;


    GuyController gc;
    GameManager gm;
    Camera mainCam;

    GameObject crosshair;
    GameObject steadyBullet;

    private void Awake()
    {
        gc = GetComponent<GuyController>();
        gm = FindObjectOfType<GameManager>();
        mainCam = Camera.main;

        gc.controls.Player.Shoot.performed += _ => SetShootBool(true);
        gc.controls.Player.Shoot.canceled += _ => SetShootBool(false);
        gc.controls.Player.Reload.performed += _ => StartReload();


    }
    // Start is called before the first frame update
    void Start()
    {
        crosshair = Instantiate(crosshairPrefab, MousePosition(), Quaternion.identity);
        reloadingBar.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            Reloading();
        }

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
        if (currentGun.fireMethod == Gun.FireMethod.Automatic)
        {
            float newRate = 1 / currentGun.fireRate;
            if (Time.time > shootTime + newRate)
            {
                if (currentGun.currentClip > 0)
                {
                    Shoot();
                    shootTime = Time.time;
                }
                else
                {
                    if (!isReloading)
                    {
                        StartReload();
                    }
                }
            }
        }
        // If Gun In Single Shot
        else if (currentGun.fireMethod == Gun.FireMethod.Single)
        {
            if (currentGun.currentClip > 0)
            {
                Shoot();
                SetShootBool(false);
            }
            else
            {
                if (!isReloading)
                {
                    StartReload();
                }
            }

        }
        // If Gun Is Burst
        else if (currentGun.fireMethod == Gun.FireMethod.Burst)
        {
            float newRate = 1 / currentGun.fireRate;
            if (currentGun.currentClip > 0)
            {
                StartCoroutine(BurstMethod(newRate, currentGun.burstAmount));
            }
            else if (!isReloading)
            {
                StartReload();
            }
        }
        // If Steady
        else if (currentGun.fireMethod == Gun.FireMethod.Steady) {
            if (!steadyHasStarded)
            {
                Shoot();
                steadyHasStarded = true;
            }
            UpdateSteadyBulletPosition(MuzzlePos(), gunHolder.rotation);
        }

    }

    void Shoot() {
        if (currentGun.bulletMethod == Gun.BulletMethod.Single)
        {
            GameObject bullet = Instantiate(currentGun.bulletPrefab, MuzzlePos(), FireMethods.BulletRotation(MousePosition(), MuzzlePos())) as GameObject;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bullet.transform.up * currentGun.bulletSpeed;
            StatTracker.shotsFired++;
            currentGun.currentClip--;
        }
        else if (currentGun.bulletMethod == Gun.BulletMethod.Multi)
        {
            for (int i = 0; i < currentGun.multiShotNumberOfShots; i++)
            {
                GameObject bullet = Instantiate(currentGun.bulletPrefab, MuzzlePos(), FireMethods.MultiBulletRotation(MousePosition(), MuzzlePos(), currentGun.multiShotSpreadAngle, currentGun.multiShotNumberOfShots, i)) as GameObject;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.velocity = bullet.transform.up * currentGun.bulletSpeed;
                StatTracker.shotsFired++;
            }
            currentGun.currentClip--;
        }
        else if (currentGun.bulletMethod == Gun.BulletMethod.Steady) {
            steadyBullet = Instantiate(currentGun.bulletPrefab, MuzzlePos(), gunHolder.rotation) as GameObject;

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
                
            }
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

   public Vector2 MousePosition() {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(gc.controls.Player.MousePosition.ReadValue<Vector2>());
        return mousePos;
    }


    void SetShootBool(bool newBool) {
        if (!isReloading)
        {
            isShooting = newBool;
            shootTime = Time.time;
        }
        else {
            isShooting = false;
        }


        if (newBool == false) {
            if (currentGun.fireMethod == Gun.FireMethod.Steady) {
                DestroySteadyBullet();
                steadyHasStarded = false;
            }

        }

    }

    Vector3 MuzzlePos() {
        GunObject currentGunObject = gunHolder.GetComponentInChildren<GunObject>();
        Vector3 newPos = currentGunObject.muzzle.position;
        return newPos;
    }


    void StartReload() {
        if (!isReloading && currentGun.currentAmmo > 0)
        {
            if (currentGun.currentClip < currentGun.clipCapcity)
            {
                isReloading = true;
                reloadTime = Time.time;
                reloadingBar.SetActive(true);
            }
        }
    }

    void Reloading() {

        if (Time.time > reloadTime + currentGun.reloadTime)
        {
            Reload();
            isReloading = false;
            reloadingBar.SetActive(false);
        }
        else
        {
            float reloadPerc = (Time.time - reloadTime) / currentGun.reloadTime;
            Vector3 reloadBarScale = reloadingBarFront.localScale;
            reloadBarScale.x = reloadPerc;
            reloadingBarFront.localScale = reloadBarScale;
        }
    }

    public void CancelReload() {
        isReloading = false;
        reloadingBar.SetActive(false);
    }

    IEnumerator BurstMethod(float fireRate, int burstNum) {
        for (int i = 0; i < burstNum; i++)
        {
            Shoot();
            SetShootBool(false);
            yield return new WaitForSeconds(fireRate);
            SetShootBool(true);
        }
        SetShootBool(false);
    }

    void DestroySteadyBullet() {
        Destroy(steadyBullet);
    }

    void UpdateSteadyBulletPosition(Vector3 pos, Quaternion rot) {
        steadyBullet.transform.position = pos;
        steadyBullet.transform.rotation = rot;

    }
}
