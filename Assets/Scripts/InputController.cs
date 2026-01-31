using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerControls playerControls;

    private PlayerControls.PlayerActions playerActions;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private Grid grid;

    private PlayerInteractor interactor;

    private Rigidbody2D rb;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        rb = GetComponent<Rigidbody2D>();
        interactor = GetComponentInChildren<PlayerInteractor>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = GetMovementDirection() * moveSpeed;
    }

    private Vector2 GetMovementDirection()
    {
        return playerActions.Move.ReadValue<Vector2>().normalized;
    }

    private void CallInteract(InputAction.CallbackContext ctx)
    {
        interactor.FlagInteract();
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerActions.Interact.performed += CallInteract;
    }

    private void OnDisable()
    {
        playerControls.Disable();

        playerActions.Interact.performed -= CallInteract;
    }
}
