using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject followTarget;
    Vector3 offsetVector = new Vector3(10, 14, -10);

    // Update is called once per frame
    void Update()
    {
        if (followTarget != null) Follow();
    }

    void Follow()
    {
        transform.position = followTarget.transform.position + offsetVector;
        transform.LookAt(followTarget.transform.position);
    }
    public void SetFollowTarget(GameObject target)
    {
        followTarget = target;
    }
}
