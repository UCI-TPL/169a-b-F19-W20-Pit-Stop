using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{

    [SerializeField] float minimumDistance = 1.0f;
    [SerializeField] float maximumDistance = 4.0f;
    [SerializeField] float smoothing = 10f;

    private Vector3 zoomDirection;
    [SerializeField] float currentDistance;

    private SimpleCameraFollow cam;


    // Start is called before the first frame update
    void Awake()
    {
        zoomDirection = transform.localPosition.normalized;
        currentDistance = transform.localPosition.magnitude;
        cam = this.transform.parent.GetComponent<SimpleCameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(maximumDistance * zoomDirection);
        RaycastHit hit;

        if(Physics.Linecast (transform.parent.position, desiredCameraPos, out hit))
        {
            currentDistance = Mathf.Clamp((hit.distance * 0.8f), minimumDistance, maximumDistance);
            cam.maxDistance = currentDistance;
            
        }
        else
        {
            currentDistance = maximumDistance;
            cam.maxDistance = currentDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, zoomDirection * currentDistance, Time.deltaTime * smoothing);
    }
}
