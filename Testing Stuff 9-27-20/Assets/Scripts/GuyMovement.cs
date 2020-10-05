using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuyMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform leftCheck, rightCheck, topCheck, bottomCheck;
    public LayerMask objectLayer;

    

    GuyController gc;
    GameManager gm;

    private void Awake()
    {
        gc = GetComponent<GuyController>();
        gm = FindObjectOfType<GameManager>();
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
        transform.position += WallChecks(MoveDir()) * speed * Time.deltaTime;
    }


    public Vector2 MoveDir() {
        Vector2 moveDir = gc.controls.Player.Movement.ReadValue<Vector2>();
        return moveDir;
    }


    Vector3 WallChecks(Vector2 moveDirection) {
        Vector3 newVector = new Vector3(moveDirection.x,moveDirection.y,0);

        //Left
        if (Physics2D.OverlapBox(leftCheck.position, new Vector2(0.1f, 0.8f), 0, objectLayer)){
            newVector.x = Mathf.Clamp(newVector.x, 0, 1);
        }
        //Right
        if (Physics2D.OverlapBox(rightCheck.position, new Vector2(0.1f, 0.8f), 0, objectLayer)){
            newVector.x = Mathf.Clamp(newVector.x, -1, 0);
        }
        //Top
        if (Physics2D.OverlapBox(topCheck.position, new Vector2(0.8f, 0.1f), 0, objectLayer)){
            newVector.y = Mathf.Clamp(newVector.y, -1, 0);
        }
        //Bottom
        if (Physics2D.OverlapBox(bottomCheck.position, new Vector2(0.8f, 0.1f), 0, objectLayer)){
            newVector.y = Mathf.Clamp(newVector.y, 0, 1);
        }

        return newVector;


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Left
        Gizmos.DrawWireCube(leftCheck.position, new Vector2(0.1f, 0.8f));
        // Right
        Gizmos.DrawWireCube(rightCheck.position, new Vector2(0.1f, 0.8f));
        // Top
        Gizmos.DrawWireCube(topCheck.position, new Vector2(0.8f, 0.1f));
        // Bottom
        Gizmos.DrawWireCube(bottomCheck.position, new Vector2(0.8f, 0.1f));

    }

}
