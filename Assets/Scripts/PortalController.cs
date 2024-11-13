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
    public float cameraOffset = 0.6f;

    [Header("Other")]
    public Transform playerCamera;

    public void Update()
    {
        Vector3 localPosition = transform.InverseTransformPoint(playerCamera.transform.position);
        mirrorPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(localPosition);

        Vector3 localDirection = transform.InverseTransformDirection(playerCamera.transform.forward);
        mirrorPortal.portalCamera.transform.forward = otherPortal.transform.TransformDirection(localDirection);

        portalCamera.nearClipPlane = Vector3.Distance(portalCamera.transform.position, transform.position) + cameraOffset;
    }

    public void SpawnIn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }
}
