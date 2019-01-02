using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    //-----------------------------
    // class
    //-----------------------------
    public class StateData
    {
        readonly T _State;
        readonly Action<T> _EnterAction;
        readonly Action _UpdateAction;
        readonly Action<T> _ExitAction;
        public StateData(T state, Action<T> enter, Action update, Action<T> exit)
        {
            _State = state;
            _EnterAction = enter;
            _UpdateAction = update;
            _ExitAction = exit;
        }

        public T Key { get { return _State; } }

        public void Enter(T prev)
        {
            _EnterAction(prev);
        }
        public void UpdateState()
        {
            _UpdateAction();
        }
        public void Exit(T next)
        {
            _ExitAction(next);
        }
    }

    //-----------------------------
    // 変数
    //-----------------------------
    Dictionary<T, StateData> _StateDictionary = new Dictionary<T, StateData>();

    StateData _CurrentState;

    //-----------------------------
    // public 関数
    //-----------------------------
    public void Add(T state, Action<T> enter, Action update, Action<T> exit)
    {
        if (_StateDictionary.ContainsKey(state))
        {
            Debug.LogError("既にキーが含まれてます。" + state.ToString());
            return;
        }

        var s = new StateData(state, enter, update, exit);
        _StateDictionary.Add(state, s);
    }

    public void UpdateState()
    {
        if (_CurrentState == null)
            return;

        _CurrentState.UpdateState();
    }

    public void ChangeState(T state)
    {
        if (_StateDictionary.ContainsKey(state) == false)
        {
            Debug.LogError("キーが含まれていません。" + state.ToString());
            return;
        }

        T prevState = default(T);

        if (_CurrentState != null)
        {
            prevState = _CurrentState.Key;
            _CurrentState.Exit(state);
        }

        _CurrentState = _StateDictionary[state];
        _CurrentState.Enter(prevState);
    }

    public StateData GetCurrentState()
    {
        return _CurrentState;
    }

}
