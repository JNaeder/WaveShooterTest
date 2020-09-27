using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyMovement : MonoBehaviour
{
    public float speed = 5f;

    GuyController gc;
    GameManager gm;

    private void Awake()
    {
        gc = GetComponent<GuyController>();
        gm = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gm.currentGameState == GameManager.GameState.offWave || gm.currentGameState == GameManager.GameState.onWave)
        {
            Movement();
        }
    }

    void Movement() {
        Vector2 moveDir = gc.controls.Player.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * speed * Time.deltaTime;
    }
}
