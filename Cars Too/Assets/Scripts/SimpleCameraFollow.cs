using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    public float hoverDistance = 6f;
    public float maxDistance = 15f;

    public Vector3 direction;
    public float currentDistance;
    public float adjustedDistance;

    public bool collisionDebug;
    public float collisionPadding = 0.35f;
    public LayerMask collisionMask;
    Ray camRay;
    RaycastHit camRaycastHit;

    void Awake()
    {
        currentDistance = maxDistance;
    }

    void LateUpdate()
    {
        CameraCollisions();

        transform.position = target.position;
        Quaternion targetRotation = Quaternion.Euler(0, target.rotation.eulerAngles.y, 0);
        transform.rotation = targetRotation;
        transform.Translate(new Vector3(0, hoverDistance, -currentDistance));

    }

    
    void CameraCollisions()
    {
        float camDistance = currentDistance + collisionPadding;

        camRay.origin = this.transform.position;
        camRay.direction = -(target.position - this.transform.position).normalized;

        if (collisionDebug)
        {
            Debug.DrawLine(camRay.origin, camRay.origin + camRay.direction * camDistance, Color.cyan);
        }

        if (Physics.Raycast(camRay, out camRaycastHit, camDistance, collisionMask))
        {
            Debug.Log("Collided with something");
            Debug.Log(camRaycastHit.collider);
            currentDistance = Vector3.Distance(camRay.origin, camRaycastHit.point) - collisionPadding;
        }
        else
        {
            currentDistance = maxDistance;
        }

        
            

    }
}
