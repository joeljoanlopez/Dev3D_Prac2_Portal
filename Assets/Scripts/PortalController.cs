using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [Header("Portal")]
    public Transform otherPortal;
    public PortalController mirrorPortal;

    [Header("Camera")]
    public Camera portalCamera;
    public float cameraOffset = 1.0f;
    public float minClipDistance = 0.1f;
    public float maxClipDistance = 1.0f;

    [Header("Other")]
    public GameObject player;

    public void Update()
    {
        Vector3 playerPosition = otherPortal.InverseTransformPoint(portalCamera.transform.position);
        mirrorPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(playerPosition);

        Vector3 playerDirection = otherPortal.InverseTransformDirection(portalCamera.transform.forward);
        mirrorPortal.portalCamera.transform.forward = otherPortal.transform.TransformDirection(playerDirection);

        portalCamera.nearClipPlane = Vector3.Distance(portalCamera.transform.position, transform.position);
    }

    public void SpawnIn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
