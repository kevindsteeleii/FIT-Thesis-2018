using UnityEngine;

/// <summary>
///	Takes input for ThrowModel
/// </summary>
public class ThrowController : Controller
{

	//[SerializeField]
	//protected ThrowModel model;

    protected virtual void Awake ()	{
        
	}

    protected virtual void Update()
    {
        //Mind you the "Grab" button doubles as 
        if (Input.GetButton("Grab") && !Ammo.emptyClip)
        {
            Ammo.shootLoad();
        }
    }

    protected void throwAngle()    {

    }
}

