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

    private void Start()
    {
        timer = coolDown;
        playerCamera = Camera.main;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public void OnBluePortal()
    {
        if (timer <= 0f)
        {
            ShootPortal(bluePortal);
            timer = coolDown;
        }
    }

    public void OnOrangePortal()
    {
        if (timer <= 0f)
        {
            ShootPortal(orangePortal);
            timer = coolDown;
        }
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