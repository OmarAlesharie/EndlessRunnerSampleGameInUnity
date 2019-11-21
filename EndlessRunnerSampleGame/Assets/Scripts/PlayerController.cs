using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // To track the player position on the lane 
    enum Position
    {
        Left, Middle, Right
    }

    public float speed;                                 // Moving speed to the left or the right
    public float sideStepWidth;

    private Animator animator;
    private Vector3 targetPosition;                     // The target vector3 to nect position (left or right)
    private Position playerPosition = Position.Middle;

    public static Transform playerTransformPosision;    // The platforms need this information to get the direction of the player when the platform added to the world
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
        sideStepWidth = 2f;
    }

    /// <summary>
    /// Stop/Cancel the jumping otherwise the action keep repeating
    /// This function is triggered from the Jump animation event trigger
    /// </summary>
    void StopJumping()
    {
        animator.SetBool("isJumping", false);
    }

    /// <summary>
    /// Stop/Cancel the moving left or right otherwise the action keep repeating
    /// This function is triggered from the straff left/right animations event trigger
    /// </summary>
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
                StartCoroutine(MovePlayer(-1f * sideStepWidth));

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
                StartCoroutine(MovePlayer(1f * sideStepWidth));

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
