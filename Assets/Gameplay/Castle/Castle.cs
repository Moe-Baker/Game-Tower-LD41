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
	public class Castle : Entity
	{
		[SerializeField]
        protected float maxHealth;
        public float MaxHealth { get { return maxHealth; } }

        public override void Heal(float points)
        {
            if (health + points > maxHealth)
                points = maxHealth - health;

            base.Heal(points);
        }

        protected virtual void Reset()
        {
            maxHealth = health;
        }

        protected override void DeathAction(IDamager damager)
        {

        }
    }
}