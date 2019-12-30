using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarryController : MonoBehaviour
{
    public CarryData data;
    public float currentObjectsWeight;
    public List<Rigidbody> carryObjects = new List<Rigidbody>();

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position, transform.forward*data.maxPickupRange);
    }

    void Start()
    {
        InvokeRepeating("DropOnDistance", 0.1f, 0.16f);
    }

    public void PickUp(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if(currentObjectsWeight >= data.maxCarryWeight)
                return;

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, data.maxPickupRange, data.pickupItemMask);
            if(hits.Length < 1)
                return;

            for (int i = 0; i < hits.Length; i++)
            {
                if(!hits[i].rigidbody)
                    continue;
                    
                Rigidbody newRb = hits[i].rigidbody;
                if((newRb.mass+currentObjectsWeight) > data.maxCarryWeight)
                    continue;

                //add i to carryObjects
                newRb.transform.parent = transform;
                newRb.useGravity = false;
                newRb.drag = 7.0f;
                newRb.angularDrag = 3.75f;
                currentObjectsWeight += newRb.mass;
                carryObjects.Add(newRb);
            }
        }
        else if(context.canceled)
        {
            Drop();
        }
    }

    void DropOnDistance()
    {
        if(carryObjects.Count < 1)
            return;

        Rigidbody[] carryobjs = carryObjects.ToArray();
        for (int i = 0; i < carryobjs.Length; i++)
        {
            float dist = (transform.position-carryobjs[i].position).magnitude;
            if(dist <= data.dropDistance)
                continue;

            carryobjs[i].transform.parent = null;
            carryobjs[i].useGravity = true;
            carryobjs[i].drag = 0.0f;
            carryobjs[i].angularDrag = 0.05f;
            carryObjects.Remove(carryobjs[i]);
            currentObjectsWeight -= carryobjs[i].mass;
        }
    }

    void Drop()
    {
        if(carryObjects.Count < 1)
            return;

        Rigidbody[] carryobjs = carryObjects.ToArray();
        for (int i = 0; i < carryobjs.Length; i++)
        {
            carryobjs[i].transform.parent = null;
            carryobjs[i].useGravity = true;
            carryobjs[i].drag = 0.0f;
            carryobjs[i].angularDrag = 0.05f;
            currentObjectsWeight -= carryobjs[i].mass;
            carryObjects.Remove(carryobjs[i]);
        }
    }
}
