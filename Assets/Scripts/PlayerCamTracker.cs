using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamTracker : MonoBehaviour
{
    public Transform playerCamera;
    public float yoffset = 17f;
    private float diff = 0f;
    void Start()
    {
        Vector3 curPos = playerCamera.position;
        diff = yoffset - curPos.y;
    }

    void Update()
    {
        Vector3 pos = playerCamera.position;
        pos.y += diff;

        this.transform.position = pos;
    }
}
