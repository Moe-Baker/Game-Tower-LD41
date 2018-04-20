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
    [CreateAssetMenu(menuName = Game.MenuPath + "Level")]
	public partial class GameLevel : ScriptableObject
	{
		[SerializeField]
        protected GameScene scene;
        public GameScene Scene { get { return scene; } }
    }
}