using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private PlayerControls playerControls;

    private PlayerControls.PlayerActions playerActions;
    private float moveAmount => grid.GetTileSize();

    [SerializeField]
    private Grid grid;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        var newPos =
            (ctx.ReadValue<Vector2>().normalized * moveAmount)
            + new Vector2(transform.position.x, transform.position.y);

        var tileData = grid.GetTileData(newPos);

        if (tileData == null)
            return;

        if (tileData.isOccupied)
            return;

        transform.position = tileData.position;
    }

    private void OnEnable()
    {
        playerControls.Enable();

        playerActions.Move.performed += Move;
    }

    private void OnDisable()
    {
        playerControls.Disable();

        playerActions.Move.performed -= Move;
    }
}
