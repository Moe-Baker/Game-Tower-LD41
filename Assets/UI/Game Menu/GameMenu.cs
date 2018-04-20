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
	public abstract class GameMenu : MonoBehaviour
	{
        public static GameMenu Current
        {
            get
            {
                if (MainMenu.Current)
                    return MainMenu.Current;
                if (Level.Current)
                    return Level.Current.Menu;
                else
                    return null;
            }
        }

        [SerializeField]
        protected OptionsMenu optionsMenu;
        public OptionsMenu OptionsMenu { get { return optionsMenu; } }

        public virtual void Init()
        {
            optionsMenu.Init();
        }
    }
}