using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    public float fixedYPosition; // Add this variable to set a fixed Y position
    Vector3 targetPos;

    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            // Set the y position to the fixed value
            posNoZ.y = fixedYPosition;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            // Use the fixed Y position in the final camera position
            transform.position = Vector3.Lerp(transform.position, new Vector3(targetPos.x, fixedYPosition, targetPos.z) + offset, 0.25f);
        }
    }
}