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
	public class AINavMeshNavigator : AINavigator
	{
        public NavMeshAgent Agent { get; protected set; }

        public override float DistanceToTarget
        {
            get
            {
                if(Agent.hasPath)
                    return Agent.stoppingDistance;

                return Mathf.Infinity;
            }
        }

        public override Vector3 Velocity
        {
            get
            {
                return Agent.velocity;
            }
        }

        public override void Init(AI link)
        {
            base.Init(link);

            Agent = GetComponent<NavMeshAgent>();
        }

        public override void SetDestination(Vector3 value)
        {
            Agent.SetDestination(value);
        }
    }
}