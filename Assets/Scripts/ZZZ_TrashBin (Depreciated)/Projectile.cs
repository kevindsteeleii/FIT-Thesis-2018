using UnityEngine;

/// <summary>
/// Class that houses logic for Projectile
/// </summary>
public class Projectile : Model
{
    //damage the projectile causes
    [Range (0,15)]
    public int damage;

}
