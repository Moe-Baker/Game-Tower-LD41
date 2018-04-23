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
	public class CastleHealthSlider : MonoBehaviour
	{
		public Slider Slider { get; protected set; }

        public Castle Castle { get { return References.Level.Castle; } }

        protected virtual void Start()
        {
            Slider = GetComponent<Slider>();

            Slider.minValue = 0f;
            Slider.maxValue = Castle.MaxHealth;
            SetValue(Castle.Health);

            Castle.OnTookDamage += OnDamaged;
        }

        protected virtual void SetValue(float health)
        {
            Slider.value = health;
        }

        private void OnDamaged(float damage, IDamager damager)
        {
            SetValue(Castle.Health);
        }
    }
}