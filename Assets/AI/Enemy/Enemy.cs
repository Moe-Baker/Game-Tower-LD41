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
	public class Enemy : MonoBehaviour
	{
        public AI AI { get; protected set; }

		public AINavigator Navigator { get { return AI.Navigator; } }

        public Castle Castle { get { return References.Level.Castle; } }

        protected virtual void Start()
        {
            AI = GetComponent<AI>();
        }

        protected virtual void Update()
        {
            Navigator.SetDestination(Castle.transform.position);
        }
	}
}