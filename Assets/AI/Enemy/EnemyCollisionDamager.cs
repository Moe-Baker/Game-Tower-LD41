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
	public class EnemyCollisionDamager : Enemy.Module
	{
        [SerializeField]
        protected bool suicideOnHit = true;
        public bool SuicideOnHit { get { return suicideOnHit; } }

        [SerializeField]
        protected float damage = 10;
        public float Damage { get { return damage; } }

        public Entity Entity { get { return Link.Entity; } }
        public AI AI { get { return Link.AI; } }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject == References.Level.Castle.gameObject)
            {
                Entity.DoDamage(collision.gameObject, damage, AI);

                if(suicideOnHit)
                    Entity.DoDamage(Entity, int.MaxValue, AI);
            }
        }
    }
}