using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGauageTutorial : DodgeGauge {
    [SerializeField]
    private PlayerControlTutorial control;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(control.EnableLongTap)
            UpdateMask();
    }

    // 4段階 (過ぎてる時間の割合：どのテクスチャ番号）  
    //0:0,　1/4:1,  1/2:2,  3/4:3,  1:4
    private void UpdateMask()
    {
        pressedTime = control.GetPlayerInput().TouchTime;

        if (ReachNeedTime(control.timeNeedDodge))
            SetTexture(recMask[4]);
        else if (ReachNeedTime(control.timeNeedDodge * thirdPhase))
            SetTexture(recMask[3]);
        else if (ReachNeedTime(control.timeNeedDodge * secondPhase))
            SetTexture(recMask[2]);
        else if (ReachNeedTime(control.timeNeedDodge * firstPhase))
            SetTexture(recMask[1]);
        else
            SetTexture(recMask[0]);
    }
}
