using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuyController : MonoBehaviour
{
    public PlayerControls controls;

    WaveManager wm;



    private void Awake()
    {
        controls = new PlayerControls();
        wm = FindObjectOfType<WaveManager>();
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
       
    }



}
