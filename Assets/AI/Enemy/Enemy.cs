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
	public class Enemy : MonoBehaviour
	{
        public AI AI { get; protected set; }
        public Entity Entity { get { return AI.Entity; } }
		public AINavigator Navigator { get { return AI.Navigator; } }

        public Castle Castle { get { return References.Level.Castle; } }

        public ModulesManager Modules { get; protected set; }
        public class ModulesManager : MoeLinkedModuleManager<Module, Enemy>
        {

        }

        public class Module : MoeLinkedBehaviourModule<Enemy>
        {

        }

        protected virtual void Start()
        {
            AI = GetComponent<AI>();

            References.Level.EnemiesManager.Add(this);

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
        }

        protected virtual void Update()
        {
            Navigator.SetDestination(Castle.transform.position);
        }
	}
}