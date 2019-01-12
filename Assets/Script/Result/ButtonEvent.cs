using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour {
    [SerializeField]
    private ResultProperty property;

    private StageManager stageManager;

    public void OnPointerClickTitle()
    {
        if(GameMain.GetCurrentState() == GameMain.BLACK)
            property.SetTitleButtonColor(stageManager.GetColorBlack());
        else
            property.SetTitleButtonColor(stageManager.GetColorWhite());
    }

    public void OnPointerExitTitle()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            property.SetTitleButtonColor(property.GetColorWhite());
        else
            property.SetTitleButtonColor(property.GetColorBlack());
    }

    public void OnPointerClickRetry()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            property.SetRetryButtonColor(stageManager.GetColorBlack());
        else
            property.SetRetryButtonColor(stageManager.GetColorWhite());
    }

    public void OnPointerExitRetry()
    {
        if (GameMain.GetCurrentState() == GameMain.BLACK)
            property.SetRetryButtonColor(property.GetColorWhite());
        else
            property.SetRetryButtonColor(property.GetColorBlack());
    }

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }
}
