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
	public class Unit : MonoBehaviour
	{
        public Tower Tower { get; protected set; }

        public ModulesManager Modules { get; protected set; }
        public class ModulesManager : MoeLinkedModuleManager<Module, Unit>
        {

        }

        public class Module : MoeLinkedBehaviourModule<Unit>
        {
            public virtual Unit Unit { get { return Link; } }
            public virtual Tower Tower { get { return Unit.Tower; } }
        }

        public virtual void Init(Tower tower)
        {
            this.Tower = tower;

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
	}
}