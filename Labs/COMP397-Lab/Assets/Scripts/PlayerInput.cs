using UnityEngine;
using UnityEngine.InputSystem;
using KBCore.Refs;


[RequireComponent(typeof(CharacterController))]
public class PlayerInput : MonoBehaviour
{
    private InputAction move;
    private InputAction look;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float gravity = -30f;
    private Vector3 velocity;
    [SerializeField] private float rotationSpped = 4f;
    [SerializeField] private float mouseSensY = 5f;
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
        transform.Rotate(Vector3.up * readLook.x * rotationSpped * Time.deltaTime);

        // Rotate the camera
        mouseSensY = mouseSensY * readLook.y;
        mouseSensY = Mathf.Clamp(mouseSensY, -90f, 90f);
        cam.gameObject.transform.localRotation = Quaternion.Euler(mouseSensY, 0, 0);
    }
}
