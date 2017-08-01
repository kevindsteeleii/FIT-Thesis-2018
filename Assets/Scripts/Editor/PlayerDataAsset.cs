using UnityEditor;
using UnityEngine;

public class PlayerDataAsset  {

    [MenuItem("Assets/Create/YourClass")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<PlayerData>();
    }
}
