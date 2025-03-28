using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;          // Character to follow
    public Vector3 offset = new Vector3(0, 3, -6);
    public float followSpeed = 5f;
    public float lookSpeed = 5f;

    void Start()
    {
        // Try to find the Companion by name if not assigned
        if (target == null)
        {
            GameObject companion = GameObject.Find("Companion");
            if (companion != null)
                target = companion.transform;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Desired camera position
        Vector3 desiredPosition = target.position + offset;

        // Smoothly move camera
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Smoothly look at the target
        Quaternion lookRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, lookSpeed * Time.deltaTime);
    }
}
