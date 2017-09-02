using UnityEditor;

public class PlayerStatsAsset  {

    [MenuItem("Assets/Create/StatsData")]
    public static void CreateAsset()
    { 
        ScriptableObjectUtility.CreateAsset<StatsData>();
    }
}
