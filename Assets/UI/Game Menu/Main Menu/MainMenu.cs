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
	public class MainMenu : GameMenu
	{
        new public static MainMenu Current { get; protected set; }
        protected virtual void SetSingeltion()
        {
            Current = this;
        }

		[SerializeField]
        protected TitleMenu titleMenu;
        public TitleMenu TitleMenu { get { return titleMenu; } }

        [SerializeField]
        protected StartMenu startMenu;
        public StartMenu StartMenu { get { return startMenu; } }

        protected virtual void Awake()
        {
            SetSingeltion();
        }


        protected virtual void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            titleMenu.Init();
            startMenu.Init();
        }
    }
}