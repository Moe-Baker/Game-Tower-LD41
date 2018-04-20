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
    [CreateAssetMenu(menuName = MenuPath + "Scenes")]
	public class GameScenes : Game.Module
	{
		[SerializeField]
        protected GameScene mainMenu;
        public GameScene MainMenu { get { return mainMenu; } }
        public virtual void LoadMainMenu()
        {
            LoadScene(mainMenu.Name);
        }

        [SerializeField]
        protected GameLevel[] levels;
        public GameLevel[] Levels { get { return levels; } }
        public virtual bool ContainsLevel(string name)
        {
            for (int i = 0; i < levels.Length; i++)
                if (levels[i].name == name) return true;

            return false;
        }
        public virtual GameLevel FindLevel(string name)
        {
            for (int i = 0; i < levels.Length; i++)
                if (levels[i].name == name)
                    return levels[i];

            throw new ArgumentException("No Level Defined In Scenes Module " + this.name.Enclose() + " With the Name " + name);
        }

        public virtual void LoadFirstLevel()
        {
            LoadLevel(0);
        }
        public virtual void LoadLevel(string name)
        {
            LoadLevel(FindLevel(name));
        }
        public virtual void LoadLevel(int index)
        {
            LoadLevel(levels[index]);
        }
        protected virtual void LoadLevel(GameLevel level)
        {
            LoadScene(level.Scene.Name);
        }

        public virtual void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}