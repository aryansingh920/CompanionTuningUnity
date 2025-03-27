using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CompanionController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 input = InputManager.MovementInput;
        Vector3 move = new Vector3(input.x, 0, input.y);

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
