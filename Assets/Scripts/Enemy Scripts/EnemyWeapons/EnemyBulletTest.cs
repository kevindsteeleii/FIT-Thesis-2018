using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletTest : MonoBehaviour {

    public event Action On_BecomeAmmo_Sent;
    public bool isGrabby = false;   //bool that toggles on start the mesh/logic for a grabbable/non-grabbable projectile
    [SerializeField] GameObject grabModel;
    [SerializeField] GameObject shootModel;

    SphereCollider sphereCollider;

    [Tooltip("The amount of damage done by the projectile")]
    [Range(0, 15)]
    public int hitPoint;

    public event Action<int> On_TransferDamage_Sent;

    // Use this for initialization
    void Start () {
        grabModel.SetActive(isGrabby);
        shootModel.SetActive(!isGrabby);

        if (sphereCollider != null)
            return;
        else
        {
            sphereCollider = gameObject.GetComponent<SphereCollider>();
        }
	}
	
    public void SetGrabbable(bool grabbable)
    {
        isGrabby = grabbable;
    }

    public bool IsGrabbable()
    {
        return isGrabby;
    }

	// Update is called once per frame
	void Update () {
        grabModel.SetActive(isGrabby);
        shootModel.SetActive(!isGrabby);
        AttackDetection();
	}

    protected virtual void BecomeProjectile()
    {
        On_BecomeAmmo_Sent += Ammo.instance.Load;
        //transmits to Ammo handling manager as a subject of subscription
        if (On_BecomeAmmo_Sent != null)
        {
            Debug.Log("Enemy bullet became your ammo");
            On_BecomeAmmo_Sent();
        }
        Destroy();
    }

    protected virtual void AttackDetection()
    {
        Collider[] cols = Physics.OverlapSphere(sphereCollider.bounds.center, .58f, LayerMask.GetMask("Player"));
        AttackPlayer(cols);
    }

    protected virtual void AttackPlayer(Collider[] cols)
    {
        foreach (var collider in cols)
        {
            if (collider.tag == "Player")
            {
                Debug.Log("Player hit");
                On_TransferDamage_Sent += collider.gameObject.GetComponent<PlayerStats>().TakeDamage;
                On_TransferDamage_Sent(hitPoint);
                On_TransferDamage_Sent -= collider.gameObject.GetComponent<PlayerStats>().TakeDamage;
                Destroy();
            }
        }
    }

    /// <summary>
    /// "Destroys" body by setting the game object the script is attached to to inactive
    /// </summary>
    /// <param name="obj"></param>
    private void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && isGrabby)
        {
            BecomeProjectile();
        }
    }
}
#region TODO list, refactoring etc
/****************************************TODO************************************************
  1-
  2-
  3-
  4-
  5-
  6-
****************************************TODO************************************************/
#endregion