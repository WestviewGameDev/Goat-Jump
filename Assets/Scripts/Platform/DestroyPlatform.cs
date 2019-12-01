using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{

    private float currentTime = 0f;

    public float totalTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > totalTime)
        {
            gameObject.SetActive(false);
        }


        currentTime += Time.deltaTime;
    }


}
