using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public PortalController portalController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<TeleportController>()?.Teleport(portalController);
        }
        else if (other.CompareTag("Cube"))
        {
            other.GetComponent<CompanionCube>()?.Teleport(portalController);
        }
    }
}