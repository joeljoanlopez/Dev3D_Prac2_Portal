using UnityEngine;

public class TeleportController : MonoBehaviour
{
    private Vector3 movementDirection;
    public float maxTeleportAngle = 20;
    private PortalController portalController;

    private void Start()
    {
        portalController = GetComponent<PortalController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        MovementController movementController = other.GetComponent<MovementController>();
        if (!movementController)
        {
            return;
        }
        movementDirection = movementController.MovementDirection;
        Teleport();
    }

    void Teleport()
    {
        float dotAngle = Vector3.Dot(movementDirection, portalController.otherPortal.transform.position);
        if (dotAngle >= Mathf.Cos(maxTeleportAngle * Mathf.Deg2Rad))
        {
            Vector3 offset = transform.position - portalController.transform.position;
            Vector3 newPosition = portalController.otherPortal.transform.position + offset;

            CharacterController characterController = GetComponent<CharacterController>();
            if (characterController != null)
            {
                characterController.enabled = false;
                characterController.transform.position = newPosition;
                characterController.enabled = true;

                Vector3 newDirection = portalController.otherPortal.transform.TransformDirection(
                    portalController.transform.InverseTransformDirection(movementDirection)
                );

                movementDirection = newDirection;
            }
        }
    }
}