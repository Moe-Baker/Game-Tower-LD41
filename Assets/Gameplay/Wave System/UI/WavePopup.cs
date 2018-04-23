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
	public class WavePopup : MonoBehaviour
	{
        public Text label;

        WaveSystem WaveSystem { get { return Level.Current.WaveSystem; } }

        public void ShowStart()
        {
            label.text = "Wave " + WaveSystem.waveNumber + " Started";

            Show();
        }

        public void ShowEnd()
        {
            label.text = "Wave " + WaveSystem.waveNumber + " Ended";

            Show();
        }

        void Show()
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        void Close()
        {
            gameObject.SetActive(false);
        }
    }
}