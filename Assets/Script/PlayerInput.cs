using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public bool HasTouch { get; private set; } //タップされてるか
    public bool SecondTap { get; private set; } //同時押ししてるか
    public bool QuickSwipe { get; private set; } //早いスワイプなのか
    public int TouchCount { get; private set; } //何タップあるのか
    public TouchPhase[] PhaseTouch { get; private set; } //タップのフェース
    public Vector2 Direction { get; private set; } //移動した方向

    private const int maxTouch = 2;
    private const float minMoveDis = 20f;

    public Vector2 touchPosition; //タップした位置
    private Vector2 oldPosition;

    public PlayerInput()
    {
        HasTouch = false;
        PhaseTouch = new TouchPhase[maxTouch];

        touchPosition = Vector2.zero;
        Direction = Vector2.zero;
        oldPosition = Vector2.zero;

        for (int i = 0; i < maxTouch; i++)
            PhaseTouch[i] = TouchPhase.Canceled;
    }

    public void Update()
    {
        HasTouch = false;
        PhaseTouch[1] = TouchPhase.Canceled;

        if (Application.isEditor)
            MouseInput();
        else
            TouchInput();

        if (PhaseTouch[0] == TouchPhase.Began)
            oldPosition = touchPosition;

        if (HasTouch)
        {
            SecondTap = HasSecondTap();
            Direction = CalcDirection();
            oldPosition = touchPosition;
        }
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
            TouchCount = 1;
            touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    //タッチインプット
    private void TouchInput()
    {
        TouchCount = Input.touchCount;

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

        if (HasQuickSwipe(touch[0]))
            QuickSwipe = true;
        else
            QuickSwipe = false;
    }

    //早いスワイプ
    private bool HasQuickSwipe(Touch touch)
    {
        if (touch.deltaPosition.magnitude >= minMoveDis)
            return true;

        return false;
    }

    //同時押し
    private bool HasSecondTap()
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