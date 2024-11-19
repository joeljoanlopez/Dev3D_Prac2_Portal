using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionCube : MonoBehaviour
{
    public LineRenderer laser;
    public LayerMask layerMask;
    public float maxDistance = 50.0f;
    bool createRefraction = false;

    private void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    void Update()
    {
        laser.gameObject.SetActive(createRefraction);
        createRefraction = false;
    }

    public void ReflectLaser()
    {
        if (createRefraction)
            return;

        createRefraction = true;
        Ray ray = new Ray(laser.transform.position, laser.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask.value))
        {
            laser.SetPosition(1, new Vector3(0.0f, 0.0f, hit.distance));
            laser.gameObject.SetActive(true);

            if (hit.collider.CompareTag("RefractionCube"))
            {
                hit.collider.GetComponent<RefractionCube>().ReflectLaser();
            }
        }
        else
            laser.gameObject.SetActive(false);
    }
}