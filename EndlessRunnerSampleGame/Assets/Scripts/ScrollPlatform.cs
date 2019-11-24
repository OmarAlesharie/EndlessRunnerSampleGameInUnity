using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPlatform : MonoBehaviour
{
    private float ScrollSpeed = 3f;

    [SerializeField]
    bool destroyMe = false;     // To be used in editor for some platform that's not part of the actual gameplay for any reason!!
    [SerializeField]
    float destroyMeAfter = 5f;  // Delay time if the destroyMe is true

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
        } 
    }

    private void Update()
    {
        if (!PlayerController.isDead)
        {
            transform.position += PlayerController.playerTransformPosision.forward * -ScrollSpeed * Time.deltaTime;
        } 
    }

    void Destroyplatform()
    {
        Destroy(gameObject, destroyMeAfter);
    }
}
