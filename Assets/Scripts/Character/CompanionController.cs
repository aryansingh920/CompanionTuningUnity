using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CompanionController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 input = InputManager.MovementInput;
        Vector3 move = new Vector3(input.x, 0, input.y);

        // Vector3 gravity = Vector3.down * 9.81f;

        controller.Move(move.normalized * moveSpeed * Time.deltaTime);
        // controller.Move((move * moveSpeed + gravity) * Time.deltaTime);

        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), 0.15f);
        }

        bool isWalking = move.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);
    }
}
