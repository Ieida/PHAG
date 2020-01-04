using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractController : MonoBehaviour
{
    public PlayerData data;
    public Image canvasimage;
    public Sprite openHand;
    public Sprite closedHand;

    void Start()
    {
        InvokeRepeating("InteractBehaviour", 0.1f, 0.16f);
    }

    void InteractBehaviour()
    {
        RaycastHit hit;
        if(!Physics.Raycast(transform.position, transform.forward, out hit, data.interactRange, data.interactMask))
        {
            canvasimage.color = new Color(255.0f, 255.0f, 255.0f, 0.0f);
            canvasimage.sprite = openHand;
            return;
        }
        canvasimage.color = new Color(255.0f, 255.0f, 255.0f, 255.0f);
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
        {
            canvasimage.sprite = closedHand;
            interactable.Interact();
        }
        else if(context.canceled)
        {
            canvasimage.sprite = openHand;
        }
    }
}
