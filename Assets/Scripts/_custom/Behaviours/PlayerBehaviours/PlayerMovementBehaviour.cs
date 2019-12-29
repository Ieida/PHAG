using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovementBehaviour : ControllableBehaviour
{    
    public PlayerData data;

    Rigidbody rb;

    public Transform playerCamera;

    CapsuleCollider playerCollider;

    Vector2 lookAxes;
    Vector2 movementInput;

    void OnDrawGizmosSelected()
    {
        CapsuleCollider coll = GetComponent<CapsuleCollider>();
        Vector3 spherePos = new Vector3(transform.position.x, transform.position.y-((coll.height/2)-coll.radius)-0.04f, transform.position.z);
        Gizmos.DrawWireSphere(spherePos, coll.radius-0.02f);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookAxes = new Vector2(transform.eulerAngles.x, transform.eulerAngles.y);
    }

    override public void Behave()
    {
        playerCamera.rotation = Quaternion.Euler(lookAxes.x, lookAxes.y, 0.0f);
        playerCamera.position = new Vector3(transform.position.x, transform.position.y+(playerCollider.height/2)-playerCollider.radius, transform.position.z);
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
        Vector3 spherePos = new Vector3(transform.position.x, transform.position.y-((playerCollider.height/2)-playerCollider.radius)-0.04f, transform.position.z);
        if(Physics.CheckSphere(spherePos, playerCollider.radius-0.02f, data.groundMask))
            rb.AddForce(transform.up*data.jumpForce, ForceMode.Impulse);
    }
}
