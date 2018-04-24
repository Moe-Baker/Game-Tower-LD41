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
	public class GameSpeedUI : MonoBehaviour
	{
        public Text label;
        public Button resetButton;
        public Button addButton;
        public Button subtractButton;

        private void Start()
        {
            resetButton.onClick.AddListener(ResetAction);
            addButton.onClick.AddListener(Add);
            subtractButton.onClick.AddListener(Subtract);

            Apply(1f);
        }

        void ResetAction()
        {
            Apply(1);
        }

        void Add()
        {
            Apply(Time.timeScale + 1);
        }

        void Subtract()
        {
            Apply(Time.timeScale - 1);
        }

        void Apply(float value)
        {
            Time.timeScale = Mathf.Clamp(value, 1f, 20f);

            label.text = Time.timeScale.ToString() ;
        }
    }
}