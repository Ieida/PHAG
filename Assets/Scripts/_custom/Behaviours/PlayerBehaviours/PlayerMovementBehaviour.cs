using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementBehaviour : ControllableBehaviour
{    
    public PlayerData data;

    //movement
    public Rigidbody rb;

    Vector2 movementInput;

    //look
    public Transform playerCamera;

    Vector2 lookAxes;

    //crouch
    public CapsuleCollider playerStandingCollider;
    public CapsuleCollider playerCrouchingCollider;

    Vector2 cameraPositions;
    float camPosY;

    bool isCrouching = false;

    public void AssemblePlayer()
    {
        rb = GetComponent<Rigidbody>();
        playerStandingCollider = transform.GetChild(0).GetComponents<CapsuleCollider>()[0];
        playerCrouchingCollider = transform.GetChild(1).GetComponents<CapsuleCollider>()[0];
        playerCamera = Camera.main.transform;
        int sibIndex = transform.GetSiblingIndex();
        playerCamera.SetSiblingIndex(sibIndex++);
        playerCamera.position = new Vector3(transform.position.x, transform.position.y+(playerStandingCollider.height/2)-playerStandingCollider.radius, transform.position.z);
        playerCamera.rotation = transform.rotation;
    }

    void OnDrawGizmosSelected()
    {
        if(playerStandingCollider)
        {
            Vector3 spherePos = new Vector3(transform.position.x, transform.position.y-((playerStandingCollider.height/2)-playerStandingCollider.radius)-0.04f, transform.position.z);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spherePos, playerStandingCollider.radius-0.02f);
        }
        else if(playerCrouchingCollider)
        {
            Vector3 spherePos = new Vector3(transform.position.x, transform.position.y-((playerCrouchingCollider.height/2)-playerCrouchingCollider.radius)-0.04f, transform.position.z);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spherePos, playerCrouchingCollider.radius-0.02f);
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookAxes = new Vector2(transform.eulerAngles.x, transform.eulerAngles.y);
        cameraPositions.x = (playerStandingCollider.height/2)-playerStandingCollider.radius;
        cameraPositions.y = cameraPositions.x-(playerStandingCollider.height-playerCrouchingCollider.height);
        camPosY = cameraPositions.x;
    }

    override public void Behave()
    {
        playerCamera.rotation = Quaternion.Euler(lookAxes.x, lookAxes.y, 0.0f);
        if(isCrouching)
            camPosY = Mathf.Lerp(camPosY, transform.position.y+cameraPositions.y, Time.deltaTime*data.crouchSpeed);
        else
            camPosY = Mathf.Lerp(camPosY, transform.position.y+cameraPositions.x, Time.deltaTime*data.crouchSpeed);

        playerCamera.position = new Vector3(transform.position.x, camPosY, transform.position.z);
    }

    override public void FixedBehave()
    {
        float addition = Mathf.Clamp01(data.maxMoveSpeed-rb.velocity.magnitude);
        Quaternion inputRot = Quaternion.Euler(transform.eulerAngles.x, playerCamera.eulerAngles.y, transform.eulerAngles.z);
        Vector3 inputDir = new Vector3(movementInput.x, 0.0f, movementInput.y).normalized;
        inputDir = (inputRot*inputDir).normalized;
        rb.AddForce(inputDir*data.moveForce*addition);
    }

    public void Look(InputAction.CallbackContext context)
    {
        float x = -context.ReadValue<Vector2>().y*data.mouseSensitivity;
        float y = context.ReadValue<Vector2>().x*data.mouseSensitivity;
        lookAxes.x += x;
        lookAxes.y += y;
        if(lookAxes.x > data.xLock)
            lookAxes.x = data.xLock;
        else if(lookAxes.x < -data.xLock)
            lookAxes.x = -data.xLock;
    }

    public void Move(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Vector3 spherePos = new Vector3(transform.position.x, transform.position.y-((playerStandingCollider.height/2)-playerStandingCollider.radius)-0.04f, transform.position.z);
        if(Physics.CheckSphere(spherePos, playerStandingCollider.radius-0.02f, data.groundMask))
            rb.AddForce(transform.up*data.jumpForce, ForceMode.Impulse);
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isCrouching = true;
        }
        else if(context.performed)
        {
            playerStandingCollider.gameObject.SetActive(false);
            playerCrouchingCollider.gameObject.SetActive(true);
        }
        else if(context.canceled)
        {
            playerStandingCollider.gameObject.SetActive(true);
            playerCrouchingCollider.gameObject.SetActive(false);
            isCrouching = false;
        }
    }
}
