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

namespace Game
{
    public class PauseMenu : Menu
    {
        [SerializeField]
        protected Button resume;
        public Button Resume { get { return resume; } }

        public virtual LevelPause Pause { get { return References.Level.Pause; } }

        public virtual bool ToggleInput
        {
            get
            {
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
                    return true;

                return false;
            }
        }

        public virtual void Init()
        {
            resume.onClick.AddListener(OnResume);
        }

        protected virtual void OnResume()
        {
            if (!Visibile) return;
            Pause.State = LevelPauseState.None;
        }

        public virtual void UpdateState()
        {
            Visibile = Pause.State == LevelPauseState.Full;
        }
    }
}