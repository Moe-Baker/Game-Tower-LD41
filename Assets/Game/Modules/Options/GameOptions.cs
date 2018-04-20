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
    [CreateAssetMenu(menuName = MenuPath + "Options")]
	public class GameOptions : Game.Module
	{
        [SerializeField]
        protected GameOptionsAudio audio;
        public GameOptionsAudio Audio { get { return audio; } }

        public override void Configure()
        {
            base.Configure();

            audio.Configure();
        }

        public override void Init()
        {
            base.Init();

            audio.Init();

            InitQuality();
        }

        #region Quality
        public int Quality
        {
            get
            {
                return QualitySettings.GetQualityLevel();
            }
            protected set
            {
                SetQuality(value);
            }
        }

        public virtual int MaxQuality { get; protected set; }

        public const string QualityPrefID = "Quality";

        protected virtual void InitQuality()
        {
            MaxQuality = QualitySettings.names.Length - 1;

            if(PlayerPrefs.HasKey(QualityPrefID))
                SetQuality(PlayerPrefs.GetInt(QualityPrefID));
        }

        public virtual void SetQuality(int newValue)
        {
            if(newValue < 0)
            {
                Debug.LogWarning("Game Options Quality cannot be set lower than zero");
                return;
            }
            else if(newValue > MaxQuality)
            {
                Debug.LogWarning("Game Options Quality cannot be set higher than the maximum value of " + MaxQuality);
                return;
            }

            QualitySettings.SetQualityLevel(newValue, true);
            PlayerPrefs.SetInt(QualityPrefID, newValue);
        }
        #endregion

        public class Module : MoeBasicScriptableModule
        {
            public const string MenuPath = Game.Module.MenuPath + "Options/";

            public virtual Game Game { get { return References.Game; } }
            public virtual GameOptions Options { get { return Game.Options; } }

            public virtual void Configure()
            {

            }
        }
    }

    public static class AudioUndumbifier
    {
        public const float Min = -80f;
        public const float Max = 20f;

        public static float LinearToDB(float linear)
        {
            float dB;

            if (linear != 0)
                dB = 20.0f * Mathf.Log10(linear);
            else
                dB = -144.0f;

            return dB;
        }

        public static float DBToLinear(float DB)
        {
            float linear = Mathf.Pow(10.0f, DB / 20.0f);

            return linear;
        }
    }
}