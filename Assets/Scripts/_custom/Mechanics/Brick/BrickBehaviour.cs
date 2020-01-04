using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BrickBehaviour : MonoBehaviour
{
    [HideInInspector] public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    virtual public void OnCollisionEnter(Collision other)
    {
        if(other.relativeVelocity.magnitude > 2.5f)
        {
            audioSource.volume = 0.1f*other.relativeVelocity.magnitude;
            audioSource.Play();
        }
    }
}
