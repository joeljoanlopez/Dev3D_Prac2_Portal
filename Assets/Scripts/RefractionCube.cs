using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractionCube : MonoBehaviour
{
    public LineRenderer m_laser;
    public LayerMask m_PhysicsLayerMask;
    public float m_MaxDistance = 50.0f;
    bool m_CreateRefraction = false;

    void Update()
    {
        m_laser.gameObject.SetActive(m_CreateRefraction);
        m_CreateRefraction = false;
    }

    public void ReflectLaser()
    {
        if (m_CreateRefraction)
            return;

        m_CreateRefraction = true;
        Ray l_Ray = new Ray(m_laser.transform.position, m_laser.transform.forward);

        if (Physics.Raycast(l_Ray, out RaycastHit l_RaycastHit, m_MaxDistance, m_PhysicsLayerMask.value))
        {
            m_laser.SetPosition(1, new Vector3(0.0f, 0.0f, l_RaycastHit.distance));
            m_laser.gameObject.SetActive(true);

            if (l_RaycastHit.collider.CompareTag("RefractionCube"))
            {
                l_RaycastHit.collider.GetComponent<RefractionCube>().ReflectLaser();
            }
        }
        else
            m_laser.gameObject.SetActive(false);
    }
}