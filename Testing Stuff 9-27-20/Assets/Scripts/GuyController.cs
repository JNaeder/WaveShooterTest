using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GuyController : MonoBehaviour
{
    public PlayerControls controls;

    Animator anim;

    WaveManager wm;
    GuyMovement guyM;
    GuyShooting gs;



    private void Awake()
    {
        controls = new PlayerControls();
        wm = FindObjectOfType<WaveManager>();
        guyM = GetComponent<GuyMovement>();
        gs = GetComponent<GuyShooting>();
        anim = GetComponent<Animator>();
        
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
        UpdateAnimator();   
    }

    void UpdateAnimator() {
        anim.SetFloat("x", gs.MousePosition().x - transform.position.x);
        anim.SetFloat("y", gs.MousePosition().y - transform.position.y);

    }



}
