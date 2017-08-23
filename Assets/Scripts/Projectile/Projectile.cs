using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{
    //damage the projectile causes
    [Range (0,15)]
    public int damage;
  
    private void OnCollisionEnter(Collision col)    {
        if (col.gameObject.CompareTag("Enemy")) {
            col.gameObject.GetComponent<Enemy>().takeDamage(damage);
            Destroy(this.gameObject);
            
        }
    }

}
