using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class TeleportController : MonoBehaviour
{
    public float maxTeleportAngle = 20;
    public float teleportOffset = 0.5f;
    public float teleportCooldown = 0.5f;
    private float teleportTimer;

    private void Start()
    {
        teleportTimer = teleportCooldown;
    }

    private void Update()
    {
        teleportTimer -= Time.deltaTime;
    }

    public void Teleport(PortalController portalController)
    {
        if (teleportTimer > 0)
        {
            return;
        }
        teleportTimer = teleportCooldown;

        MovementController movementController = GetComponent<MovementController>();
        Vector3 movementDirection = movementController.MovementDirection;
        float dotAngle = Vector3.Dot(movementDirection, portalController.otherPortal.transform.position);
        if (dotAngle >= Mathf.Cos(maxTeleportAngle * Mathf.Deg2Rad))
        {
            Vector3 position = transform.position + movementDirection * teleportOffset;
            Vector3 localPosition = portalController.transform.InverseTransformPoint(position);
            Vector3 worldPosition = portalController.mirrorPortal.transform.TransformPoint(localPosition);

            Vector3 forward = transform.forward;
            Vector3 localForward = portalController.transform.InverseTransformDirection(forward);
            Vector3 worldForward = portalController.mirrorPortal.transform.TransformDirection(-localForward);

            Vector3 entranceRotation = transform.rotation.eulerAngles;
            CharacterController characterController = GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                transform.position = worldPosition;
                transform.forward = worldForward;
                characterController.enabled = true;

            }
        }
    }
}