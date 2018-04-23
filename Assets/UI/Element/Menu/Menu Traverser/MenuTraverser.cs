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
    [RequireComponent(typeof(Menu))]
	public class MenuTraverser : MonoBehaviour
	{
		[SerializeField]
        protected Menu menu;
        public Menu Menu { get { return menu; } }
		
        [SerializeField]
        protected Transition returnTransition;
        public Transition ReturnTransition
        {
            get
            {
                return returnTransition;
            }
            set
            {
                returnTransition = value;

                InitReturnTransition();
            }
        }
        protected virtual void InitReturnTransition()
        {
            if (returnTransition.IsValid)
                RegisterTransition(returnTransition);
        }

        [SerializeField]
        protected Transition[] transitions;
        public Transition[] Transitions { get { return transitions; } }
        protected virtual void InitTransitions(Menu menu)
        {
            for (int i = 0; i < transitions.Length; i++)
                RegisterTransition(transitions[i]);
        }

        protected virtual void GetComponents()
        {
            if (menu == null) menu = GetComponent<Menu>();
        }

        protected virtual void OnValidate()
        {
            GetComponents();
        }
        protected virtual void Reset()
        {
            GetComponents();
        }

        protected virtual void Start()
        {
            InitReturnTransition();

            InitTransitions(Menu);
        }

        public virtual void RegisterTransition(SelectableInputRelay controller, Menu target)
        {
            var transition = new Transition(controller, target);

            RegisterTransition(transition);
        }
        public virtual void RegisterTransition(Transition transition)
        {
            transition.Register(menu);
        }

        [Serializable]
        public class Transition
        {
            [SerializeField]
            [UnityEngine.Serialization.FormerlySerializedAs("controller")]
            protected SelectableInputRelay relay;
            public SelectableInputRelay Relay { get { return relay; } }

            [SerializeField]
            protected Menu target;
            public Menu Target { get { return target; } }

            public bool IsValid { get { return relay != null; } }

            public virtual void Register(Menu current)
            {
                relay.Register(() =>
                {
                    current.Close();

                    if(target)
                        target.Show();
                });
            }

            public Transition(SelectableInputRelay controller, Menu target)
            {
                this.relay = controller;
                this.target = target;
            }
        }
	}
}