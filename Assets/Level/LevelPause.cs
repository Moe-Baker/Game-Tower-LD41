using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

using Moe.Tools;

namespace Game
{
    public class LevelPause : Level.Module
    {
        protected LevelPauseState _state = LevelPauseState.None;
        public LevelPauseState State
        {
            get
            {
                return _state;
            }
            set
            {
                SetState(value);
            }
        }

        public virtual bool IsNone { get { return State == LevelPauseState.None; } }
        public virtual bool IsSoft { get { return State == LevelPauseState.Soft; } }
        public virtual bool IsFull { get { return State == LevelPauseState.Full; } }

        public event Action<LevelPauseState> OnChanged;

        public virtual void SetState(LevelPauseState newValue)
        {
            if (newValue == State) return;

            ProcessStateChange(newValue);

            if (OnChanged != null)
                OnChanged(newValue);
        }
        protected virtual void ProcessStateChange(LevelPauseState newValue)
        {
            _state = newValue;

            Time.timeScale = newValue == LevelPauseState.Full ? 0f : 1f;
            AudioListener.pause = newValue == LevelPauseState.Full;
        }


        public PauseMenu PauseMenu { get { return Level.Menu.PauseMenu; } }


        protected virtual void Update()
        {
            ProcessInput();
        }


        [SerializeField]
        protected KeyCodeList inputKeys = new KeyCodeList(KeyCode.Escape, KeyCode.Home);
        public KeyCodeList InputKeys { get { return inputKeys; } }

        protected virtual void ProcessInput()
        {
            if (IsNone)
            {
                if (inputKeys.GetInputDown())
                    State = LevelPauseState.Full;
            }
        }


        protected virtual void OnDestroy()
        {
            if (State != LevelPauseState.None)
                ProcessStateChange(LevelPauseState.None);
        }
    }

    public enum LevelPauseState
    {
        None, Soft, Full
    }
}