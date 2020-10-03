using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "New Gun")]
public class Gun : ScriptableObject
{
    public GunObject gunObject;

    public string gunName;
    public Sprite gunImage;

    public int startAmmo;
    public int maxAmmo;
    public int currentAmmo;
    public int clipCapcity;
    public int currentClip;
    public float reloadTime;

    public float bulletSpeed;

    [Range(1, 20)]
    public float fireRate = 1f;

    public GameObject bulletPrefab;

    public enum FireMethod {Single, Automatic, Burst, Steady}
    public BulletMethod bulletMethod;
    public enum BulletMethod {Single, Multi, Steady}
    public FireMethod fireMethod;

    public float multiShotSpreadAngle;
    public int multiShotNumberOfShots;

    public int burstAmount;




}
