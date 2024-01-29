using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 PlayerMoveInput;
    private Vector2 MouseMoveInput;

    private CharacterController Controller;
    private PlayerControls Controls;

    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;

    private bool enable = true;

    // Start is called before the first frame update
    private void Start()
    {
        // initialize the controls we have created in the folders Controls
        Controls = new PlayerControls();
        Controls.Enable();

        // MoveKeys = AWSD
        Controls.Keyboard.MoveKeys.performed += ctx =>
        {
            PlayerMoveInput = new Vector3(ctx.ReadValue<Vector2>().x, PlayerMoveInput.y, ctx.ReadValue<Vector2>().y);
        };
        Controls.Keyboard.MoveKeys.canceled += ctx =>
        {
            PlayerMoveInput = new Vector3(ctx.ReadValue<Vector2>().x, PlayerMoveInput.y, ctx.ReadValue<Vector2>().y);
        };

        Controls.Keyboard.UpDown.performed += ctx =>
        {
            PlayerMoveInput = new Vector3(PlayerMoveInput.x, ctx.ReadValue<float>(), PlayerMoveInput.z);
        };
        Controls.Keyboard.UpDown.canceled += ctx =>
        {
            PlayerMoveInput = new Vector3(PlayerMoveInput.x, ctx.ReadValue<float>(), PlayerMoveInput.z);
        };

        Controls.Keyboard.MouseDelta.performed += ctx =>
        {
            MouseMoveInput = ctx.ReadValue<Vector2>();
        };
        Controls.Keyboard.MouseDelta.canceled += ctx =>
        {
            MouseMoveInput = ctx.ReadValue<Vector2>();
        };

        Controller = GetComponent<CharacterController>();
    }

    private void ctx(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    private void Update()
    {
        if (enable) Move();
        Look();
    }

    private void Move()
    {
        Vector3 MoveVec = transform.TransformDirection(PlayerMoveInput);

        Controller.Move(MoveVec * Speed * Time.deltaTime);
    }

    private float xRot;

    private void Look()
    {
        Vector2 NonNormalizedDelta = MouseMoveInput * .5f * .1f;

        xRot -= NonNormalizedDelta.y * Sensitivity;

        transform.Rotate(0f, NonNormalizedDelta.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
    }
}