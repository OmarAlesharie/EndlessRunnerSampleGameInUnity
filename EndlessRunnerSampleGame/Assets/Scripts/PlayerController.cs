using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // to track the player position on the lane 
    enum Position
    {
        Left, Middle, Right
    }

    public float speed;                                 // moving speed to the left or the right
    private Animator animator;
    private Vector3 targetPosition;                     // the target vector3 to nect position (left or right)
    private Position playerPosition = Position.Middle;

    public static Transform playerTransformPosision;
    public static bool isDead = false;

    private void Awake()
    {
        playerTransformPosision = this.gameObject.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        speed = 10f;
    }

    void StopJumping()
    {
        animator.SetBool("isJumping", false);
    }

    void StopMoving()
    {
        animator.SetBool("isMoveLeft", false);
        animator.SetBool("isMoveRight", false);
    }

    void StopSliding()
    {
        animator.SetBool("isSliding", false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (playerPosition == Position.Middle || playerPosition == Position.Right)
            {
                animator.SetBool("isMoveLeft", true);
                StartCoroutine(MovePlayer(-1f));

                switch (playerPosition)
                {
                    case Position.Middle:
                        playerPosition = Position.Left;
                        break;
                    case Position.Right:
                        playerPosition = Position.Middle;
                        break;
                }
            } 
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (playerPosition == Position.Middle || playerPosition == Position.Left)
            {
                animator.SetBool("isMoveRight", true);
                StartCoroutine(MovePlayer(1f));

                switch (playerPosition)
                {
                    case Position.Middle:
                        playerPosition = Position.Right;
                        break;
                    case Position.Left:
                        playerPosition = Position.Middle;
                        break;
                }
            }

            
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("isSliding", true);
        }
    }

    private IEnumerator MovePlayer(float value)
    {
        targetPosition = new Vector3(transform.position.x + value, transform.position.y, transform.position.z);
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
