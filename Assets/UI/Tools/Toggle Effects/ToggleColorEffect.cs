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
	public class ToggleColorEffect : ToggleEffect
    {
        [SerializeField]
        protected Graphic graphic;
        public Graphic Graphic { get { return graphic; } }

        [SerializeField]
        protected Color on = Color.white;
        public Color On { get { return on; } }

        [SerializeField]
        protected Color off = Color.white;
        public Color Off { get { return off; } }

        protected override void Reset()
        {
            base.Reset();

            graphic = toggle.targetGraphic;
        }

        protected override void Start()
        {
            base.Start();

            UpdateState();
        }

        protected override void OnValueChanged(bool isOn)
        {
            base.OnValueChanged(isOn);

            SetState(isOn);
        }

        protected virtual void UpdateState()
        {
            SetState(Toggle.isOn);
        }
        protected virtual void SetState(bool value)
        {
            graphic.color = value ? on : off;
        }
	}
}