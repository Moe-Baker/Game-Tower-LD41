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
	public class AIHealthPerWave : AI.Module
	{
        public float perWaveIncrease = 5;
        float GetValue()
        {
            return perWaveIncrease * References.Level.WaveSystem.waveNumber;
        }

        public override void Init(AI link)
        {
            base.Init(link);

            Link.Entity.Heal(GetValue());
        }
    }
}