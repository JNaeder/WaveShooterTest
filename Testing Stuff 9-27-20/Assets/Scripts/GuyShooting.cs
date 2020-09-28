using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyShooting : MonoBehaviour
{
    public GameObject crosshairPrefab;
    public Transform arm;
    public Transform muzzle;

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
            RotateArm();

            if (isShooting) {
                Shooting();
            }
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

            if (Time.time > shootTime + newRate && currentGun.currentAmmo > 0)
            {
                Shoot();
                shootTime = Time.time;
            }
        }
        else if (currentGun.fireMethod == Gun.FireMethod.single) {
            if (currentGun.currentAmmo > 0)
            {
                Shoot();
                SetShootBool(false);
            }
        }

        
        
    }

    void Shoot() {
       
        if (currentGun.fireMethod == Gun.FireMethod.single || currentGun.fireMethod == Gun.FireMethod.continuous)
        {
            GameObject bullet = Instantiate(currentGun.bulletPrefab, muzzle.position, Quaternion.identity) as GameObject;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = FireMethods.StraightShot(muzzle.position, MousePosition()) * currentGun.bulletSpeed;
            StatTracker.shotsFired++;
            currentGun.currentAmmo--;
        }
        
    }

    void RotateArm() {
        Vector3 diff = MousePosition() - new Vector2(transform.position.x, transform.position.y);
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        arm.rotation = Quaternion.Euler(0f, 0f, rot_z - 45);
    }

    Vector2 MousePosition() {
        Vector2 mousePos = mainCam.ScreenToWorldPoint(gc.controls.Player.MousePosition.ReadValue<Vector2>());
        return mousePos;
    }
    void SetShootBool(bool newBool) {
        isShooting = newBool;
        shootTime = Time.time;
    }

    



}
