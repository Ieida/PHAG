public class PlayerController : ControllerBehaviour
{
    public int state;
    public ControllableBehaviour[] behaviours;

    void Update()
    {
        InitState(state, 0);
    }

    void FixedUpdate()
    {
        InitState(state, 1);
    }

    override public void InitState(int initState, int updateType)
    {
        if(!behaviours[initState])
            return;

        if(behaviours[initState].update && updateType == 0)
        {
            behaviours[initState].Behave();
            return;
        }
        if(behaviours[initState].fixedUpdate && updateType == 1)
            behaviours[initState].FixedBehave();
    }
}
