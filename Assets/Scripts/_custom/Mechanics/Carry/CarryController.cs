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
            Debug.Log("started");
            if(currentObjectsWeight >= data.maxCarryWeight)
                return;

            Debug.Log("can carry more");
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, data.maxPickupRange, data.pickupItemMask);
            if(hits.Length < 1)
                return;

            Debug.Log("there is more to carry");
            Debug.Log(hits.Length);
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
                newRb.drag = 20.0f;
                newRb.angularDrag = 5.0f;
                currentObjectsWeight += newRb.mass;
                carryObjects.Add(newRb);
                Debug.Log("added to carry");
            }
        }
        else if(context.canceled)
        {
            Debug.Log("canceled");
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
            Debug.Log(dist);
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
