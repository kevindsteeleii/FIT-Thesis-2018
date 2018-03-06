using UnityEngine;
/// <summary>
/// Used for the player options to save
/// </summary>
/// 
[CreateAssetMenu(fileName ="PlayerData",menuName ="DataAsset/PlayerData")]
public class PlayerData : ScriptableObject {
    //variables for movement
    [Range(0f, 8f)]
    public float runSpeed;

    [Range(1, 20)]
    public float fallMultiplier = 2.5f;

    //establishes the lowest jump possible upon quick release of jump button 
    [Range(1, 10)]
    public float lowJumpMultiplier = 5f;
    //for jumping player starts suspended above ground by default is not on ground
    [Range(0, 10)]
    public float jumpHeight;
    }
