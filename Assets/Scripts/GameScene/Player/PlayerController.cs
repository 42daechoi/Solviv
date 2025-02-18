using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController LocalPlayerInstance { get; private set; }
    private string _currentAnimationState = "Default";
    public Animator _animator { get; private set; }
    private IState IdleState { get; set; }
    private IState JumpState { get; set; }
    private IState UseComputerState { get; set; }
    
    private IState _previousState;

    [Header("Speed Settings")]
    [SerializeField] private MovementSettings _speedSettings;
    
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    private IState _currentState;
    private Interaction _interaction;
    
    private Vector3 _inputDirection;
    public Vector3 InputDirection => _inputDirection;
    private bool _isSprinting;
    private float _offset;
    private float _currentSpeed;
    private Vector3 _computerInteractionPoint;
    private Quaternion _computerInteractionRotation;
    
    private InputManager_Game _defaultInputManager;
    private InputManager_Computer _computerInputManager;
    
    
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
        _animator  = GetComponent<Animator>();
        _currentSpeed = _speedSettings.walkSpeed;

        IdleState = new IdleState();
        JumpState = new JumpState();
        
        _defaultInputManager = FindObjectOfType<InputManager_Game>();
        
        _computerInputManager = gameObject.AddComponent<InputManager_Computer>();
        _computerInputManager.enabled = false;
        
        if (IdleState != null)
        {
            TransitionToState(IdleState);
        }
        else
        {
            Debug.LogError("IdleState가 초기화되지 않았습니다!");
        }
    }
    
    private void OnEnable()
    {
        EventManager_Game.Instance.OnPlayerMove += UpdateMoveInput;
        EventManager_Game.Instance.OnPlayerSprint += UpdateSprintInput;
        EventManager_Game.Instance.OnPlayerJump += HandlePlayerJump;
        EventManager_Game.Instance.OnInteraction += HandleInteraction;
        EventManager_Game.Instance.OnUseComputer += HandleUseComputer;
        EventManager_Game.Instance.OnMoveToComputer += HandleMoveToComputer;
        EventManager_Game.Instance.OnAnimationStateChanged += HandleAnimationStateChanged;
    }

    private void OnDisable()
    {
        EventManager_Game.Instance.OnPlayerMove -= UpdateMoveInput;
        EventManager_Game.Instance.OnPlayerSprint -= UpdateSprintInput;
        EventManager_Game.Instance.OnPlayerJump -= HandlePlayerJump;
        EventManager_Game.Instance.OnInteraction -= HandleInteraction;
        EventManager_Game.Instance.OnUseComputer -= HandleUseComputer;
        EventManager_Game.Instance.OnMoveToComputer -= HandleMoveToComputer;
        EventManager_Game.Instance.OnAnimationStateChanged -= HandleAnimationStateChanged;
    }
    
    private void Update()
    {
        if (_photonView.IsMine)
        {
            _currentState.UpdateState(this, _inputDirection, _offset);
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
        if (_currentState != null)
        {
            _previousState = _currentState;
            _currentState.ExitState(this);
        }

        _currentState = newState;
        _currentState.EnterState(this);
        
        if (_previousState is UseComputerState && _currentState is IdleState)
        {
            Debug.Log("컴퓨터종료 아이들로전환 컴퓨터강제종료이벤트발행");
            EventManager_Game.Instance.InvokeExitComputer();
        }
    }
    
    private void UpdateMoveInput(float horizontal, float vertical)
    {
        _inputDirection = new Vector3(horizontal, 0, vertical);
        _offset = 0.5f + (_isSprinting ? 0.5f : 0f);
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

    private void HandleUseComputer(bool isActComputer)
    {
        if (!_photonView.IsMine) return;

        if (EventManager_Game.Instance == null)
        {
            Debug.LogError("EventManager_Game 인스턴스가 null입니다.");
            return;
        }

        if (isActComputer)
        {
            if (_currentState is UseComputerState) return;

            if (UseComputerState == null)
            {
                UseComputerState = new UseComputerState();
            }
            SetInputManager(_computerInputManager);
            TransitionToState(UseComputerState);
            
            EventManager_Game.Instance.InvokeCameraActive(false);
        }
        else
        {
            if (_currentState is IdleState) return;

            SetInputManager(_defaultInputManager);
            TransitionToState(IdleState);
            
            EventManager_Game.Instance.InvokeCameraActive(true);
        }
    }

    private void HandleMoveToComputer(int playerId, Vector3 targetPosition, Quaternion targetRotation)
    {
        _computerInteractionPoint = targetPosition;
        _computerInteractionRotation = targetRotation;
    }
    
    public void StartMoveToComputer()
    {
        StopAllCoroutines();
        StartCoroutine(WalkToComputer(_computerInteractionPoint, _computerInteractionRotation));
        // transform.position = _computerInteractionPoint; 즉시텔레포트
    }
    
    private IEnumerator WalkToComputer(Vector3 targetPosition, Quaternion targetRotation)
    {
        float distanceThreshold = 0.1f;
        float moveSpeed = _speedSettings.walkSpeed;
        float rotationSpeed = 1f;
        
        Vector3 startPosition = transform.position;
        Vector3 direction = (targetPosition - startPosition).normalized;
        
        if (_animator != null)
        {
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.z);
        }

        while (Vector3.Distance(transform.position, targetPosition) > distanceThreshold)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            _rigidbody.MovePosition(newPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }
        
        transform.position = targetPosition;
        transform.rotation = targetRotation;
        
        if (_animator != null)
        {
            _animator.SetFloat("Horizontal", 0);
            _animator.SetFloat("Vertical", 0);
        }
        
        _animator?.SetTrigger("Typing");
    }
    

    private void SetInputManager(MonoBehaviour newInputManager)
    {
        if (!_photonView.IsMine) return;

        _defaultInputManager.enabled = false;
        _computerInputManager.enabled = false;

        newInputManager.enabled = true;
    }
    
    private void HandleAnimationStateChanged(string animationState)
    {
        _currentAnimationState = animationState;

        // 애니메이션 상태 변경
    }
    
    public void UpdateAnimator()
    {
        _animator.SetFloat("Horizontal", _inputDirection.x * _offset);
        _animator.SetFloat("Vertical", _inputDirection.z * _offset);
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
