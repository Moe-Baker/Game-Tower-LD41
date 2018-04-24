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
	public class CardsUIUseButton : MonoBehaviour
	{
        public Text label;

        public CardsUI CardsUI;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            if (CardsUI.QueryPredicate == null)
            {
                label.text = "Cancel Using";

                CardsUI.Query(UseQuery);
                CardsUI.OnClick += OnCardClicked;
            }
            else
            {
                label.text = "Use A Card";

                CardsUI.ClearQuery();
                CardsUI.OnClick -= OnCardClicked;
            }
        }

        private void OnCardClicked(CardUITemplate obj)
        {
            if(Level.Current.CardsManager.Inventory.Use(obj.Data))
            {
                
            }
        }

        bool UseQuery(Card card)
        {
            return card is IUsableCard;
        }
    }
}