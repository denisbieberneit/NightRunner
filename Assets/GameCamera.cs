﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    public Vector3 offset;
    public Transform target;
    public float smoothSpeed = 0.125f;


    private void FixedUpdate()
    {
        if(target != null)
        {
            Vector3 desiredPos = new Vector3(target.position.x, target.position.y, -30) + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothedPos;
        }
    }
}
