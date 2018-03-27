using UnityEngine;
using System;

public class PlatformRemoverControl : Singleton<PlatformRemoverControl>
{

    public Transform target;
    float horizontalOffSet;
    Vector3 respawnPos, hidden;

    /// <summary>
    /// Event that sends the Vector3 location of the platform remover to a recipient
    /// </summary>
    public event Action<Vector3> On_PlatformRemoverPass_Sent;

    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        respawnPos = transform.position;
        hidden = new Vector3(-99f, -99f, -99f);
        horizontalOffSet = Mathf.Abs(transform.position.x - target.position.x);

        GameManager.instance.onGameOverState += OnGameOverState;
        GameManager.instance.On_RestartState_Sent += OnRestartState;
    }

    protected virtual void Update()
    {
        Vector3 pos = transform.position;
        pos.x = target.position.x - horizontalOffSet;
        transform.position = pos;
        //sends the platformRemover's location to a subscriber/listener
        if (On_PlatformRemoverPass_Sent != null)
        {
            On_PlatformRemoverPass_Sent(transform.position);
        }

    }

    /// <summary>
    /// OnGameOverState this function whisks platformRemover far away from where the platforms may respawn 
    /// </summary>
    protected virtual void OnGameOverState()
    {
        transform.position = hidden;
    }

    /// <summary>
    /// Upon RestartState this function puts the platformRemover back in its initial spot
    /// </summary>
    protected virtual void OnRestartState()
    {
        transform.position = respawnPos;
    }


}
