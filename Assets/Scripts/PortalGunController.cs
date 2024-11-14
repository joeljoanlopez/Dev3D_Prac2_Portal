using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGunController : MonoBehaviour
{
    [Header("Portals")]
    public GameObject bluePortal;
    public GameObject orangePortal;

    [Header("Stats")]
    public float coolDown = 0.5f;
    private float timer;
    public float maxPortalDistance = 100f;
    private Camera playerCamera;
    private AttachObjectController attachObjectController;

    private void Start()
    {
        timer = coolDown;
        playerCamera = Camera.main;
        attachObjectController = GetComponent<AttachObjectController>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public void OnBluePortal()
    {
        if (CanShootPortal())
        {
            ShootPortal(bluePortal);
            timer = coolDown;
        }
    }

    public void OnOrangePortal()
    {
        if (CanShootPortal())
        {
            ShootPortal(orangePortal);
            timer = coolDown;
        }
    }

    void OnAttach()
    {
        if (CanShootPortal())
        {
            RaycastHit hit = ShootRay();
            if (!hit.collider.CompareTag("Spawner"))
            {
                return;
            }
            Debug.Log("Ray shot");
            hit.collider.GetComponent<CompanionSpawnerController>().SpawnCube();
        }
    }

    private bool CanShootPortal()
    {
        return timer <= 0 && attachObjectController.CanShoot();
    }

    public void ShootPortal(GameObject portal)
    {
        RaycastHit hit = ShootRay();
        if (!hit.collider.CompareTag("Wall"))
        {
            return;
        }

        if (!portal.activeSelf)
        {
            portal.SetActive(true);
        }

        portal.SetActive(true);
        PortalController portalController = portal.GetComponent<PortalController>();
        portalController?.SpawnIn(hit.point, Quaternion.LookRotation(hit.normal));
    }

    private RaycastHit ShootRay()
    {

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, maxPortalDistance);
        return hit;
    }
}