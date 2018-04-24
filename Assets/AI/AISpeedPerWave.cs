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
	public class AISpeedPerWave : AI.Module
	{
        public float perWaveIncrease = 0.1f;
        float GetValue()
        {
            return perWaveIncrease * References.Level.WaveSystem.waveNumber;
        }

        public override void Init(AI link)
        {
            base.Init(link);

            Link.GetComponent<NavMeshAgent>().speed += GetValue();
        }
    }
}