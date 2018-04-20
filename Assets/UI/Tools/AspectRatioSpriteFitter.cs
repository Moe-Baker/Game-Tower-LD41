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
    [ExecuteInEditMode]
    [RequireComponent(typeof(Image), typeof(AspectRatioFitter))]
	public class AspectRatioSpriteFitter : MonoBehaviour
	{
        [SerializeField]
        protected Image image;
        public Image Image { get { return image; } }
        public virtual Sprite Sprite { get { return image.sprite; } }

        [SerializeField]
        protected AspectRatioFitter aspectFitter;
        public AspectRatioFitter AspectFitter { get { return aspectFitter; } }
        public virtual float AspectRatio
        {
            get
            {
                return aspectFitter.aspectRatio;
            }
            set
            {
                aspectFitter.aspectRatio = value;
            }
        }

        protected virtual void GetComponents()
        {
            if (image == null) image = GetComponent<Image>();
            if (aspectFitter == null) aspectFitter = GetComponent<AspectRatioFitter>();
        }

        protected virtual void OnValidate()
        {
            GetComponents();
        }

        protected virtual void Reset()
        {
            GetComponents();
        }


        protected virtual void Awake()
        {
            GetComponents();

            if(Application.isPlaying)
                Image.RegisterDirtyVerticesCallback(UpdateAspect);

            UpdateAspect();
        }


        protected virtual void Update()
        {
            if (!Application.isPlaying)
                UpdateAspect();
        }


        protected virtual void UpdateAspect()
        {
            AspectRatio = CaluculateRatio(Sprite);
        }

        public static float CaluculateRatio(Sprite sprite)
        {
            if (sprite == null)
                return 1f;

            return CaluculateRatio(sprite.bounds.size.x, sprite.bounds.size.y);
        }
        public static float CaluculateRatio(float width, float height)
        {
            return width / height;
        }
    }
}