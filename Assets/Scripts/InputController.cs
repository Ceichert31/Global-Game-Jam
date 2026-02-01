using DG.Tweening;
using Tutorial_System.Scripts.EventChannel;
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
    [SerializeField]
    AudioPitcherSO footsteps;

    [SerializeField]
    AudioSource source;
    float audioTimer = 0.0f;
    float footstepInterval = 2.0f;
    bool isMoving = false;

    private SpriteRenderer characterSprite;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerActions = playerControls.Player;

        rb = GetComponent<Rigidbody2D>();
        interactor = GetComponentInChildren<PlayerInteractor>();
        characterSprite = GetComponentInChildren<SpriteRenderer>();
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

    Tweener flipTween;

    [SerializeField]
    private float flipDuration = 0.3f;

    [SerializeField]
    private Ease flipEase = Ease.Linear;

    [SerializeField]
    private VoidEventChannel moveCheck;

    private void Update()
    {
        //Moving left
        if (GetMovementDirection().x < 0)
        {
            flipTween?.Kill(true);
            //characterSprite.flipX = true;
            flipTween = characterSprite
                .transform.DOLocalRotate(new Vector3(0, 180, 0), flipDuration)
                .SetEase(flipEase);
        }
        else if (GetMovementDirection().x > 0)
        {
            flipTween?.Kill(true);
            //characterSprite.flipX = false;
            flipTween = characterSprite
                .transform.DOLocalRotate(new Vector3(0, 0, 0), flipDuration)
                .SetEase(flipEase);
        }

        /* //Shake player
         if (isMoving)
         {
             characterSprite
                 .transform.DOLocalRotate(new Vector3(0, 0, 5), flipDuration)
                 .OnComplete(() =>
                 {
                     characterSprite.transform.DOLocalRotate(new Vector3(0, 0, -5), flipDuration);
                 })
                 .SetLoops(-1, LoopType.Yoyo);
         }*/
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
            moveCheck.CallEvent();
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
