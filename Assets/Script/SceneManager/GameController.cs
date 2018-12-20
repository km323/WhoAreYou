using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        GameObject gameController = new GameObject("GameController");
        gameController.AddComponent<GameController>();

        GameObject sceneController = new GameObject("SceneController");
        sceneController.AddComponent<SceneController>();
    }

    public void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(this);
    }
}
