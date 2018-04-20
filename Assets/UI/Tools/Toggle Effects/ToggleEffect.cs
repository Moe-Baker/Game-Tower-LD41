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
    [RequireComponent(typeof(Toggle))]
	public class ToggleEffect : MonoBehaviour
	{
		[SerializeField]
        protected Toggle toggle;
        public Toggle Toggle { get { return toggle; } }

        protected virtual void GetComponents()
        {
            if (toggle == null) toggle = GetComponent<Toggle>();
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
            toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected virtual void OnValueChanged(bool isOn)
        {

        }
    }
}