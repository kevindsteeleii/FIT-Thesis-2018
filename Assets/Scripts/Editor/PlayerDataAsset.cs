using UnityEditor;
using UnityEngine;

public class PlayerDataAsset  {

    [MenuItem("Assets/Create/PlayerData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<PlayerData>();
    }
}
