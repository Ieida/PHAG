using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BrickDeadlyBehaviour : BrickBehaviour
{
    public float damage;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    override public void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
        if(other.relativeVelocity.magnitude < 2.5f)
            return;

        if(!other.transform.TryGetComponent(out HealthBehaviour healthBehaviour))
            return;

        healthBehaviour.Hurt(damage);
    }
}
