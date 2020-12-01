using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBehaviour : MonoBehaviour
{

    [SerializeField] Transform feetNextTargets;
    [Tooltip("The speed of motion")] [SerializeField] float speed;
    [Tooltip("The distance that the feet will be offset from the body in the direction of motion")] [SerializeField] float maxBodyOffset;
    [Tooltip("The height between the feet and the body")] [SerializeField] float bodyHeightOffset;

    [SerializeField] Transform[] feetCurrentTargets;

    [Header("Feet Setup")]

    [Tooltip("The height offset of a foot whilst it's steping")] public AnimationCurve yCurve;
    [Tooltip("The max distance between a foot and it's target before it steps")] public float stepDistance;
    [Tooltip("The x speed of the foot when it steps")]  public float stepSpeed;

    float movement;

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal") * speed;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector2(movement * Time.fixedDeltaTime + transform.position.x, transform.position.y);
        feetNextTargets.localPosition = new Vector2(feetNextTargets.localPosition.x, Mathf.Clamp(-movement , -maxBodyOffset, maxBodyOffset));

        float maxFootHeight = Mathf.NegativeInfinity;
        Vector2 lowestFootPosition = new Vector2(0, Mathf.Infinity);

        foreach (Transform target in feetCurrentTargets)
        {
            maxFootHeight = Mathf.Max(maxFootHeight, target.position.y);
        }

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, maxFootHeight + bodyHeightOffset), 0.01f);
    }
}
