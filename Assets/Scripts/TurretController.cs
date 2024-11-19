using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public LineRenderer laser;
    public LayerMask laserLayerMask;
    public float maxLaserDistance = 10.0f;
    public float maxAngleLaserAlive = 30.0f;
    private bool laserEnabled;

    private void Start()
    {
        laserEnabled = true;
    }

    private void Update()
    {
        if (IsLaserAlive())
        {
            Ray ray = new Ray(laser.transform.position, laser.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxLaserDistance, laserLayerMask))
            {
                laser.gameObject.SetActive(true);
                laser.SetPosition(0, laser.transform.position);
                laser.SetPosition(1, hit.point);
                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponentInParent<LifeController>().Die();
                }
                if (hit.collider.CompareTag("RefractionCube"))
                {
                    hit.collider.GetComponent<RefractionCube>()?.ReflectLaser();
                }
                if (hit.collider.CompareTag("Turret"))
                {
                    hit.collider.GetComponent<TurretController>().DisableLaser();
                }
            }
            else
            {
                laser.gameObject.SetActive(true);
                laser.SetPosition(0, laser.transform.position);
                laser.SetPosition(1, laser.transform.position + transform.forward * maxLaserDistance); // Extend to max distance
            }
        }
        else
        {
            laser.gameObject.SetActive(false);
        }
    }

    bool IsLaserAlive()
    {
        return Vector3.Dot(transform.up, Vector3.up) > Mathf.Cos(maxAngleLaserAlive * Mathf.Deg2Rad) && laserEnabled;
    }

    public void DisableLaser()
    {
        laserEnabled = false;
    }
}