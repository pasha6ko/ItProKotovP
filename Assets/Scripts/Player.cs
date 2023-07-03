using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Camera
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform playerBody;

    [SerializeField] GameObject interactionIcon;
    [SerializeField] TextMeshProUGUI interactionIconText;

    [SerializeField] float mouseSensitivity;

    private Vector2 _mouseInput;

    private Vector2 _rotation;
    #endregion

    #region Movement 
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float gravityForce = -3;
    [SerializeField] float sandartVelocity;

    private Vector3 _velocity;
    private Vector2 _keyboardInput;
    #endregion

    Interacteble interactableObject;


    // Start is called before the first frame update
    void Start()
    {
        CursorLocker.SetCursorLockState(CursorLockMode.Locked);
    }

    // Update is called once per frame
    void Update()
    {
        #region Move
        float velocity = sandartVelocity;
        if (Input.GetKey(KeyCode.LeftShift)) velocity *= 1.4f;
        _velocity = transform.forward * _keyboardInput.y + transform.right * _keyboardInput.x;
        _velocity = Vector3.ClampMagnitude(_velocity, 1);
        _velocity *= velocity * Time.deltaTime;

        _velocity.y = gravityForce;

        characterController.Move(_velocity);
        #endregion
        #region Look
        _rotation.x -= _mouseInput.y;
        _rotation.y += _mouseInput.x;

        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

        playerCamera.localEulerAngles = Vector3.right * _rotation.x;
        playerBody.localEulerAngles = Vector3.up * _rotation.y;
        #endregion
        InteractionRaycast();
        
    }

    void InteractionRaycast()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2f))
        {
            Interacteble interact = hit.transform.GetComponent<Interacteble>();
            if (interact != null)
            {
                interactionIcon.SetActive(true);
                interact.UpdateUI(interactionIconText);
                interactableObject = interact;
            }
            else
            {
                interactionIcon.SetActive(false);
            }
        }
        else
        {
            interactionIcon.SetActive(false);
        }
    }

    public void OnMove(InputValue value)
    {
        _keyboardInput = value.Get<Vector2>();
    }
    public void OnLook(InputValue value)
    {
        _mouseInput = value.Get<Vector2>() * Time.deltaTime * mouseSensitivity / 100;
    }
    public void OnFire(InputValue value)
    {
        if(interactableObject!=null) interactableObject.Interact();
    }
}
