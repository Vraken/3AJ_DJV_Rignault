using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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
    Animator rollController;

    [SerializeField]
    Camera playerHUD;
    
    private PlayerStats playerStats;

    RaycastHit hit;

    public CameraController mouseLook = new CameraController();
    private SCRIPT_dataManager dataManager = new SCRIPT_dataManager();

    bool isGrounded = false;
    bool isJumping = false;
    bool isRolling = false;
    bool isPaused = false;
    bool isActivated = false;

    Vector3 jumpVector;
    private float jumpForce = 300.0f;

    public void OnEnable()
    {
        mouseLook.Init(playerTranform, playerCamera.transform);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        playerCamera.gameObject.SetActive(true);
        playerHUD.gameObject.SetActive(true);
        isActivated = true;
        playerStats = dataManager.loadProfile();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) { return; }

        RotateCamera();
        if (!isRolling)
        {
            playerStats.staminaRegen(Time.deltaTime);
        }

        if (playerStats.getIsDead())
        {
            StartCoroutine(Die());
        }

        if (Input.GetButtonDown("Pause"))
        {
            CmdPauseGame();
        }
    }

    [Command]
    public void CmdPauseGame()
    {
        RpcPauseGame();
    }

    [ClientRpc]
    public void RpcPauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
        }
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
            if(Input.GetButtonDown("Roll") && !isJumping && !isRolling && playerStats.getStamina() >= 25)
            {
                playerStats.makeRoll();
                Vector3 rollDirection = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal"));
                CmdRoll(rollDirection);
            }
        }
        else
        {
            playerRigidbody.drag = 0f;
        }
    }

    [Command]
    public void CmdRoll(Vector3 rollDirection)
    {
        if (!Network.isClient)
        {
            StartCoroutine(Roll(rollDirection));
        }
        RpcRoll(rollDirection);
    }

    [ClientRpc]
    public void RpcRoll(Vector3 rollDirection)
    {
        if (rollDirection.x > 0 && rollDirection.z == 0)
        {
            rollController.SetTrigger("RollF");
        }
        else if (rollDirection.x > 0 && rollDirection.z > 0)
        {
            rollController.SetTrigger("RollF");
        }
        else if (rollDirection.x > 0 && rollDirection.z < 0)
        {
            rollController.SetTrigger("RollF");
        }
        else if (rollDirection.x == 0 && rollDirection.z > 0)
        {
            rollController.SetTrigger("RollR");
        }
        else if (rollDirection.x == 0 && rollDirection.z < 0)
        {
            rollController.SetTrigger("RollL");
        }
        else if (rollDirection.x < 0 && rollDirection.z > 0)
        {
            rollController.SetTrigger("RollB");
        }
        else if (rollDirection.x < 0 && rollDirection.z < 0)
        {
            rollController.SetTrigger("RollB");
        }
        else
        {
            rollController.SetTrigger("RollB");
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
        playerTranform.gameObject.layer = 17;
        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        playerRigidbody.velocity = Vector3.zero;
        Vector3 rollForce = new Vector3();
        rollController.enabled = true;

        if (rollDirection.x > 0 && rollDirection.z == 0)
        {
            rollForce = new Vector3(0f, 0f, 1000f);
        }
        else if(rollDirection.x > 0 && rollDirection.z > 0)
        {
            rollForce = new Vector3(500f, 0f, 500f);
        }
        else if (rollDirection.x > 0 && rollDirection.z < 0)
        {
            rollForce = new Vector3(-500f, 0f, 500f);
        }
        else if (rollDirection.x == 0 && rollDirection.z > 0)
        {
            rollForce = new Vector3(1000f, 0f, 0f);
        }
        else if (rollDirection.x == 0 && rollDirection.z < 0)
        {
            rollForce = new Vector3(-1000f, 0f, 0f);
        }
        else if (rollDirection.x < 0 && rollDirection.z > 0)
        {
            rollForce = new Vector3(500f, 0f, -500f);
        }
        else if (rollDirection.x < 0 && rollDirection.z < 0)
        {
            rollForce = new Vector3(-500f, 0f, -500f);
        }
        else
        {
            rollForce = new Vector3(0f, 0f, -1000f);
        }
        playerRigidbody.AddRelativeForce(rollForce, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
        playerTranform.gameObject.layer = 9;
        isRolling = false;
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    IEnumerator Die()
    {
        rollController.enabled = true;
        playerRigidbody.isKinematic = true;
        playerCollider.enabled = false;
        rollController.SetTrigger("die");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Dead");
    }

    public bool getIsActivated()
    {
        return isActivated;
    }

    public PlayerStats getPlayerStats()
    {
        return playerStats;
    }
}