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
    [RequireComponent(typeof(Button))]
	public class ButtonOnBack : MonoBehaviour
	{
        public Button Button { get; protected set; }

        protected virtual void Awake()
        {
            Button = GetComponent<Button>();
        }

        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
                Action();
        }

        protected virtual void Action()
        {
            Button.onClick.Invoke();
        }
    }
}