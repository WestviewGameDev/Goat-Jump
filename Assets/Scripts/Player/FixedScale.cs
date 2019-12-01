using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FixedScale : MonoBehaviour
{

    public float FixeScale = 1.229622f;
    public GameObject parent;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(FixeScale / parent.transform.localScale.x, FixeScale / parent.transform.localScale.y, FixeScale / parent.transform.localScale.z);

    }
}