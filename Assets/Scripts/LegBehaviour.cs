using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegBehaviour : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform foot;

    [SerializeField] LegBehaviour partnerLeg;

    [SerializeField] RobotBehaviour mainBody;


    float speed;
    float stepDistance = 5f;
    AnimationCurve yCurve;

    float desiredYPosition;
    float dist;
    float fullStepDist = 1;
    float distLeftToTravel;

    [HideInInspector] public bool inAir = false;

    private void Start()
    {
        speed = mainBody.stepSpeed;
        yCurve = mainBody.yCurve;
        stepDistance = mainBody.stepDistance;

        foot.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        dist = Vector2.Distance(foot.position, transform.position);
        if (dist > 0.1f)
        {
            distLeftToTravel = foot.position.x - transform.position.x;

            float yOffset = fullStepDist != 0 ? yCurve.Evaluate(distLeftToTravel / fullStepDist) : 0;
            foot.position = Vector2.MoveTowards(foot.position, new Vector2(transform.position.x, transform.position.y + yOffset), speed * Time.deltaTime);

            inAir = true;
        }
        else
        {
            foot.position = transform.position;

            inAir = false;
        }

        RaycastHit2D hit = Physics2D.Raycast(new
        Vector2(target.position.x, transform.position.y + 5), -Vector2.up, 12f, LayerMask.GetMask("Ground"));

        // If we hit a collider, set the desiredYPosition to the hit Y point.        
        if (hit.collider != null)
        {
            desiredYPosition = hit.point.y;
        }
        else
        {
            desiredYPosition = transform.position.y;
        }

        target.position = new Vector2(target.position.x, desiredYPosition);

        dist = Vector2.Distance(target.position, transform.position);
        if ((dist > stepDistance || Vector2.Distance(foot.position, mainBody.gameObject.transform.position) > 2.1f) && !partnerLeg.inAir)
        {
            fullStepDist = transform.position.x - target.position.x;
            transform.position = target.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 0.07f);
    }
}
