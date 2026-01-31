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

    //footsteps
    [SerializeField] AudioPitcherSO footsteps;
    [SerializeField] AudioSource source;
    float audioTimer = 0.0f;
    float footstepInterval = 2.0f;
    bool isMoving = false;

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

        //footstep sfx
        if (isMoving)
        {
            audioTimer += moveSpeed * Time.deltaTime;

            if (audioTimer > footstepInterval)
            {
                audioTimer = 0.0f;
                footsteps.Play(source);
            }
        }
        else
        {
            audioTimer = 0.0f;
        }
    }

    private void Move()
    {
        rb.velocity = GetMovementDirection() * moveSpeed;
    }

    private Vector2 GetMovementDirection()
    {
        //set is moving bool
        if (playerActions.Move.ReadValue<Vector2>().magnitude != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

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
