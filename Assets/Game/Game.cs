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
    [CreateAssetMenu(menuName = MenuPath + "Asset")]
	public partial class Game : ScriptableObjectResourceSingleton<Game>
	{
        public const string MenuPath = "Game/";

        #region Modules
        protected ModulesManager modules;
        public ModulesManager Modules { get { return modules; } }
        public class ModulesManager : MoeBasicModuleManager<Module>
        {
            public virtual void Configure()
            {
                ForAll(ConfigureModule);
            }
            protected virtual void ConfigureModule(Module module)
            {
                module.Configure();
            }
        }

        [SerializeField]
        protected GameScenes scenes;
        public GameScenes Scenes { get { return scenes; } }

        [SerializeField]
        protected GameOptions options;
        public GameOptions Options { get { return options; } }
        #endregion

        public SceneAcessor SceneAcessor { get; protected set; }

        public class Module : MoeBasicScriptableModule
        {
            public virtual Game Game { get { return References.Game; } }

            public const string MenuPath = Game.MenuPath + "Modules/";

            public virtual void Configure()
            {

            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void GameLoad()
        {
            if (!InstanceAvailable)
                throw new Exception("No Game Asset Found");

            Instance.Configure();
        }

        #region Configure
        protected virtual void Configure()
        {
            SceneAcessor = SceneAcessor.Create();

            ConfigureModules();

            SceneManager.sceneLoaded += OnSceneLoad;
        }

        protected virtual void ConfigureModules()
        {
            modules = new ModulesManager();

            AddModules();

            Modules.Configure();
        }
        protected virtual void AddModules()
        {
            Modules.Add(scenes);
            Modules.Add(options);
        }
        #endregion

        protected virtual void OnSceneLoad(Scene scene, LoadSceneMode mode)
        {
            Init();
        }

        #region Init
        protected virtual void Init()
        {
            InitModules();
        }

        protected virtual void InitModules()
        {
            Modules.Init();
        }
        #endregion

        public virtual void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public static partial class References
    {
        public static Game Game { get { return Game.Instance; } }
    }
}