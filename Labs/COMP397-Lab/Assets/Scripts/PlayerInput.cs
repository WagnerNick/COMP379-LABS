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
    [SerializeField] private float mobileScale = 10f;
    private float camXRotation;
    [SerializeField, Self] private CharacterController controller;
    [SerializeField, Child] private Camera cam;

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
#if !UNITY_ANDROID
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    private void OnDisable()
    {
        jump.started -= Jump;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        AudioController.Instance.PlayJumpSFX();
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

        // Rotate the camera
#if UNITY_ANDROID
        transform.Rotate(Vector3.up * readLook.x * rotationSpeed * Time.deltaTime * mobileScale);
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1 * mobileScale;
#else
        transform.Rotate(Vector3.up * readLook.x * rotationSpeed * Time.deltaTime);
        camXRotation += mouseSensY * readLook.y * Time.deltaTime * -1;
#endif
        camXRotation = Mathf.Clamp(camXRotation, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(camXRotation, 0, 0);
    }

    public void ChangeMouseSensitivity(float value)
    {
        mouseSensY = value * 10;
        rotationSpeed = value * 10;
    }
}
