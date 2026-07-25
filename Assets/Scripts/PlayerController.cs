using System;
using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputSystem_Actions;

public class PlayerController : MonoBehaviour, IPlayerActions
{
    Animator animator;
    InputSystem_Actions inputActions;
    CharacterController character;
    CameraController cameraController;

    [Header("Weapon")]
    public SwingHitbox hitbox;
    public float damage;

    [Header("Player")]

    public float moveSpeed;
    public float moveInertia;
    Vector2 moveInput;
    Vector2 movement;

    [Header("Debug")]
    public GameObject debugCursor;

    Vector3 worldCursor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterController>();
        cameraController = FindAnyObjectByType<CameraController>();
        Debug.Log(character);

        if (inputActions == null)
        {
            inputActions = new InputSystem_Actions();
        }
        inputActions.Player.SetCallbacks(this);
        inputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cursorRay, out RaycastHit hitInfo))
        {
            worldCursor = hitInfo.point;
            debugCursor.transform.position = worldCursor;
        }

        float angle = Mathf.Atan2(worldCursor.x - transform.position.x, worldCursor.z - transform.position.z);
        transform.rotation = Quaternion.Euler(Vector3.up * angle * Mathf.Rad2Deg);

        // Movement
        movement = Vector2.Lerp(movement, moveInput, moveInertia * Time.deltaTime);
        character.SimpleMove(new Vector3(movement.x * moveSpeed, 0, movement.y * moveSpeed));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {

        if (context.ReadValue<float>() != 1 || context.phase != InputActionPhase.Performed)
        {
            return;
        }

        animator.SetTrigger("Swing");

        Boolean hitAnything = false;
        // Get enemies and apply damage
        foreach (Collider enemy in hitbox.inside)
        {
            if (enemy != null && enemy.TryGetComponent(out EnemyController e))
            {
                hitAnything = true;
                e.ApplyDamage(new DamageData(gameObject, damage));
            }
        }

        if (hitAnything)
        {
            cameraController.Shake(0.5f);
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
    }

    public void OnNext(InputAction.CallbackContext context)
    {
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }
}
