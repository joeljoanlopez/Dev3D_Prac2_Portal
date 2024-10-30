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
    public Camera camera;
    public float cameraOffset = 0.6f;

    [Header("Other")]
    public MovementController movementController;

    // Update is called once per frame
    public void Update()
    {
        Vector3 position = movementController.transform.position;
        Vector3 forward = movementController.transform.position;

        Vector3 localPosition = otherPortal.InverseTransformPoint(position);
        Vector3 localForward = otherPortal.InverseTransformDirection(forward);

        Vector3 worldPosition = mirrorPortal.transform.TransformPoint(localPosition);
        Vector3 worldForward = mirrorPortal.transform.TransformDirection(localForward);

        mirrorPortal.camera.transform.position = worldPosition;
        mirrorPortal.camera.transform.forward = worldForward;


        float distanceToPortal = Vector3.Distance(worldPosition, mirrorPortal.transform.position);
        float distanceNearClipPlane = cameraOffset + distanceToPortal;
        mirrorPortal.camera.nearClipPlane = distanceNearClipPlane;
    }

    public void Shoot(Vector3 target)
    {
        transform.position = target;
    }
}
