using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform playerBody;

    [SerializeField] GameObject interactionIcon;
    [SerializeField] TextMeshProUGUI interactionIconText;

    [SerializeField] float mouseSensitivity;

    private Vector2 _mouseInput;

    private Vector2 _rotation;

    private void Start()
    {
        CursorLocker.SetCursorLockState(CursorLockMode.Locked);
    }
    private void Update()
    {
        #region Controll
        _rotation.x -= _mouseInput.y;
        _rotation.y += _mouseInput.x;

        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

        playerCamera.localEulerAngles = Vector3.right * _rotation.x;
        playerBody.localEulerAngles = Vector3.up * _rotation.y;
        #endregion
        #region raycast
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,2f))
        {
            Interacteble interactebleObject = hit.transform.GetComponent<Interacteble>();
            if (interactebleObject!=null) 
            {
                interactionIcon.SetActive(true);
                interactebleObject.UpdateUI(interactionIconText);
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
        #endregion
    }

    public void OnLook(InputValue input)
    {
        _mouseInput = input.Get<Vector2>() * Time.deltaTime * mouseSensitivity/100;
    }

    
}
