using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float cameraFollowSpeed = 5f;
    private void LateUpdate()
    {
        if (player == null) return;
        var position = player.position;
        Vector3 targetPosition = new Vector3(position.x, position.y + 2, -10);
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);
    }
}