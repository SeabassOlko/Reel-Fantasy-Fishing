using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerControls input;

    public event Action OnTouchBegin;
    public event Action OnTouchEnd;

    private void Awake()
    {
        input = new PlayerControls();
    }

    private void OnEnable()
    {
        input.Enable();
        InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
        input.Base.Touch.started += ctx => OnTouchBegin();
        input.Base.Touch.canceled += ctx => OnTouchEnd();
    }

    private void OnDisable()
    {
        input.Disable();
        input.Base.Touch.started -= ctx => OnTouchBegin();
        input.Base.Touch.canceled -= ctx => OnTouchEnd();
    }

    public Vector2 PrimaryPosition()
    {
        Vector2 touchPos = input.Base.PrimaryPosition.ReadValue<Vector2>();
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, Camera.main.nearClipPlane));
        return worldPos;
    }

    public Vector3 LeftRightTilt()
    {
        Vector3 holder = input.Base.SideToSideTilt.ReadValue<Vector3>();
        return holder;
    }
}
