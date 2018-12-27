using UnityEngine;

public class ResettableScriptableObject : ScriptableObject
{
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CallBeforPlayScene()
    {
        // recently, unity editor will crash
        // recommend SaveProject before playing
        //	UnityEditor.AssetDatabase.SaveAssets ();
    }
#endif

    protected virtual void OnEnable()
    {
#if UNITY_EDITOR
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == true)
        {
            UnityEditor.EditorApplication.playmodeStateChanged += () => {
                if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode == false)
                {
                    Resources.UnloadAsset(this);
                }
            };
        }
#endif
    }
}