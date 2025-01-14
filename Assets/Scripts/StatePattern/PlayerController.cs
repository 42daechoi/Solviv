using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IMovementState IdleState { get; set; }
    private IMovementState MoveState { get; set; }
    private IMovementState SprintState { get; set; }
    private IMovementState JumpState { get; set; }

    [Header("Speed Settings")]
    [SerializeField] private MovementSettings _speedSettings;
    
    private Rigidbody _rigidbody;
    private PhotonView _photonView;
    private IMovementState _currentState;
    
    private float _currentSpeed;
    

    [HideInInspector] public Vector3 inputDirection;
    [HideInInspector] public bool isGunEquipped;
    
    public Rigidbody Rigidbody => _rigidbody;
    public MovementSettings SpeedSettings => _speedSettings;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _photonView = GetComponent<PhotonView>();
        _currentSpeed = _speedSettings.walkSpeed;
        
        TransitionToState(new IdleState());
    }

    private void Update()
    {
        if (_photonView.IsMine)
        {
            _currentState.UpdateState(this);
        }
    }

    private void FixedUpdate()
    {
        if (_photonView.IsMine)
        {
            _currentState.FixedUpdateState(this);
        }
    }

    public void TransitionToState(IMovementState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public Vector3 CalculateMovement(float speed)
    {
        Vector3 targetVelocity = new Vector3(inputDirection.x, 0, inputDirection.z);
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
}
