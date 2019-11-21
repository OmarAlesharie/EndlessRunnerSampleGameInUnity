using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPlatform : MonoBehaviour
{
    private float ScrollSpeed = 3f;

    private void Start()
    {
        transform.forward = PlayerController.playerTransformPosision.forward;
    }
    private void Update()
    {
        if (!PlayerController.isDead)
        {
            transform.Translate(0f, 0f, -ScrollSpeed * Time.deltaTime);
        } 
    }
}
