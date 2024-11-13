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

    private bool CanShootPortal()
    {
        return timer <= 0 && attachObjectController.CanShoot();
    }

    public void ShootPortal(GameObject portal)
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, maxPortalDistance))
        {
            return;
        }

        if (!hit.collider.CompareTag("Wall"))
        {
            return;
        }

        portal.SetActive(true);
        PortalController portalController = portal.GetComponent<PortalController>();
        portalController?.SpawnIn(hit.point, Quaternion.LookRotation(hit.normal));
    }
}