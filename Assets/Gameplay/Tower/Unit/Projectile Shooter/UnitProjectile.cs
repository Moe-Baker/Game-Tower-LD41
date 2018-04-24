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
	public class UnitProjectile : MonoBehaviour
	{
        [SerializeField]
        protected float damage = 20f;
        public float Damage { get { return damage; } }

        new public Rigidbody rigidbody { get; protected set; }

        public Unit Unit { get; protected set; }
        public Tower Tower { get { return Unit.Tower; } }
        public ITowerDamager Damager { get { return Tower; } }

        public virtual void Init(Unit unit)
        {
            this.Unit = unit;
            rigidbody = GetComponent<Rigidbody>();

            MoeTools.GameObject.SetCollision(gameObject, Tower.gameObject, false);
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject != Level.Current.Castle.gameObject)
            {
                if (Entity.DoDamage(collision.gameObject, damage, Damager))
                {

                }
            }

            Destroy(gameObject);
        }
	}
}