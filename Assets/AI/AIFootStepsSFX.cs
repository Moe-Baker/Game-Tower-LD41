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
	public class AIFootStepsSFX : MonoBehaviour
	{
        public AudioSource aud;

        public SoundSet Set;

        public void Step()
        {
            aud.PlayOneShot(Set.RandomClip);
        }
    }
}