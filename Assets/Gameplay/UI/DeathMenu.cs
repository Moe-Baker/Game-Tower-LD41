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
	public class DeathMenu : Menu
	{
        public Text waveLabel;

        public override void Show()
        {
            base.Show();

            waveLabel.text = "You Survived For " + References.Level.WaveSystem.waveNumber + " Waves";
        }
    }
}