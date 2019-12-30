using UnityEngine;

[CreateAssetMenu(fileName = "CarryDataObject", menuName = "ControllerData/Carry/CarryDataObject")]
public class CarryData : ScriptableObject
{
    public float maxCarryWeight;
    public float maxPickupRange;
    public float dropDistance;
    public LayerMask pickupItemMask;
}
