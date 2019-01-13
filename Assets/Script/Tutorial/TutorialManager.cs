using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialManager : MonoBehaviour {
    [SerializeField]
    private GameObject whitePrefab;

    [SerializeField]
    private PlayerControlTutorial blackControl;
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
    private GameObject systemCanvas1;
    [SerializeField]
    private GameObject systemCanvas2;

    enum State
    {
        Start,
        Move,
        Shot,
        Dodge1,
        Dodge2,
        System1,
        System2,
    }

    StateMachine<State> stateMachine = new StateMachine<State>();

    private RecordTutorial record;
    private bool canChangeState;
    private bool continueState;

    void Start () {
        InactiveCanvas();
        InactiveObject();
        SetupState();

        record = FindObjectOfType<RecordTutorial>();

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
        systemCanvas1.SetActive(false);
        systemCanvas2.SetActive(false);
    }

    private void InactiveObject()
    {
        bulletSpawner.SetActive(false);
    }

    private void SetupState()
    {
        SetupStateStart();
        SetupStateMove();
        SetupStateShot();
        SetupStateDodge1();
        SetupStateDodge2();
        SetupStateSystem1();
        SetupStateSystem2();

        stateMachine.ChangeState(State.Start);
    }

    private void SetupStateStart()
    {
        State state = State.Start;
        Action<State> enter = (prev) => {  };
        Action update = () =>
        {
            if (blackControl.HasStart)
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
            canChangeState = true;
            MoveIn(moveCanvas);
            blackControl.EnableMove = true;
        };
        Action update = () =>
        {
            if (blackControl.HasMove)
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
            blackControl.EnableShot = true;
        };
        Action update = () =>
        {
            if (blackControl.HasShot)
                nextButton.SetActive(true);
        };
        Action<State> exit = (next) => 
        {
            MoveOut(shotCanvas);
            nextButton.SetActive(false);
            blackControl.GetPlayerInput().ResetTouchTime();
        };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateDodge1()
    {
        State state = State.Dodge1;
        Action<State> enter = (prev) =>
        {
            MoveIn(dodgeCanvas1);
            blackControl.EnableLongTap = true;
        };
        Action update = () =>
        {
            if(blackControl.HasLongTap)
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
            blackControl.EnableDodge = true;
            bulletSpawner.SetActive(true);
        };
        Action update = () =>
        {
            if (blackControl.HasDodge)
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

    private void SetupStateSystem1()
    {
        PlayerControlTutorial whiteControl = null;
        State state = State.System1;
        Action<State> enter = (prev) =>
        {
            canChangeState = false;
            MoveIn(systemCanvas1);
            nextButton.SetActive(true);
            blackControl.GetPlayerInput().DisableInput();
        };
        Action update = () =>
        {
            if(continueState)
            {
                continueState = false;
                MoveOut(systemCanvas1);
                nextButton.SetActive(false);

                GameObject white = Instantiate(whitePrefab, new Vector3(0, 5, 0), Quaternion.identity);
                whiteControl = white.GetComponent<PlayerControlTutorial>();
                blackControl.GetPlayerInput().EnableInput();
                record.StartRecord();
            }
            if (whiteControl != null && whiteControl.HasDie)
            {
                record.StopRecord();
                blackControl.GetPlayerInput().DisableInput();
                Camera.main.GetComponent<CameraRotateTutorial>().CameraRotate();
                stateMachine.ChangeState(State.System2);
            }

        };
        Action<State> exit = (next) =>
        {

        };
        stateMachine.Add(state, enter, update, exit);
    }

    private void SetupStateSystem2()
    {
        PlayerControlTutorial whiteControl = null;
        State state = State.System2;
        Action<State> enter = (prev) =>
        {
            StartCoroutine("System2Anim");
        };
        Action update = () =>
        {
            if (continueState)
            {
                continueState = false;
                MoveOut(systemCanvas2);
                nextButton.SetActive(false);

                GameObject white = Instantiate(whitePrefab, new Vector3(0, 5, 0), Quaternion.identity);
                whiteControl = white.GetComponent<PlayerControlTutorial>();
                blackControl.gameObject.SetActive(true);
                record.StartPlayRecord();
                scroll.ScrollToBottom();
            }

            if (whiteControl != null && whiteControl.HasDie)
            {
                Invoke("OnReturn", 2f);
            }
        };
        Action<State> exit = (next) =>
        {

        };
        stateMachine.Add(state, enter, update, exit);
    }
    IEnumerator System2Anim()
    {
        canChangeState = false;
        blackControl.GetPlayerInput().ResetTouchTime();
        yield return new WaitForSeconds(2f);

        MoveIn(systemCanvas2);
        nextButton.SetActive(true);
        blackControl.gameObject.SetActive(false);
    }

    private void ChangeState()
    {
        stateMachine.ChangeState(stateMachine.GetCurrentState().Key + 1);
    }

    public void OnNext()
    {
        if (canChangeState && stateMachine.GetCurrentState().Key != State.System2)
            Invoke("ChangeState", 0.1f);
        else
            continueState = true;
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
