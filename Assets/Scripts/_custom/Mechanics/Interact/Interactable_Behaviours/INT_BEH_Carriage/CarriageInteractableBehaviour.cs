using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarriageInteractableBehaviour : InteractableBehaviour
{
    public int nextLevel;
    bool toHub = false;
    public string tentScene;
    [Space]
    public string[] scenes;

    void Awake()
    {
        DontDestroyOnLoad(this.transform.parent.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded.");
        Transform placeHolder = GameObject.FindGameObjectWithTag("CarriagePlaceholder").transform;
        if(!placeHolder)
            return;

        transform.parent.position = placeHolder.position;
        transform.parent.rotation = placeHolder.rotation;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag != "InteractableBrick")
            return;

        if(!other.TryGetComponent(out Rigidbody brickRb))
            return;

        if(!brickRb.IsSleeping())
            return;

        other.transform.parent = transform.parent;
    }

    override public void Interact()
    {
        if(toHub)
        {
            SceneManager.LoadScene(tentScene, LoadSceneMode.Single);
            toHub = false;
            return;
        }
        if(nextLevel > scenes.Length-1)
        {
            nextLevel = 0;
        }
        SceneManager.LoadScene(scenes[nextLevel], LoadSceneMode.Single);
        nextLevel++;
        toHub = true;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
