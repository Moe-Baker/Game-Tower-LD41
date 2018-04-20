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
	public class SceneAcessor : BaseUnityDispatcher
    {
        public static SceneAcessor Create()
        {
            var instance = new GameObject("Scene Accessor").AddComponent<SceneAcessor>();

            DontDestroyOnLoad(instance.gameObject);
            instance.Configure();

            return instance;
        }
	}
}