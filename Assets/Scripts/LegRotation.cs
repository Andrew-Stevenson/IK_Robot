using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRotation : MonoBehaviour
{
    [SerializeField] float minRotation;
    [SerializeField] float maxRotation;
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        float targetAngle = Vector2.Angle(new Vector2(0, 1), new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y));
        if (target.position.x > transform.position.x)
        {
            targetAngle = 360 - targetAngle;
        }
        targetAngle = Mathf.Clamp(targetAngle, minRotation, maxRotation);
        Quaternion correctedQuaternion = Quaternion.Euler(0, 0, targetAngle) * transform.parent.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, correctedQuaternion, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, minRotation) * new Vector2(0, 1.5f));
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, 0, maxRotation) * new Vector2(0, 1.5f));
    }
}
