using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Camera
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform playerBody;
    [SerializeField] PlayerInput playerInput;

    [SerializeField] GameObject interactionIcon;
    [SerializeField] TextMeshProUGUI interactionIconText;
    [SerializeField] GameObject beerPrefab;

    [SerializeField] float mouseSensitivity, gamepadSensivity;

    [SerializeField] GameObject menu;

    private Vector2 _mouseInput;

    private Vector2 _rotation;

    public Transform hand;

    #endregion

    #region Movement 
    [SerializeField] private CharacterController characterController;

    [SerializeField] private float gravityForce = -3;
    [SerializeField] float sandartVelocity;

    private Vector3 _velocity;
    private Vector2 _keyboardInput;
    #endregion

    Interacteble interactableObject;

    public bool controllModeStay;
    public bool lookModeStay;
    [SerializeField] GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        CursorLocker.SetCursorLockState(CursorLockMode.Locked);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(playerCamera.position, playerCamera.forward, Color.red);
        InteractionRaycast();
        if (controllModeStay) return;
        #region Look
        //_mouseInput *= Time.deltaTime;
        _rotation.x -= _mouseInput.y;
        _rotation.y += _mouseInput.x;

        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

        playerCamera.localEulerAngles = Vector3.right * _rotation.x     ;
        playerBody.localEulerAngles = Vector3.up * _rotation.y;
        #endregion
        if (lookModeStay) return;
        #region Move
        float velocity = sandartVelocity;
        if (Input.GetKey(KeyCode.LeftShift)) velocity *= 1.4f;
        _velocity = transform.forward * _keyboardInput.y + transform.right * _keyboardInput.x;
        _velocity = Vector3.ClampMagnitude(_velocity, 1);
        _velocity *= velocity * Time.deltaTime;

        _velocity.y = gravityForce;

        characterController.Move(_velocity);
        #endregion

    }

    void InteractionRaycast()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f, 3))
        {
            Interacteble interact = hit.transform.GetComponent<Interacteble>();
            if (interact != null)
            {
                interactionIcon.SetActive(true);
                interact.UpdateInteractionIcon(interactionIconText);
                interactableObject = interact;
            }
            else
            {
                interactableObject = null;
                interactionIcon.SetActive(false);
            }
        }
        else
        {
            interactableObject = null;
            interactionIcon.SetActive(false);
        }
    }

    public void FixCamera()
    {
        controllModeStay = true;
        CursorLocker.SetCursorLockState(CursorLockMode.None);
    }
    public void UnFixCamera()
    {
        controllModeStay = false;
        CursorLocker.SetCursorLockState(CursorLockMode.Locked);
    }
    public void FixPlayerMovment()
    {
        lookModeStay = true;
    }
    public void UnFixPlayerMovment()
    {
        lookModeStay = false;
    }
    public void PutInHand(Transform item)
    {
        if (hand.childCount > 0) return;
        item.SetParent(hand);
        item.localPosition = Vector3.zero;
    }
    public void RemoveItemFromHand()
    {
        Destroy(hand.GetChild(0).gameObject);
    }
    public void SpawnBeer()
    {
        if (hand.childCount > 0) return;
        GameObject clone = Instantiate(beerPrefab,hand);
        clone.transform.localPosition = Vector3.zero;

    }
    public void OnMove(InputValue value)
    {
        _keyboardInput = value.Get<Vector2>();
    }
    public void OnLook(InputValue value)
    {   
        if (playerInput.currentControlScheme == "Gamepad") _mouseInput = value.Get<Vector2>() * gamepadSensivity * Time.deltaTime;
        else _mouseInput = value.Get<Vector2>() * mouseSensitivity * Time.deltaTime;

        //_mouseInput = value.Get<Vector2>() * Time.deltaTime * mouseSensitivity / 100;
    }
    public void OnInteration(InputValue value)
    {
        print("interact");
        if (interactableObject != null) interactableObject.Interact(this);
    }
    public void OnEsc()
    {
        if (menu.active)
        {
            CursorLocker.SetCursorLockState(CursorLockMode.Locked);
            UnFixCamera();

        }
        else
        {
            CursorLocker.SetCursorLockState(CursorLockMode.None);
            FixCamera();
        }
        menu.SetActive(!menu.active);
    }
}
