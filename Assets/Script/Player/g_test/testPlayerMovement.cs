using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpSpeed;

    [SerializeField]
    private bool canJump;
    private bool canMove;
    private UnityJellySprite jellySprite;
    private bool GetKeyJump;
    private int testGetKeyMove;

    testPlayerStick playerStick;

    testPlayerFloorDetect playerFloorDetect;


    void Start()
    {
        jellySprite = GetComponent<UnityJellySprite>();
        playerStick = gameObject.GetComponentInChildren<testPlayerStick>();
        playerFloorDetect = gameObject.GetComponentInChildren<testPlayerFloorDetect>();
        // item = null;
    }
    void Update()
    {
        GetKeyJump = Input.GetButtonDown("Jump_" + this.tag) || Input.GetKeyDown("z");

        if (Input.GetKeyDown("right")) testGetKeyMove = 1;
        else if (Input.GetKeyDown("left")) testGetKeyMove = -1;


        canJump = playerFloorDetect.isOnFloor;
        canMove = playerFloorDetect.isOnFloor;
    }

    void FixedUpdate()
    {
        if (!playerStick.isStick)
        {
            Move();
            Jump();
        }

    }
    void Move()
    {
        if (canMove)
            jellySprite.AddVelocity(new Vector2(testGetKeyMove * moveSpeed, 0.0f));
        // jellySprite.AddVelocity(new Vector2(Input.GetAxisRaw("Horizontal_" + this.tag) * moveSpeed, 0.0f));
    }

    void Jump()
    {
        if (GetKeyJump && canJump)
        {
            canJump = false;
            jellySprite.AddForce(new Vector2(0, jumpSpeed));
        }
    }

}
