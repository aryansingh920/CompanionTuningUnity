using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Vector2 MovementInput { get; private set; }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow
        float v = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow

        MovementInput = new Vector2(h, v).normalized;
    }
}
