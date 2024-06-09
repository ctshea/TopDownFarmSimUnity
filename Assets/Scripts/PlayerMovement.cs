using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    

    private float horizontal;
    private float vertical;

    private string playerDirection;
    private bool changedDir;


    // Start is called before the first frame update
    void Start()
    {
        playerDirection = "";
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Update()
    {
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);

        if (horizontal > 0f)
        {
            if (playerDirection != "right") changedDir = true;
            else
            {
                changedDir = false;
            }
            playerDirection = "right";
        }

        else if (horizontal < 0f)
        {
            if (playerDirection != "left") changedDir = true;
            else
            {
                changedDir = false;
            }
            playerDirection = "left";
        }

        else if (vertical > 0f)
        {
            /*if (horizontal < 0f)
            {
                //Debug.Log("UP AND TO THE LEFT:   " + playerDirection);
                if (playerDirection != "left") changedDir = true;
                else changedDir = false;
                playerDirection = "left";
            }
            else if (horizontal > 0f)
            {
                //Debug.Log("up AND TO THE RIGHT:   " + playerDirection);
                if (playerDirection != "right") changedDir = true;
                else changedDir = false;
                playerDirection = "right";
            }
            else*/            //just up
            {
                //Debug.Log("UP ONLY:   " + playerDirection);
                if (playerDirection != "up") changedDir = true;
                else changedDir = false;
                playerDirection = "up";
            }
            
        }

        else if (vertical < 0f)
        {
            if (playerDirection != "down") changedDir = true;
            else
            {
                changedDir = false;
            }
            playerDirection = "down";
        }

    }

    public string getPlayerDirection()
    {
        return playerDirection;
    }

    public bool getPlayerTurned()
    {
        return changedDir;
    }
   
}
