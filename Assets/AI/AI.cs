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
	public class AI : MonoBehaviour
	{
        public ModulesManager Modules { get; protected set; }
		public class ModulesManager : MoeLinkedModuleManager<Module, AI>
        {

        }

        public AINavigator Navigator { get; protected set; }
        public class Module : MoeLinkedBehaviourModule<AI>
        {

        }

        protected virtual void Start()
        {
            InitModules();
        }

        protected virtual void InitModules()
        {
            Modules = new ModulesManager();

            AddModules();

            Modules.Init(this);
        }
        protected virtual void AddModules()
        {
            Modules.AddAll(gameObject);

            Navigator = Modules.Find<AINavigator>();
        }
	}
}