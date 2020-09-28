using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "New Gun")]
public class Gun : ScriptableObject
{
    public string gunName;
    public Sprite gunImage;

    public int maxAmmo;
    public int currentAmmo;
    public float bulletSpeed;

    [Range(1, 20)]
    public float fireRate = 1f;

    public GameObject bulletPrefab;

    public enum FireMethod {single, continuous, threeShot}
    public FireMethod fireMethod;

    public bool holdShot;

    

    
}
