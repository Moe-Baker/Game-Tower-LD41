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
    [CreateAssetMenu(menuName = Game.MenuPath + "Selectable Color Transition Profile")]
	public class SelectableColorsProfile : ScriptableObject
	{
		[SerializeField]
        protected Color normal = Color.white;
        public Color Normal { get { return normal; } }

        [SerializeField]
        protected Color highlighted = Color.white;
        public Color Highlighted { get { return highlighted; } }

        [SerializeField]
        protected Color pressed = Color.grey;
        public Color Pressed { get { return pressed; } }

        [SerializeField]
        protected Color disabled = new Color(Color.grey.r, Color.grey.g, Color.grey.b, 0.75f);
        public Color Disabled { get { return disabled; } }

        [SerializeField]
        [Range(1f, 5f)]
        protected float multiplier = 1;
        public float Multiplier { get { return multiplier; } }

        [SerializeField]
        protected float fadeDuration = 0.1f;
        public float FadeDuration { get { return fadeDuration; } }

        public ColorBlock ColorBlock
        {
            get
            {
                return new ColorBlock()
                {
                    normalColor = normal,
                    highlightedColor = highlighted,
                    pressedColor = pressed,
                    disabledColor = disabled,
                    colorMultiplier = multiplier,
                    fadeDuration = fadeDuration
                };
            }
        }
    }
}