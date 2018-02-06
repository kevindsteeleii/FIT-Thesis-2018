using UnityEditor;

public class ThrowingDataAsset {

    [MenuItem("Assets/Create/ThrowingData")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<ThrowingData>();
    }
}

