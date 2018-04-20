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
    [RequireComponent(typeof(OptionsBox))]
	public class QualityOptionsBox : MonoBehaviour
	{
		public OptionsBox OptionsBox { get; protected set; }

        [SerializeField]
        protected string[] names;
        public string[] Names { get { return names; } }

        public GameOptions Options { get { return References.Game.Options; } }

        protected virtual void Reset()
        {
            names = QualitySettings.names;
        }

        protected virtual void Start()
        {
            OptionsBox = GetComponent<OptionsBox>();

            OptionsBox.Options.Clear();

            for (int i = 0; i < names.Length; i++)
                OptionsBox.Options.Add(names[i]);

            OptionsBox.Value = Options.Quality;

            OptionsBox.OnValueChanged.AddListener(OnValueChanged);
        }

        protected virtual void OnValueChanged(int newValue)
        {
            Options.SetQuality(newValue);
        }
    }
}