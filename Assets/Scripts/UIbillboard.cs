using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIbillboard : MonoBehaviour
{
    private Transform body;
    private Transform cam;
    private Quaternion originalRotate;
    void Start()
    {
        body = this.transform;
        cam = Camera.main.transform;
        originalRotate = body.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        body.rotation = cam.rotation*originalRotate;
    }
}
