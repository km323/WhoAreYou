using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : SingletonMonoBehaviour<GameController> {
    public static int BestScore { get; set; }
    public static int CurrentScore { get; set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        GameObject gameController = new GameObject("GameController");
        gameController.AddComponent<GameController>();

        GameObject sceneController = new GameObject("SceneController");
        sceneController.AddComponent<SceneController>();

        GameObject soundManager = new GameObject("SoundManager");
        soundManager.AddComponent<SoundManager>();
    }

    protected override void Awake()
    {
        base.Awake();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);

        BestScore = -1;
    }
}
