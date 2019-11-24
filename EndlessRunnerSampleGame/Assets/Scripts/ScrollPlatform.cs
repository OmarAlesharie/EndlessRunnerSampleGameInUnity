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

    private void OnEnable()
    {
        // The check here is needed for the first instantiated platform as PlayerController.playerTransformPosision could be null
        if (PlayerController.playerTransformPosision != null)
        {
            transform.forward = PlayerController.playerTransformPosision.forward;
            Debug.Log("I'm " + gameObject.tag.ToString() + " and current player forward: " + PlayerController.playerTransformPosision.forward + " with rotation: "
                + PlayerController.playerTransformPosision.rotation);
        } 
    }

    private void Update()
    {
        if (!PlayerController.isDead)
        {
            transform.position += PlayerController.playerTransformPosision.forward * -ScrollSpeed * Time.deltaTime;
        } 
    }
}
