using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    public float hoverDistance = 6f;
    public float maxDistance = 15f;

    void LateUpdate()
    {
        transform.position = target.position;
        Quaternion targetRotation = Quaternion.Euler(0, target.rotation.eulerAngles.y, 0);
        transform.rotation = targetRotation;
        transform.Translate(new Vector3(0, hoverDistance, -maxDistance));
    }
}
