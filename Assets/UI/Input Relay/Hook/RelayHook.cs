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
	public class RelayHook : MonoBehaviour
	{
        public SelectableInputRelay Relay { get; protected set; }

        protected virtual void Start()
        {
            Relay = GetComponent<SelectableInputRelay>();

            if (Relay == null)
                throw MoeTools.ExceptionTools.Templates.MissingDependacny<SelectableInputRelay, QuitRelayHook>(this.name);

            Relay.Register(Action);
        }

        protected virtual void Action()
        {
            
        }
    }
}