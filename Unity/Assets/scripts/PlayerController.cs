using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

namespace UnityAsset.Characters.ThirdPerson
{
    public class PlayerController : NetworkBehaviour
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

        [SerializeField]
        SCRIPT_GroundCheck groundCheck;

        [SerializeField]
        Animator animController;

        RaycastHit hit;

        public CameraController mouseLook = new CameraController();
        public PlayerStats playerStats = new PlayerStats();

        bool isGrounded;
        bool isJumping;
        bool isRolling;

        Vector3 jumpVector;
        private float jumpForce = 300.0f;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
        }

        // Update is called once per frame
        void Update()
        {
            if (!isLocalPlayer) { return; }

            RotateCamera();
            if (!isRolling)
            {
                playerStats.staminaRegen();
            }

            if (playerStats.getIsDead())
            {
                StartCoroutine(Die());
            }
        }

        void Start()
        {
            if (!isLocalPlayer)
            {
                playerCamera.gameObject.SetActive(false);
            }

            mouseLook.Init(playerTranform, playerCamera.transform);
            isJumping = false;
            isRolling = false;
        }

        void FixedUpdate()
        {
            if (!isLocalPlayer) { return; }

            isGrounded = GroundCheck();

            if (isGrounded && !isRolling && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                Vector3 MoveDirection = playerCamera.transform.forward * Input.GetAxis("Vertical") + playerCamera.transform.right * Input.GetAxis("Horizontal");
                MoveDirection.Normalize();

                playerRigidbody.AddForce(MoveDirection * horizontalMaxSpeed, ForceMode.Impulse);
            }

            if(isGrounded)
            {
                playerRigidbody.drag = 1f;

                if (Input.GetButtonDown("Jump") && !isJumping && !isRolling)
                {
                    playerRigidbody.drag = 0f;
                    playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
                    Vector3 jump = new Vector3(0f, jumpForce, 0f);
                    playerRigidbody.AddForce(jump, ForceMode.Impulse);
                }    
                /*
                if(Input.GetButtonDown("Roll") && !isJumping && !isRolling)
                {
                    Vector3 rollDirection = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
                    StartCoroutine(Roll(rollDirection));
                }    */       
            }
            else
            {
                playerRigidbody.drag = 0f;
            }
        }

        bool GroundCheck()
        {
            if (groundCheck.IsGrounded())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void RotateCamera()
        {
            mouseLook.LookRotation(playerTranform, playerCamera.transform);
        }

        IEnumerator Roll(Vector3 rollDirection)
        {
            isRolling = true;
            if(rollDirection.x >= 0 && rollDirection.z == 0)
            {
                animController.SetBool("isRollingF", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingF", false);
                playerCollider.enabled = true;
            }
            else if(rollDirection.x >= 0 && rollDirection.z >= 0)
            {
                animController.SetBool("isRollingFR", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingFR", false);
                playerCollider.enabled = true;
            }
            else if (rollDirection.x >= 0 && rollDirection.z <= 0)
            {
                animController.SetBool("isRollingFL", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingFL", false);
                playerCollider.enabled = true;
            }
            else if (rollDirection.x == 0 && rollDirection.z >= 0)
            {
                animController.SetBool("isRollingR", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingR", false);
                playerCollider.enabled = true;
            }
            else if (rollDirection.x == 0 && rollDirection.z <= 0)
            {
                animController.SetBool("isRollingL", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingL", false);
                playerCollider.enabled = true;
            }
            else if (rollDirection.x <= 0 && rollDirection.z >= 0)
            {
                animController.SetBool("isRollingBR", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingBR", false);
                playerCollider.enabled = true;
            }
            else if (rollDirection.x <= 0 && rollDirection.z <= 0)
            {
                animController.SetBool("isRollingBL", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingBL", false);
                playerCollider.enabled = true;
            }
            else
            {
                animController.SetBool("isRollingB", true);
                playerCollider.enabled = false;
                playerStats.makeRoll();
                yield return new WaitForSeconds(1.0f);
                isRolling = false;
                animController.SetBool("isRollingB", false);
                playerCollider.enabled = true;
            }
        }

        IEnumerator Die()
        {
            playerCollider.enabled = false;
            animController.SetBool("isDying", true);
            yield return new WaitForSeconds(2);
            Application.LoadLevel("Dead");
        }
    }
}