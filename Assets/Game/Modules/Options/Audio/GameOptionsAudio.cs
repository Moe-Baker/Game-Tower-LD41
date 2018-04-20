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

using UnityEngine.Audio;

namespace Game
{
    [CreateAssetMenu(menuName = MenuPath + "Audio")]
	public class GameOptionsAudio : GameOptions.Module
	{
        [SerializeField]
        protected AudioMixerGroup[] groups;
        public AudioMixerGroup[] Groups { get { return groups; } }

        public override void Configure()
        {
            base.Configure();

            //Needs to be initilized on the next frame because UNITY
            Game.SceneAcessor.YieldForFrame(ConfigureGroups);
        }

        protected virtual void ConfigureGroups()
        {
            for (int i = 0; i < groups.Length; i++)
                SetVolume(groups[i], GetVolume(groups[i]));
        }

        public virtual float GetVolume(AudioMixerGroup group)
        {
            var ID = GetGroupVolumeID(group);

            if (PlayerPrefs.HasKey(ID))
                return PlayerPrefs.GetFloat(ID);
            else
                return GetGroupVolume(group);
        }
        protected virtual float GetGroupVolume(AudioMixerGroup group)
        {
            float value;

            var ID = GetGroupVolumeID(group);

            if (!group.audioMixer.GetFloat(ID, out value))
                throw new Exception("Couldn't get volume for Mixer Group " + group.name + " Make sure a volume parameter with the name " + ID + " Exists in your Audio Mixer");

            return AudioUndumbifier.DBToLinear(value);
        }

        public virtual void SetVolume(AudioMixerGroup group, float newValue)
        {
            newValue = Mathf.Clamp01(newValue);

            SetGroupVolume(group, newValue);

            PlayerPrefs.SetFloat(GetGroupVolumeID(group), newValue);
        }
        protected virtual void SetGroupVolume(AudioMixerGroup group, float newValue)
        {
            var ID = GetGroupVolumeID(group);

            if (!group.audioMixer.SetFloat(ID, AudioUndumbifier.LinearToDB(newValue)))
                throw new Exception("Couldn't set volume for Mixer Group " + group.name + " Make sure a volume parameter with the name " + ID + " Exists in your Audio Mixer");
        }

        public static string GetGroupVolumeID(AudioMixerGroup group)
        {
            return group.name + " " + "Volume";
        }
    }
}