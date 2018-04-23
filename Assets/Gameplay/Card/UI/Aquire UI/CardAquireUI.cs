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
	public class CardAquireUI : MonoBehaviour
	{
        public Animator animator;

        public CardUITemplate UITemplate;

        public AudioClip SFX;

		public void Init()
        {
            References.Level.CardsManager.Inventory.OnAdd += OnCardInventoryAdd;
        }

        private void OnCardInventoryAdd(Card obj)
        {
            Display(obj);
        }

        public void Display(Card card)
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);

            References.Level.SFXManager.Play(SFX);

            UITemplate.SetData(card);
        }

        void Close()
        {
            gameObject.SetActive(false);
        }
	}
}