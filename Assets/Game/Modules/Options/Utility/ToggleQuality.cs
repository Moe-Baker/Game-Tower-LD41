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
	public class ToggleQuality : MonoBehaviour
	{
        [SerializeField]
        protected int index;
        public int Index { get { return index; } }

        public Toggle Toggle { get; protected set; }

        protected virtual void Start()
        {
            Toggle = GetComponent<Toggle>();

            Toggle.isOn = Index == References.Game.Options.Quality;

            Toggle.onValueChanged.AddListener(OnValueChanged);
        }

        protected virtual void OnValueChanged(bool newValue)
        {
            if(newValue)
                References.Game.Options.SetQuality(Index);
        }
	}
}