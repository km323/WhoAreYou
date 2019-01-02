using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    [SerializeField]
    private PlayerEffectTutorial effect;
    [SerializeField]
    private PlayerControlTutorial control;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject returnButton;
    [SerializeField]
    private GameObject moveCanvas;
    [SerializeField]
    private GameObject shotCanvas;

    enum State
    {
        Start,
        Move,
        Shot,
        Doge,
        Die
    }

    StateMachine<State> stateMachine = new StateMachine<State>();

    void Start () {
        InactiveCanvas();
        SetupState();
	}
	
	void Update () {
        stateMachine.UpdateState();
    }

    private void InactiveCanvas()
    {
        nextButton.SetActive(false);
        returnButton.SetActive(false);
        moveCanvas.SetActive(false);
        shotCanvas.SetActive(false);
    }

    private void SetupState()
    {
        SetupStateStart();
        SetupStateMove();
        SetupStateShot();
        stateMachine.ChangeState(State.Start);
    }

    private void SetupStateStart()
    {
        State state = State.Start;
        Action<State> enter = (prev) => { effect.StartEffect(); };
        Action update = () =>
        {
            if (effect.GetHasStart())
                stateMachine.ChangeState(State.Move);
        };
        Action<State> exit = (next) => { };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateMove()
    {
        State state = State.Move;
        Action<State> enter = (prev) => 
        {
            moveCanvas.SetActive(true);
            control.EnableMove = true;
        };
        Action update = () =>
        {
            if (control.HasMove)
                nextButton.SetActive(true);
        };
        Action<State> exit = (next) => { nextButton.SetActive(false); };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateShot()
    {
        State state = State.Shot;
        Action<State> enter = (prev) =>
        {
            //moveCanvas.SetActive(true);
            //control.EnableMove = true;
        };
        Action update = () =>
        {
            //if (control.HasMove)
            //    nextButton.SetActive(true);
        };
        Action<State> exit = (next) => { /*nextButton.SetActive(false);*/ };
        stateMachine.Add(state, enter, update, exit);
    }

    public void OnNext()
    {
        if (stateMachine.GetCurrentState().Key != State.Die)
            stateMachine.ChangeState(stateMachine.GetCurrentState().Key + 1);
    }
}
