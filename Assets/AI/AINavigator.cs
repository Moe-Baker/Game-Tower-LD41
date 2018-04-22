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
    [RequireComponent(typeof(NavMeshAgent))]
	public abstract class AINavigator : AI.Module
	{
        public abstract float DistanceToTarget { get; }

        public abstract Vector3 Velocity { get; }

        public abstract void SetDestination(Vector3 value);
    }
}