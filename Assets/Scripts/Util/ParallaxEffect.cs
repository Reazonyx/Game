using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    private Vector2 startingPosition;
    private float startingZ;
    private Vector2 cameraMoveSinceStart => (Vector2) cam.transform.position - startingPosition;
    private float distanceZfromTarget => transform.position.z - followTarget.position.z;

    private float clippingPlane =>
        (cam.transform.position.z + (distanceZfromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    private float parallaxFactor => Mathf.Abs(distanceZfromTarget) / clippingPlane;
    

    private void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPosition = startingPosition + cameraMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
