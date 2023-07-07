using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Windows;
using System;
using UnityEngine.InputSystem.LowLevel;

public class MouseMenuController : MonoBehaviour
{
    [SerializeField] InputAction joystickAction;
    [SerializeField] InputAction interatonAction;
    [SerializeField]float mouseSensitivity;
    Mouse mouse;

    private void Start()
    {
        mouse = InputSystem.AddDevice<Mouse>();
    }
    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked) return;

        Vector2 mouseDelta = joystickAction.ReadValue<Vector2>() * mouseSensitivity;
        print(mouseDelta);
        var currentPosition = mouse.position.ReadValue();
        InputSystem.QueueStateEvent(mouse,
            new MouseState
            {
                position = currentPosition + mouseDelta,
                clickCount = Convert.ToUInt16(interatonAction.ReadValue<bool>() ? 1 : 0)
            });
    }
    /*
    public void OnLook(InputValue value)
    {
        print(playeInput.currentControlScheme);
        print(Cursor.lockState);
        if (playeInput.currentControlScheme != "Gamepad" || Cursor.lockState==CursorLockMode.Locked) return;
        print("OnLook");
        _gamepadInput += value.Get<Vector2>() * mouseSensitivity * Time.deltaTime;
        Mouse.current.WarpCursorPosition(_gamepadInput);
        Mouse.current.
    }*/
}
