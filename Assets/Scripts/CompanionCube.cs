using UnityEngine;
using UnityEngine.UIElements;

public class CompanionCube : MonoBehaviour
{
    public Transform spawner;
    public float teleportOffset = 20.0f;
    public float teleportCooldown = 0.5f;
    private float teleportTimer;
    private bool teleportable;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        teleportTimer = teleportCooldown;
    }
    private void Update()
    {
        teleportTimer -= Time.deltaTime;
    }

    public void Restart()
    {
        transform.position = spawner.position;
    }

    public bool IsTeleportable()
    {
        return teleportable && teleportTimer <= 0;
    }
    public void SetTeleportable(bool state)
    {
        teleportable = state;
    }

    // Similar method when teleporting player, maybe can externalize
    public void Teleport(PortalController portal)
    {
        if (!IsTeleportable())
        {
            return;
        }
        teleportTimer = teleportCooldown;

        Vector3 movementDirection = rigidbody.velocity;
        movementDirection.Normalize();

        Vector3 position = transform.position + movementDirection * teleportOffset;
        Vector3 localPosition = portal.transform.InverseTransformPoint(position);
        Vector3 worldPosition = portal.mirrorPortal.transform.TransformPoint(localPosition);

        Vector3 forward = transform.forward;
        Vector3 localForward = portal.transform.InverseTransformDirection(forward);
        Vector3 worldForward = portal.mirrorPortal.transform.TransformDirection(-localForward);

        Vector3 localVelocity = portal.transform.InverseTransformDirection(rigidbody.velocity);
        Vector3 worldVelocity = portal.mirrorPortal.transform.TransformDirection(-localVelocity);

        float scale = portal.mirrorPortal.transform.localScale.x / portal.transform.localScale.x;

        rigidbody.isKinematic = true;
        rigidbody.transform.position = worldPosition;
        rigidbody.transform.rotation = Quaternion.LookRotation(worldForward);
        rigidbody.transform.localScale = Vector3.one * scale;
        rigidbody.isKinematic = false;
        rigidbody.velocity = worldVelocity;
    }
}