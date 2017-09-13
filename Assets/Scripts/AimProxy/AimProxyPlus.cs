using UnityEngine;

public class AimProxyPlus : MonoBehaviour {

    [SerializeField]
    protected AimProxyModel model;

    protected virtual void Awake()
    {
        // check to see if the model variable is empty
        if (!model)
        {
            // if it is then get the model attached to the current GameObject
            model = this.gameObject.GetComponent<AimProxyModel>();
        }
    }
    /*adds additional functionality outside of the keyboardinputparameters
     it is triggered by the push and release of the input button labeled
     as "Aim"*/
       protected virtual void Update() { 
        if(Input.GetButtonDown("Aim"))
            {
                model.StartProgressUpdateLoop();
            }
        
       else if(Input.GetButtonUp("Aim"))
            {
                model.EndProgressUpdateLoop();
            }
        }
    }

