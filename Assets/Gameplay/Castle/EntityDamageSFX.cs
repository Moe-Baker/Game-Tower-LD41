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
	public class EntityDamageSFX : MonoBehaviour
	{
        public AudioSource aud;

        public AudioClip clip;

        void Start()
        {
            GetComponent<Entity>().OnTookDamage += OnDamaged;
        }

        private void OnDamaged(float arg1, IDamager arg2)
        {
            aud.PlayOneShot(clip);
        }
    }
}