using UnityEngine;
using UnityEngine.UIElements;

public class CompanionCube : MonoBehaviour
{
    bool teleportable;
    Rigidbody rigidbody;
    public float teleportOffset;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public bool IsTeleportable()
    {
        return teleportable;
    }
    public void SetTeleportable(bool state)
    {
        teleportable = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (teleportable && other.CompareTag("Portal"))
        {
            Teleport(other.GetComponent<PortalController>());
        }
    }


    // Similar method when teleporting player, maybe can externalize
    void Teleport(PortalController portal)
    {
        Vector3 movementDirection = rigidbody.velocity;
        movementDirection.Normalize();

        Vector3 position = transform.position + movementDirection * teleportOffset;
        Vector3 localPosition = portal.otherPortal.transform.InverseTransformPoint(position);
        Vector3 worldPosition = portal.mirrorPortal.transform.TransformPoint(localPosition);

        Vector3 forward = transform.forward;
        Vector3 localForward = portal.otherPortal.transform.InverseTransformDirection(forward);
        Vector3 worldForward = portal.mirrorPortal.transform.TransformDirection(localForward);

        Vector3 localVelocity = portal.otherPortal.transform.InverseTransformDirection(rigidbody.velocity);
        Vector3 worldVelocity = portal.mirrorPortal.transform.TransformDirection(localVelocity);

        float scale = portal.mirrorPortal.transform.localScale.x / portal.transform.localScale.x;
        rigidbody.isKinematic = true;
        rigidbody.transform.position = worldPosition;
        rigidbody.transform.rotation = Quaternion.LookRotation(worldForward);
        rigidbody.transform.localScale = Vector3.one * scale;
        rigidbody.isKinematic = false;
        rigidbody.velocity = worldVelocity;
    }
}