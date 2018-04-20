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
    [RequireComponent(typeof(Slider))]
	public class VolumeSlider : MonoBehaviour
	{
        [SerializeField]
        protected AudioMixerGroup group;
        public AudioMixerGroup Group { get { return group; } }

        public Slider Slider { get; protected set; }

        public GameOptionsAudio Audio { get { return References.Game.Options.Audio; } }

        protected virtual void Start()
        {
            Slider = GetComponent<Slider>();

            Slider.minValue = 0f;
            Slider.maxValue = 1f;
            Slider.value = Audio.GetVolume(group);

            Slider.onValueChanged.AddListener(OnChanged);
        }

        protected virtual void OnChanged(float newVolume)
        {
            Audio.SetVolume(group, newVolume);
        }
    }
}