using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float playerSpeed;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&& rb.velocity.y < 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        /*
        if(!LeftValidMove()||!RightValidMove())
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        */
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-1 * playerSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(playerSpeed, rb.velocity.y);
        }
    }
    private bool LeftValidMove()
    {
        foreach (Transform children in transform)
        {
            float roundedX = children.transform.position.x;
            float roundedY = children.transform.position.y;
            //int roundedX = Mathf.RoundToInt(children.transform.position.x);
            //int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX < 0 || roundedX >= TetrisBlock.width || roundedY < 0 || roundedY >= TetrisBlock.height)
            {
                return false;
            }
            /*
            if (children.transform.localPosition.y == 0f && TetrisBlock.grid[roundedX, roundedY - 1] == null)
            {
                return false;
            }
            if (TetrisBlock.grid[roundedX, roundedY] != null)
            {
                return false;
            }
            */
        }
        return true;
    }
    private bool RightValidMove()
    {
        foreach (Transform children in transform)
        {
            float roundedX = children.transform.position.x;
            float roundedY = children.transform.position.y;
            //int roundedX = Mathf.RoundToInt(children.transform.position.x);
            //int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedX > TetrisBlock.width)
                return false;
            if (roundedX < 0 || roundedX >= TetrisBlock.width || roundedY < 0 || roundedY >= TetrisBlock.height)
            {
                return false;
            }
            /*
            if (children.transform.localPosition.y == 0f && TetrisBlock.grid[roundedX, roundedY - 1] == null)
            {
                return false;
            }
            
            if (TetrisBlock.grid[roundedX, roundedY] != null)
            {
                return false;
            }
            */
        }
        return true;
    }
}
