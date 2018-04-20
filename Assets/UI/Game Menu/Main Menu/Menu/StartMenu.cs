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
	public class StartMenu : Menu
    {
		[SerializeField]
        protected Button newGame;
        public Button NewGame { get { return newGame; } }

        public virtual void Init()
        {
            newGame.onClick.AddListener(OnNewGame);
        }

        public virtual void OnNewGame()
        {
            References.Game.Scenes.LoadFirstLevel();
        }
    }
}