namespace SF_FPSController
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Photon.Pun;

    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviourPun
    {
        public float walkingSpeed = 7.5f;
        public float runningSpeed = 11.5f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;
        public Camera playerCamera; // Player's camera
        public GameObject flashlightHolder; // Flashlight
        public float lookSpeed = 2.0f;
        public float lookXLimit = 45.0f;

        private CharacterController characterController;
        private Vector3 moveDirection = Vector3.zero;
        private float rotationX = 0;

        [HideInInspector]
        public bool canMove = true;

        void Start()
        {
            characterController = GetComponent<CharacterController>();

            if (photonView.IsMine)
            {
                // Local player setup
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                // Dynamically find the player camera if not assigned
                if (playerCamera == null)
                {
                    playerCamera = GetComponentInChildren<Camera>();
                }

                if (playerCamera != null)
                {
                    playerCamera.enabled = true; // Enable local player's camera
                }
            }
            else
            {
                // Remote player setup
                if (playerCamera != null)
                {
                    playerCamera.enabled = false; // Disable remote player's camera
                }

                // Disable AudioListener for remote players
                AudioListener audioListener = GetComponentInChildren<AudioListener>();
                if (audioListener != null)
                {
                    audioListener.enabled = false;
                }

                // Disable flashlight for remote players
                if (flashlightHolder != null)
                {
                    flashlightHolder.SetActive(false);
                }
            }
        }

        void Update()
        {
            // Ensure only the local player can control their character
            if (!photonView.IsMine) return;

            // Movement
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
            {
                moveDirection.y = jumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                flashlightHolder.SetActive(!flashlightHolder.activeSelf);
            }

            if (!characterController.isGrounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }

            characterController.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
}
