using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    //最初の入力
    public delegate void OnFirstTap();
    public event OnFirstTap onFirstTap;

    public bool HasTouch { get; private set; } //タップされてるか
    public bool SameTimeTap { get; private set; } //同時押ししてるか
    public bool QuickSwipe { get; private set; } //早いスワイプなのか
    public bool HasReleased { get; private set; } //指離したか
    public int TouchCount { get; private set; } //何タップあるのか
    public float TouchTime { get; private set; } //長押ししてる時間
    public TouchPhase[] PhaseTouch { get; private set; } //タップのフェース
    public Vector2 Direction { get; private set; } //移動した方向
    public bool SameTimeTapBegin { get; private set; }

    private const int maxTouch = 2;
    private const float minMoveDis = 15f;

    private Vector2 touchPosition; //タップした位置
    private Vector2 oldPosition;
    private bool firstTapDone;
    private bool enableInput;


    public PlayerInput()
    {
        firstTapDone = false;
        HasTouch = false;
        PhaseTouch = new TouchPhase[maxTouch];
        touchPosition = Vector2.zero;
        Direction = Vector2.zero;
        oldPosition = Vector2.zero;
        enableInput = true;

        for (int i = 0; i < maxTouch; i++)
            PhaseTouch[i] = TouchPhase.Canceled;

        GameMain.OnNextGame += ResetTouchTime;
    }

    public void Update()
    {
        if (!enableInput)
            return;

        ResetTouchState();

        if (Application.isEditor)
            MouseInput();
        else
            TouchInput();

        if (PhaseTouch[0] == TouchPhase.Began)
            oldPosition = touchPosition;

        if (HasTouch)
        {
            SetLongPressTime();
            SameTimeTap = HasSecondTap();
            HasReleased = HasReleaseFinger();
            Direction = CalcDirection();
            oldPosition = touchPosition;
            SameTimeTapBegin = HasSecondTapBegin();
        }
    }

    public void EnableInput()
    {
        enableInput = true;
    }

    public void DisableInput()
    {
        enableInput = false;
    }

    public void ResetTouchTime()
    {
        TouchTime = 0f;
    }

    public PlayerInput GetTouch()
    {
        return this;
    }

    //マウスインプット
    private void MouseInput()
    {
        #region right click
        if (Input.GetMouseButton(1))
        {
            PhaseTouch[0] = TouchPhase.Moved;
            HasTouch = true;
        }

        if (Input.GetMouseButtonDown(1))
        {
            PhaseTouch[0] = TouchPhase.Began;
            HasTouch = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            PhaseTouch[0] = TouchPhase.Ended;
            HasTouch = true;
        }
        #endregion

        #region left click
        if (Input.GetMouseButton(1))
        {
            PhaseTouch[1] = TouchPhase.Moved;
        }
        if (Input.GetMouseButtonDown(0))
        {
            PhaseTouch[1] = TouchPhase.Began;
        }
        if (Input.GetMouseButtonUp(0))
        {
            PhaseTouch[1] = TouchPhase.Ended;
        }
        #endregion

        if (HasTouch)
        {
            FirstTapDone();
            TouchCount = 1;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    //タッチインプット
    private void TouchInput()
    {
        TouchCount = Input.touchCount;
        QuickSwipe = false;

        if (TouchCount <= 0 || TouchCount > maxTouch)
            return;
        
        Touch[] touch = new Touch[TouchCount];
        

        for (int i = 0; i < touch.Length; i++)
        {
            touch[i] = Input.GetTouch(i);
            PhaseTouch[i] = touch[i].phase;
        }
        touchPosition = Camera.main.ScreenToWorldPoint(touch[0].position);

        HasTouch = true;

        FirstTapDone();

        if (HasQuickSwipe(touch[0]))
            QuickSwipe = true;
        else
            QuickSwipe = false;
    }

    //最初の入力
    private void FirstTapDone()
    {
        if (firstTapDone)
            return;

        firstTapDone = true;
        if (onFirstTap != null)
            onFirstTap();
    }

    private void ResetTouchState()
    {
        if (HasReleased)
            TouchTime = 0f;
        HasTouch = false;
        SameTimeTap = false;
        HasReleased = false;
        SameTimeTapBegin = false;
        PhaseTouch[1] = TouchPhase.Canceled;
    }

    //早いスワイプ
    private bool HasQuickSwipe(Touch touch)
    {
        if (touch.phase == TouchPhase.Ended && touch.deltaPosition.magnitude >= minMoveDis)
            return true;

        return false;
    }

    //同時押し
    private bool HasSecondTap()
    {
        if (PhaseTouch[1] == TouchPhase.Ended)
            return true;

        return false;
    }
    
    private bool HasReleaseFinger()
    {
        if (PhaseTouch[0] == TouchPhase.Ended)
            return true;

        return false;
    }

    private void SetLongPressTime()
    {
        if (PhaseTouch[0] == TouchPhase.Began)
        {
            TouchTime = 0f;
            TouchTime += Time.deltaTime;
        }
        else if (PhaseTouch[0] == TouchPhase.Moved || PhaseTouch[0] == TouchPhase.Stationary)
        {
            TouchTime += Time.deltaTime;
        }
    }
    private bool HasSecondTapBegin()
    {
        if (PhaseTouch[1] == TouchPhase.Began)
            return true;

        return false;
    }

    //スワイプの方向
    private Vector2 CalcDirection()
    {
        return (touchPosition - oldPosition).normalized;
    }
}