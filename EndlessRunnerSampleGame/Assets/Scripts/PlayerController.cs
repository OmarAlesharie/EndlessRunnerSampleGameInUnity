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

    public float speed = 10f;                           // Moving speed to the left or the right
    public float sideStepWidth = 2f;

    private Animator animator;
    private Vector3 targetPosition;                     // The target vector3 to nect position (left or right)
    private Position playerPosition = Position.Middle;
    private bool canChangeDirection = false;
    private bool AlreadyChangeDirection = false;        // Control change the direction only once in TSection
    private Vector3 changeDirectionPoint;               // The point of the trigger that allow change direction;

    public static Transform playerTransformPosision;    // The platforms need this information to get the direction of the player when the platform added to the world
    public static bool isDead = false;
    public static string currentPlatform = "null";      // The current platfrom the player standing on, this will guide the platform to rise up or down

    private int RandJump;

    private void Awake()
    {
        playerTransformPosision = this.gameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            animator.SetBool("isWallHit", true);
            isDead = true;
            return;
        }

        if (collision.gameObject.CompareTag("Platfrom_Tsection"))
        {
            AlreadyChangeDirection = false;
        }

        currentPlatform = collision.gameObject.tag;
    }

    // enable change direction with arrow keys rather change lane
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ChangeDirectionTrigger") && !AlreadyChangeDirection)
        {
            canChangeDirection = true;
            changeDirectionPoint = other.gameObject.transform.position;
        }

        if (other.gameObject.CompareTag("Left_Lane"))
        {
            CorrectPositionAfterTurning(other);
            playerPosition = Position.Left;
        }

        if (other.gameObject.CompareTag("Middle_Lane"))
        {
            CorrectPositionAfterTurning(other);
            playerPosition = Position.Middle;
        }

        if (other.gameObject.CompareTag("Right_Lane"))
        {
            CorrectPositionAfterTurning(other);
            playerPosition = Position.Right;
        }
    }

    /// <summary>
    /// Correct player position when triggering with the guide boxes after turning
    /// </summary>
    /// <param name="other"></param>
    private void CorrectPositionAfterTurning(Collider other)
    {
        Vector3 positionCorrector = other.transform.position;
        positionCorrector.y = transform.position.y;
        transform.position = positionCorrector;
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

    /// <summary>
    /// Stop/Cancel sliding otherwise the action keep repeating
    /// This function is triggered from the Running Slide animations event trigger
    /// </summary>
    void StopSliding()
    {
        animator.SetBool("isSliding", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RandJump = UnityEngine.Random.Range(0, 2);
            animator.SetInteger("JumpAnimation", RandJump);
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (canChangeDirection)
            {
                playerTransformPosision = this.gameObject.transform;    // Update player transform variable to the platforms
                changeDirectionPoint.y = transform.position.y;          // Set the location to the same height as the player
                transform.position = changeDirectionPoint;              // Take the trigger location to keep the player at the correct lane
                transform.Rotate(Vector3.up * -90);
                canChangeDirection = false;
                AlreadyChangeDirection = true;                          // No need to change the direction more than once
                return;                                                 // No need to process the key down farther until the next key down
            }

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
                if (canChangeDirection)
                {
                    playerTransformPosision = this.gameObject.transform;    // Update player transform variable to the platforms
                    changeDirectionPoint.y = transform.position.y;          // Set the location to the same height as the player
                    transform.position = changeDirectionPoint;              // Take the trigger location to keep the player at the correct lane
                    transform.Rotate(Vector3.up * 90);
                    canChangeDirection = false;
                    AlreadyChangeDirection = true;                          // No need to change the direction more than once
                    return;                                                 // No need to process the key down farther until the next key down
                }

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
        targetPosition = transform.position + transform.right * value;
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
