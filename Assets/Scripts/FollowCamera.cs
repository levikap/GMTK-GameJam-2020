using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public float interpVelocity;
    public float minDistance;
    public float followDistance;

    private GameObject target;
    public float smoothSpeed = 6f;
    public Vector3 offset;

    public float leftBound = -4.3f;
     public float rightBound = 4.3f;
     public float upBound = 2.7f;
     public float downBound = -2.7f;

    Vector2 targetPos;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetPos = transform.position;
    }


    void FixedUpdate()
    {
        // X axis
        if (transform.position.x <= leftBound)
        {
            transform.position = new Vector2(leftBound, transform.position.y);
        }
        else if (transform.position.x >= rightBound)
        {
            transform.position = new Vector2(rightBound, transform.position.y);
        }

        // Y axis
        if (transform.position.y <= downBound)
        {
            transform.position = new Vector2(transform.position.x, downBound);
        }
        else if (transform.position.y >= upBound)
        {
            transform.position = new Vector2(transform.position.x, upBound);
        }

        if (target && GameState.isGameOver == false)
        {
            //Vector3 posNoZ = transform.position;
            //posNoZ.z = target.transform.position.z;

            //Vector3 targetDirection = (target.transform.position - posNoZ);

            //interpVelocity = targetDirection.magnitude * 5f;

            //targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            //transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

            Vector3 desiredPosition = target.transform.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            smoothedPosition = smoothedPosition + new Vector3(0f, 0f, -10f);
            transform.position = smoothedPosition;
        }
    }
}