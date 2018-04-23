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
	public class AIIdleSFX : MonoBehaviour
	{
        public AudioSource aud;

        public AudioClip clip;

        float minDelay = 8f;
        float maxDelay = 20f;

        IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

                aud.PlayOneShot(clip);
            }
        }
    }
}