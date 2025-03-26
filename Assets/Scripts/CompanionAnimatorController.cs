using UnityEngine;

public class CompanionController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 2.0f;

    private bool isWalking = false;

    void Update()
    {
        // Check input
        if (Input.GetKey(KeyCode.W))
        {
            WalkForward();
        }
        else
        {
            StopWalking();
        }
    }

    void WalkForward()
    {
        if (!isWalking)
        {
            animator.Play("Walking"); // Must match the name of your animation state
            isWalking = true;
        }

        // Move the character forward (in local space)
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void StopWalking()
    {
        if (isWalking)
        {
            animator.Play("Sitting Talking"); // Default idle or sitting state
            isWalking = false;
        }
    }
}
