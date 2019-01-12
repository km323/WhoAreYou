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
    private GameObject dodgeCanvas1;
    [SerializeField]
    private GameObject dodgeCanvas2;
    [SerializeField]
    private GameObject dieCanvas;

    enum State
    {
        Start,
        Move,
        Shot,
        Dodge1,
        Dodge2,
        Die
    }

    StateMachine<State> stateMachine = new StateMachine<State>();

    void Start () {
        InactiveCanvas();
        SetupState();

        SoundManager.Instance.PlayBgm(BGM.Title);
	}
	
	void Update () {
        stateMachine.UpdateState();
    }

    private void InactiveCanvas()
    {
        nextButton.SetActive(false);
        moveCanvas.SetActive(false);
        shotCanvas.SetActive(false);
        dodgeCanvas1.SetActive(false);
        dodgeCanvas1.SetActive(false);
        dieCanvas.SetActive(false);
        bulletSpawner.SetActive(false);
    }

    private void SetupState()
    {
        SetupStateStart();
        SetupStateMove();
        SetupStateShot();
        SetupStateDodge1();
        SetupStateDodge2();
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

    private void SetupStateDodge1()
    {
        State state = State.Dodge1;
        Action<State> enter = (prev) =>
        {
            MoveIn(dodgeCanvas1);
            control.EnableLongTap = true;
        };
        Action update = () =>
        {
            if(control.HasLongTap)
                nextButton.SetActive(true);
        };
        Action<State> exit = (next) =>
        {
            MoveOut(dodgeCanvas1);
            nextButton.SetActive(false);
        };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateDodge2()
    {
        State state = State.Dodge2;
        Action<State> enter = (prev) =>
        {
            MoveIn(dodgeCanvas2);
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
            MoveOut(dodgeCanvas2);
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

        yield return new WaitForSeconds(2.5f);
        
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
