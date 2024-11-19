using UnityEngine;

public class AttachObjectController : MonoBehaviour
{
    public Camera cam;

    [Header("AttachObjects")]
    public Transform attachTransform;
    public float attachObjectSpeed = 10.0f;
    public float startRotatingDistance = 1.0f;
    public float detachObjectForce = 20.0f;
    public float minAttachDistance = 1.0f;
    bool attachingObject;
    bool attachedObject;
    private Rigidbody attachObjectRigidBody;
    private Transform attachedObjectPreviousParent;

    // Input management
    private void OnAttach()
    {
        AttachObject();
    }

    private void OnBluePortal()
    {
        if (!CanShoot())
        {
            DetachObject(detachObjectForce);
        }
    }

    private void OnOrangePortal()
    {
        if (!CanShoot())
        {
            DetachObject(0.0f);
        }
    }

    // Controller Logic

    public bool CanShoot()
    {
        return !(attachedObject || attachingObject);
    }

    private void Update()
    {
        if (attachingObject && attachObjectRigidBody != null)
        {
            UpdateAttachingObject();
        }
    }

    void AttachObject()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }
        if (!hit.collider.CompareTag("Cube") && !hit.collider.CompareTag("Turret"))
        {
            return;
        }

        AttachObject(hit.transform.GetComponent<Rigidbody>());
    }

    void AttachObject(Rigidbody objectRigidBody)
    {
        attachObjectRigidBody = objectRigidBody;
        attachObjectRigidBody.isKinematic = true;
        attachingObject = true;
        attachedObject = false;
        attachedObjectPreviousParent = attachObjectRigidBody.transform.parent;
        attachObjectRigidBody.GetComponent<CompanionCube>().SetTeleportable(false);
    }

    public void DetachObject(float force)
    {
        attachObjectRigidBody.transform.SetParent(attachedObjectPreviousParent);
        attachObjectRigidBody.isKinematic = false;
        attachObjectRigidBody.velocity = attachTransform.forward * force;
        attachingObject = false;
        attachedObject = false;
        attachObjectRigidBody.GetComponent<CompanionCube>().SetTeleportable(true);
        attachObjectRigidBody.GetComponent<TurretController>()?.DisableLaser();
    }

    void UpdateAttachingObject()
    {
        Vector3 direction = attachTransform.position - attachObjectRigidBody.position;
        float distance = direction.magnitude;
        direction /= distance;
        float movement = attachObjectSpeed * Time.deltaTime;

        if (movement >= distance || distance <= minAttachDistance)
        {
            attachedObject = true;
            attachingObject = false;
            attachObjectRigidBody.transform.SetParent(attachTransform);
            attachObjectRigidBody.transform.localPosition = Vector3.zero;
            attachObjectRigidBody.transform.localRotation = Quaternion.identity;
        }
        else
        {
            attachObjectRigidBody.transform.position += movement * direction;
            float pct = Mathf.Min(1.0f, distance / startRotatingDistance);
            attachObjectRigidBody.transform.rotation = Quaternion.Lerp(attachTransform.rotation, attachObjectRigidBody.transform.rotation, pct);
        }
    }
}