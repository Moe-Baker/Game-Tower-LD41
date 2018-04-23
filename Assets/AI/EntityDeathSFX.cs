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
	public class EntityDeathSFX : MonoBehaviour
	{
        public Entity entity;

        public AudioSource source;
        public AudioClip SFX;

        private void Start()
        {
            entity.OnDied += OnDeath;
        }

        private void OnDeath(IDamager obj)
        {
            transform.SetParent(null);

            Invoke("Destroy", 4);

            source.enabled = true;
            this.enabled = true;

            if(obj is ITowerDamager)
            {
                source.PlayOneShot(SFX);
            }
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}