using UnityEngine;
using UnityEngine.InputSystem;

public class InteractController : MonoBehaviour
{
    public PlayerData data;

    void Start()
    {
        InvokeRepeating("InteractBehaviour", 0.1f, 0.16f);
    }

    void InteractBehaviour()
    {
        RaycastHit hit;
        if(!Physics.Raycast(transform.position, transform.forward, out hit, data.interactRange, data.interactMask))
            return;

        //change cursor
    }

    public void Interact(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if(!Physics.Raycast(transform.position, transform.forward, out hit, data.interactRange, data.interactMask))
            return;

        InteractableBehaviour interactable;
        interactable = hit.transform.root.GetComponentInChildren<InteractableBehaviour>();

        if(!interactable)
            return;

        if(context.started)
            interactable.Interact();
    }
}
