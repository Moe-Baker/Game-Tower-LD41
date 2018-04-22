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
    [RequireComponent(typeof(Text))]
	public class ScoreText : MonoBehaviour
	{
		public Text Text { get; protected set; }

        public ScoreManager ScoreManager { get { return References.Level.ScoreManager; } }

        protected virtual void Start()
        {
            Text = GetComponent<Text>();

            Set(ScoreManager.Value);

            ScoreManager.OnChanged += OnChanged;
        }

        protected virtual void Set(uint points)
        {
            Text.text = "Points: " + points.ToString();
        }

        protected virtual void OnChanged(uint newValue)
        {
            Set(newValue);
        }
	}
}