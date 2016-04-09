using UnityEngine;
using System.Collections;

namespace UnityAsset.Characters.ThirdPerson
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        [Range(0f, 40f)]
        private float horizontalMaxSpeed = 25;

        [SerializeField]
        private Rigidbody playerRigidbody;

        [SerializeField]
        private CapsuleCollider playerCollider;

        [SerializeField]
        private Transform playerTranform;

        [SerializeField]
        Camera playerCamera;

        RaycastHit hit;

        public CameraController mouseLook = new CameraController();

        bool isGrounded;
        bool isJumping;

        Vector3 jumpVector;
        private float jumpForce = 500.0f;

        // Update is called once per frame
        void Update()
        {
            RotateCamera();
        }

        void Start()
        {
            mouseLook.Init(playerTranform, playerCamera.transform);
            isJumping = false;
        }

        void FixedUpdate()
        {
            isGrounded = GroundCheck();
            Debug.Log(isGrounded);

            if (isGrounded && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                Vector3 MoveDirection = playerCamera.transform.forward * Input.GetAxis("Vertical") + playerCamera.transform.right * Input.GetAxis("Horizontal");
                MoveDirection.Normalize();

                playerRigidbody.AddForce(MoveDirection * horizontalMaxSpeed, ForceMode.Impulse);
            }

            if(isGrounded)
            {
                playerRigidbody.drag = 1f;

                if (Input.GetButtonDown("Jump") && !isJumping)
                {
                    playerRigidbody.drag = 0f;
                    playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
                    Vector3 jump = new Vector3(0f, jumpForce, 0f);
                    playerRigidbody.AddForce(jump, ForceMode.Impulse);
                }               
            }
            else
            {
                playerRigidbody.drag = 0f;
            }

        }

        bool GroundCheck()
        {
            //Ray ray = new Ray(playerTranform.position, playerTranform.TransformDirection(Vector3.down));
            //Physics.Raycast(ray, out hit);

            if (Physics.SphereCast(transform.position, playerCollider.radius, Vector3.down, out hit,
                                   ((playerCollider.height / 2f) - playerCollider.radius), ~0, QueryTriggerInteraction.Ignore))
            {
                this.isJumping = false;
                return true;
            }
            else
            {
                this.isJumping = true;
                return false;
            }
        }

        void RotateCamera()
        {
            mouseLook.LookRotation(playerTranform, playerCamera.transform);
        }
    }

}