using System;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public LineRenderer laser;
    public LayerMask laserLayerMask;
    public float maxLaserDistance;
    public float maxAngleLaserAlive = 10;

    private void Update()
    {
        if (IsLaserAlive())
        {
            Ray ray = new Ray(laser.transform.position, laser.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, maxLaserDistance))
            {
                laser.gameObject.SetActive(true);
                laser.SetPosition(1, new Vector3(0.0f, 0.0f, hit.distance));
                if (hit.collider.CompareTag("RefractionCube"))
                {
                    hit.collider.GetComponent<RefractionCube>().ReflectLaser();
                }
            }
            else
            {
                laser.gameObject.SetActive(false);
            }
        }
        laser.gameObject.SetActive(false);
    }

    bool IsLaserAlive()
    {
        return Vector3.Dot(transform.up, Vector3.up) > Mathf.Cos(maxAngleLaserAlive * Mathf.Deg2Rad);
    }
}