using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float gravityForce = -3;
    [SerializeField] float sandartVelocity;

    private Vector3 _velocity;
    private Vector2 _keyboardInput;

    void Update()
    {
        float velocity = sandartVelocity;
        if (Input.GetKey(KeyCode.LeftShift)) velocity *= 1.4f;
        _velocity = transform.forward * _keyboardInput.y + transform.right * _keyboardInput.x;
        _velocity = Vector3.ClampMagnitude(_velocity, 1);
        _velocity *= velocity * Time.deltaTime;

        _velocity.y = gravityForce;

        characterController.Move(_velocity);
    }

    public void OnMove(InputValue value)
    {
        _keyboardInput = value.Get<Vector2>();
    }
}
