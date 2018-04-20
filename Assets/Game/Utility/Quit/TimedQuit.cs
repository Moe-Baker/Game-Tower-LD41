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
	public class TimedQuit : MonoBehaviour
	{
		[SerializeField]
        protected float delay = 2f;
        public float Delay { get { return delay; } }

        protected virtual void OnEnable()
        {
            Invoke(nameof(Action), delay);
        }

        protected virtual void Action()
        {
            References.Game.Quit();
        }
    }
}