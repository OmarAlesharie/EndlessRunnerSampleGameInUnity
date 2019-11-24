using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNextPlatform : MonoBehaviour
{
    [SerializeField]
    Transform NextLocation;

    private GameObject nextPlatform;
    private Vector3 NextPlatformLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nextPlatform = PoolPlatforms.singleton.GetRandom();
            if (nextPlatform!=null)
            {
                nextPlatform.transform.position = NextLocation.position;
                nextPlatform.SetActive(true);
            } 
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
