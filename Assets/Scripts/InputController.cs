using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerControls playerControls;

    private PlayerControls.PlayerActions playerActions;

    [SerializeField]
    private float moveAmount = 1f;

    private Grid grid;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var newPos =
            (MoveDirection() * moveAmount)
            + new Vector2(transform.position.x, transform.position.y);

        bool isOccupied = grid.GetTileData(newPos);

        if (isOccupied)
            return;

        transform.position = newPos;
    }

    private Vector2 MoveDirection()
    {
        return playerActions.Move.ReadValue<Vector2>().normalized;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
