using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IState IdleState { get; set; }
    private IState MoveState { get; set; }
    private IState SprintState { get; set; }
    private IState JumpState { get; set; }

    [Header("Speed Settings")]
    [SerializeField] private MovementSettings _speedSettings;
    
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    private IState _currentState;
    private Interaction _interaction;
    
    private Vector3 _inputDirection;
    private bool _isSprinting;
    
    private float _currentSpeed;
    
    
    [HideInInspector] public bool isGunEquipped;
    
    public Rigidbody Rigidbody => _rigidbody;
    public MovementSettings SpeedSettings => _speedSettings;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _interaction = GetComponent<Interaction>();
        _currentSpeed = _speedSettings.walkSpeed;
        
        TransitionToState(new IdleState());
    }
    
    private void OnEnable()
    {
        EventManager_Game.Instance.OnPlayerMove += UpdateMoveInput;
        EventManager_Game.Instance.OnPlayerSprint += UpdateSprintInput;
        EventManager_Game.Instance.OnInteraction += HandleInteraction;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnPlayerMove -= UpdateMoveInput;
        EventManager_Game.Instance.OnPlayerSprint -= UpdateSprintInput;
        EventManager_Game.Instance.OnInteraction -= HandleInteraction;
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            _currentState.UpdateState(this, _inputDirection, _isSprinting);
        }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            _currentState.FixedUpdateState(this);
        }
    }

    public void TransitionToState(IState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(_inputDirection.x, 0, _inputDirection.z);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        Vector3 velocity = _rigidbody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;

        velocityChange.x = Mathf.Clamp(velocityChange.x, -_speedSettings.maxVelocityChange, _speedSettings.maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -_speedSettings.maxVelocityChange, _speedSettings.maxVelocityChange);
        velocityChange.y = 0;

        return velocityChange;
    }

    private void OnUseItem()
    {
        if (TryGetComponent(out HeldItem heldItem))
        {
            Item currentItem = heldItem.GetItem();
            currentItem?.UseItem();
        }
    }
    
    private void UpdateMoveInput(Vector3 moveDirection)
    {
        _inputDirection = moveDirection;
    }

    private void UpdateSprintInput(bool isSprinting)
    {
        _isSprinting = isSprinting;
    }
    
    private void HandleInteraction()
    {
        if (_currentState.CanInteraction())
        {
            Debug.Log("인터렉션 실행");
            _interaction.RunInteraction();
        }
        else
        {
            Debug.Log("현재 상태에서 Interaction 실행 불가.");
        }
    }

    public IState GetCurrentState()
    {
        return _currentState;
    }
}
