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
	public class InGameMenu : GameMenu
	{
        [SerializeField]
        protected PauseMenu pauseMenu;
        public PauseMenu PauseMenu { get { return pauseMenu; } }

        [SerializeField]
        protected HUDMenu _HUD;
        public HUDMenu HUD { get { return _HUD; } }

        public override void Init()
        {
            base.Init();

            pauseMenu.Init();
            HUD.Init();
        }
    }
}