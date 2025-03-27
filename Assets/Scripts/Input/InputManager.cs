using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Vector2 MovementInput { get; private set; }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        MovementInput = new Vector2(h, v).normalized;
    }
}
