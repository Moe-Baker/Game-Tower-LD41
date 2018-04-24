using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
    public class Entity : MonoBehaviour, IDamagable
    {
        [SerializeField]
        protected float health = 100f;
        public float Health
        {
            get
            {
                return health;
            }
        }
        public event Action<float> OnHealthChanged;
        protected void TriggerHealthChange()
        {
            if (OnHealthChanged != null)
                OnHealthChanged(health);
        }
        public virtual void Heal(float points)
        {
            health += points;

            TriggerHealthChange();
        }

        protected virtual void Awake()
        {
            if(health == 0)
            {
                Debug.LogWarning("Entity " + name + " Initiated With Zero Health, Will Be Killed");

                Died(null);
            }
        }

        void IDamagable.TakeDamage(float damage, IDamager damager)
        {
            TakeDamage(damage, damager);
        }

        public event Action<float, IDamager> OnTookDamage;
        protected virtual void TakeDamage(float damage, IDamager damager)
        {
            if (health == 0)
                return;

            if (health > damage)
            {
                health -= damage;

                if (OnTookDamage != null)
                    OnTookDamage(damage, damager);

                TriggerHealthChange();
            }
            else
            {
                health = 0;

                if (OnTookDamage != null)
                    OnTookDamage(damage, damager);

                TriggerHealthChange();

                Died(damager);
            }
        }

        public event Action<IDamager> OnDied;
        protected virtual void Died(IDamager damager)
        {
            DeathAction(damager);

            if (OnDied != null)
                OnDied(damager);
        }
        protected virtual void DeathAction(IDamager damager)
        {
            Destroy(gameObject);
        }

        public static bool DoDamage(GameObject target, float damage, IDamager damager)
        {
            var damagable = target.GetComponent<IDamagable>();

            if (damagable == null)
                return false;

            DoDamage(damagable, damage, damager);
            return true;
        }
        public static void DoDamage(IDamagable target, float damage, IDamager damager)
        {
            target.TakeDamage(damage, damager);
        }
    }
}