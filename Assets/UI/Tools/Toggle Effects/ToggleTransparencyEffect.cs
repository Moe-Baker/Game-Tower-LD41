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
    [RequireComponent(typeof(CanvasGroup))]
	public class ToggleTransparencyEffect : ToggleEffect
    {
        [SerializeField]
        [Range(0f, 1f)]
        protected float on = 1f;
        public float On { get { return on; } }

        [SerializeField]
        [Range(0f, 1f)]
        protected float off = 0.8f;
        public float Off { get { return off; } }

        [SerializeField]
        protected CanvasGroup canvasGroup;
        public CanvasGroup CanvasGroup { get { return canvasGroup; } }
        public virtual float Transparency
        {
            get
            {
                return canvasGroup.alpha;
            }
            protected set
            {
                canvasGroup.alpha = value;
            }
        }

        protected override void GetComponents()
        {
            base.GetComponents();

            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
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
        protected virtual void SetState(bool isOn)
        {
            Transparency = isOn ? on : off;
        }
	}
}