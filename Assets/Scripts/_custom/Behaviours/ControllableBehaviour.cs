using UnityEngine;

public class ControllableBehaviour : MonoBehaviour
{
    public bool update;
    public bool fixedUpdate;

    virtual public void Behave()
    {
    }

    virtual public void FixedBehave()
    {
    }
}
