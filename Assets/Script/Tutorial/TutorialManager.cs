using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour {
    [SerializeField]
    private PlayerControlTutorial control;
    [SerializeField]
    private ScrollBgTutorial scroll;

    [SerializeField]
    private float centerPosX;
    [SerializeField]
    private float endPosX;
    [SerializeField]
    private float duration = 0.2f;

    [SerializeField]
    private GameObject bulletSpawner;

    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject returnButton;

    [SerializeField]
    private GameObject moveCanvas;
    [SerializeField]
    private GameObject shotCanvas;
    [SerializeField]
    private GameObject dodgeCanvas;
    [SerializeField]
    private GameObject dieCanvas;

    enum State
    {
        Start,
        Move,
        Shot,
        Dodge,
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
        moveCanvas.SetActive(false);
        shotCanvas.SetActive(false);
        dodgeCanvas.SetActive(false);
        dieCanvas.SetActive(false);
        bulletSpawner.SetActive(false);
    }

    private void SetupState()
    {
        SetupStateStart();
        SetupStateMove();
        SetupStateShot();
        SetupStateDodge();
        SetupStateDie();
        stateMachine.ChangeState(State.Start);
    }

    private void SetupStateStart()
    {
        State state = State.Start;
        Action<State> enter = (prev) => {  };
        Action update = () =>
        {
            if (control.HasStart)
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
            MoveIn(moveCanvas);
            control.EnableMove = true;
        };
        Action update = () =>
        {
            if (control.HasMove)
                nextButton.SetActive(true);
        };
        Action<State> exit = (next) => 
        {
            MoveOut(moveCanvas);
            nextButton.SetActive(false);
        };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateShot()
    {
        State state = State.Shot;
        Action<State> enter = (prev) =>
        {
            MoveIn(shotCanvas);
            control.EnableShot = true;
        };
        Action update = () =>
        {
            if (control.HasShot)
                nextButton.SetActive(true);
        };
        Action<State> exit = (next) => 
        {
            MoveOut(shotCanvas);
            nextButton.SetActive(false);
        };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateDodge()
    {
        State state = State.Dodge;
        Action<State> enter = (prev) =>
        {
            MoveIn(dodgeCanvas);
            control.EnableDodge = true;
            bulletSpawner.SetActive(true);
        };
        Action update = () =>
        {
            if (control.HasDodge)
                nextButton.SetActive(true);
        };
        Action<State> exit = (next) =>
        {
            MoveOut(dodgeCanvas);
            nextButton.SetActive(false);
            bulletSpawner.SetActive(false);
        };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateDie()
    {
        State state = State.Die;
        Action<State> enter = (prev) =>
        {
            StartCoroutine("DieEffect");
        };
        Action update = () =>{ };
        Action<State> exit = (next) =>{};
        stateMachine.Add(state, enter, update, exit);
    }

    IEnumerator DieEffect()
    {
        MoveIn(dieCanvas);
        returnButton.SetActive(false);

        yield return new WaitForSeconds(2f);
        
        MoveOut(dieCanvas);
        scroll.ScrollToBottom();
    }

    public void OnNext()
    {
        if (stateMachine.GetCurrentState().Key != State.Die)
            stateMachine.ChangeState(stateMachine.GetCurrentState().Key + 1);
    }
    public void OnReturn()
    {
        SceneController.Instance.Change(Scene.Title);
    }

    private void MoveIn(GameObject obj)
    {
        obj.SetActive(true);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.DOAnchorPosX(centerPosX, duration);
    }
    private void MoveOut(GameObject obj)
    {
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.DOAnchorPosX(endPosX, duration)
            .OnComplete(() => rect.gameObject.SetActive(false));
    }
}
