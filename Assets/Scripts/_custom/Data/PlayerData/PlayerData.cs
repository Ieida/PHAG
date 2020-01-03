using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataObject", menuName = "ControllerData/Player/PlayerDataObject")]
public class PlayerData : ControllerData
{
    [Header("Look Settings")]
    [Range(0.0f, 2.0f)]
    public float mouseSensitivity;
    public float xLock;
    [Header("Move Settings")]
    public float maxMoveSpeed;
    public float moveForce;
    [Header("Jump Settings")]
    public LayerMask groundMask;
    public float jumpForce;
    [Header("Crouch Settings")]
    public float crouchSpeed;
    [Header("Interact Settings")]
    public LayerMask interactMask;
    public float interactRange;
}
