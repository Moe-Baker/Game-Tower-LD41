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

using UnityEngine.EventSystems;

using Moe.Tools;

namespace Game
{
    public class CardUITemplate : UITemplate<Card>, IPointerClickHandler
    {
        [SerializeField]
        protected Text label;
        public Text Label { get { return label; } }

        [SerializeField]
        protected Text useCost;
        public Text UseCost { get { return useCost; } }

        [SerializeField]
        protected Image icon;
        public Image Icon { get { return icon; } }

        [SerializeField]
        protected Text description;
        public Text Description { get { return description; } }

        public override void SetData(Card data)
        {
            base.SetData(data);

            label.text = data.name;
            useCost.text = data.UseCost.ToString() + Environment.NewLine + "Cost";
            icon.sprite = data.Icon;
            description.text = data.Description;
        }

        public event Action OnClick;
        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClick != null)
                OnClick();
        }
    }
}