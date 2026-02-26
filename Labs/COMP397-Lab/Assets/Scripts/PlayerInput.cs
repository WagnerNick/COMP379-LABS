using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;


[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    private InputAction jump;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float gravity = -30f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpeed = 4f;
    [SerializeField] private float mouseSensY = 5f;
    private float camXRotation;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;
    [SerializeField, Scene] private AudioController audioController;

    private void OnValidate()
    {
        this.ValidateRefs();
    }

    void Start()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        look = InputSystem.actions.FindAction("Player/Look");
        jump = InputSystem.actions.FindAction("Player/Jump");
        jump.started += Jump;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        jump.started -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        audioController.PlayJumpSFX();
    }

    void Update()
    {
        Vector2 readMove = move.ReadValue<Vector2>();
        Vector2 readLook = look.ReadValue<Vector2>();
        // Player Movement
        Vector3 movement = transform.right * readMove.x + transform.forward * readMove.y;
        velocity.y += gravity * Time.deltaTime;
        movement *= maxSpeed * Time.deltaTime;
        movement += velocity;
        controller.Move(movement);

        // Player Look
        transform.Rotate(Vector3.up * readLook.x * rotationSpeed * Time.deltaTime);

        // Rotate the camera
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    public void ChangeMouseSensitivity(float value)
    {
        mouseSensY = value * 10;
        rotationSpeed = value * 10;
    }
}
