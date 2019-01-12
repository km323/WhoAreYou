using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeGaugeTutorial : DodgeGauge {
    [SerializeField]
    private PlayerControlTutorial control;

	// Use this for initialization
	void Start () {
        foreach (GameObject obj in gauge)
            obj.SetActive(false);
        pressedTimeNeed = control.timeNeedDodge;
    }
	
	// Update is called once per frame
	void Update () {
        if (control.EnableLongTap)
        {
            pressedTime = control.GetPlayerInput().TouchTime;
            UpdateMask();
        }
    }

    //private void UpdateMask()
    //{
    //    

    //    if (ReachNeedTime(control.timeNeedDodge))
    //        gauge[0].SetActive(true);
    //    else if (ReachNeedTime(control.timeNeedDodge * secondPhase))
    //        gauge[1].SetActive(true);
    //    else if (ReachNeedTime(control.timeNeedDodge * firstPhase))
    //        gauge[2].SetActive(true);
    //    else
    //    {
    //        foreach (GameObject obj in gauge)
    //            obj.SetActive(false);
    //    }
    //}
}
