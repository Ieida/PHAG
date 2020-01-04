using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public float health;
    public Image hurtimage;

    public void Hurt(float value)
    {
        health -= value;
        hurtimage.color = new Color(255.0f, 0.0f, 0.0f, 0.27f);
        Invoke("RecoverHealth", 18.5f);
        if(health > 0.0f)
            return;

        Application.Quit();
    }

    void RecoverHealth()
    {
        hurtimage.color = new Color(255.0f, 0.0f, 0.0f, 0.0f);
    }
}
