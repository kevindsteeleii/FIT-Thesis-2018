using UnityEngine;

public class PlatformRemoverControl : MonoBehaviour
{

    public Transform target;
    float horizontalOffSet;
    Vector3 respawnPos, hidden;

    // Use this for initialization
    void Start()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        respawnPos = this.transform.position;
        hidden = new Vector3(-99f, -99f, -99f);
        horizontalOffSet = Mathf.Abs(this.transform.position.x - target.position.x);

        GameManager.instance.onGameOverState += OnGameOverState;
        GameManager.instance.onRestartState += OnRestartState;
    }

    protected virtual void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x = target.position.x - horizontalOffSet;
        this.transform.position = pos;

    }

    /// <summary>
    /// OnGameOverState this function whisks platformRemover far away from where the platforms may respawn 
    /// </summary>
    protected virtual void OnGameOverState()
    {
        this.transform.position = hidden;
    }

    /// <summary>
    /// Upon RestartState this function puts the platformRemover back in its initial spot
    /// </summary>
    protected virtual void OnRestartState()
    {
        this.transform.position = respawnPos;
    }


}
