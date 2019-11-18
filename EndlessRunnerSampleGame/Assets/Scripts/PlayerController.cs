using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void StopJumping()
    {
        animator.SetBool("isJumping", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("isJumping", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
    }
}
