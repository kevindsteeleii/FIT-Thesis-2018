using UnityEditor;

public class GrabDataAsset {
    [MenuItem("Assets/Create/Grab Data")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<GrabData>();
    }
}
