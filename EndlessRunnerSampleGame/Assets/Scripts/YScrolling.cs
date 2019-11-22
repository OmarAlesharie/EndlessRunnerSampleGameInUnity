using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YScrolling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerController.currentPlatform == "Platfrom_Downstairs")
        {
            transform.Translate(0f, 0.06f, 0f);
        }
        else if (PlayerController.currentPlatform == "Platfrom_Upstairs")
        {
            transform.Translate(0f, -0.06f, 0f);
        }
    }
}
