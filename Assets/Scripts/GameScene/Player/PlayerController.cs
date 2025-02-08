using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController LocalPlayerInstance { get; private set; }
    private string _currentAnimationState = "Default";
    public Animator Animator { get; private set; }
    private IState IdleState { get; set; }
    private IState JumpState { get; set; }
    private IState UseCumputerState { get; set; }

    [Header("Speed Settings")]
    [SerializeField] private MovementSettings _speedSettings;
    
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    private IState _currentState;
    private Interaction _interaction;
    
    private Vector3 _inputDirection;
    public Vector3 InputDirection => _inputDirection;
    private bool _isSprinting;
    
    private float _currentSpeed;
    
    
    public Rigidbody Rigidbody => _rigidbody;
    public MovementSettings SpeedSettings => _speedSettings;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
        if (_photonView.IsMine)
        {
            if (LocalPlayerInstance == null)
            {
                LocalPlayerInstance = this;
            }
            else
            {
                Debug.LogWarning("로컬 PlayerController가 이미 존재.");
                Destroy(gameObject);
            }
        }
    }
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _interaction = GetComponent<Interaction>();
        Animator = GetComponent<Animator>();
        _currentSpeed = _speedSettings.walkSpeed;
        
        IdleState = new IdleState();
        JumpState = new JumpState();
        
        TransitionToState(new IdleState());
    }
    
    private void OnEnable()
    {
        EventManager_Game.Instance.OnPlayerMove += UpdateMoveInput;
        EventManager_Game.Instance.OnPlayerSprint += UpdateSprintInput;
        EventManager_Game.Instance.OnPlayerJump += HandlePlayerJump;
        EventManager_Game.Instance.OnInteraction += HandleInteraction;
        EventManager_Game.Instance.OnUseComputer += HandleUseComputer;
        EventManager_Game.Instance.OnAnimationStateChanged += HandleAnimationStateChanged;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnPlayerMove -= UpdateMoveInput;
        EventManager_Game.Instance.OnPlayerSprint -= UpdateSprintInput;
        EventManager_Game.Instance.OnPlayerJump -= HandlePlayerJump;
        EventManager_Game.Instance.OnInteraction -= HandleInteraction;
        EventManager_Game.Instance.OnUseComputer -= HandleUseComputer;
        EventManager_Game.Instance.OnAnimationStateChanged -= HandleAnimationStateChanged;
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
    
    private void UpdateMoveInput(Vector3 moveDirection)
    {
        _inputDirection = moveDirection;
        _currentState.UpdateState(this, _inputDirection, _isSprinting);
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

    public void HandleUseComputer(bool isActComputer)
    {
        if (EventManager_Game.Instance == null)
        {
            Debug.LogError("EventManager_Game 인스턴스가 null입니다.");
            return;
        }

        if (isActComputer)
        {
            if (UseCumputerState == null)
            {
                UseCumputerState = new UseComputerState();
            }
            TransitionToState(UseCumputerState);
        }
        else
        {
            TransitionToState(IdleState);
        }
    }
    
    private void HandleAnimationStateChanged(string animationState)
    {
        _currentAnimationState = animationState;

        // 애니메이션 상태 변경
        Animator.SetBool("isCarrying", animationState == "Carry");
    }
    
    
    private void HandlePlayerJump()
    {
        if (IsGrounded())
        {
            TransitionToState(JumpState);
        }
    }
    
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.1f);
    }

    public IState GetCurrentState()
    {
        return _currentState;
    }
}
