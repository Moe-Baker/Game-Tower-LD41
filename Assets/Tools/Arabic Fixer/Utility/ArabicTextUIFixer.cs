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

namespace Moe.ArabicFixer
{
    [RequireComponent(typeof(Text))]
    [ExecuteInEditMode]
	public class ArabicTextUIFixer : MonoBehaviour
	{
		[SerializeField]
        [TextArea]
        protected string text;
        public string Text { get { return text; } set { text = value; UpdateText(); } }

        [SerializeField]
        protected bool updateAtRuntime = false;
        public bool UpdateAtRuntime { get { return updateAtRuntime; } }

        private Text _label;

        public Text Label
        {
            get
            {
                if (_label == null)
                    _label = GetComponent<Text>();

                return _label;
            }
        }

        void Start()
        {
            UpdateText();
        }

        void Update()
        {
            if (Application.isPlaying && !updateAtRuntime)
                return;

            UpdateText();
        }

        void UpdateText()
        {
            Label.text = ArabicFixer.Process(text);
        }
    }
}