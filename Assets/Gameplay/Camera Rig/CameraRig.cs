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
	public class CameraRig : MonoBehaviour
	{
		[SerializeField]
        protected Camera _camera;
        new public Camera camera { get { return _camera; } }

        [SerializeField]
        protected FloatValueRange heightRange = new FloatValueRange(20, 100f);
        public FloatValueRange HeightRange { get { return heightRange; } }

        [SerializeField]
        protected float heightMoveSpeed = 1;
        public float HeightMoveSpeed { get { return heightMoveSpeed; } }

        public virtual float Height
        {
            get
            {
                return camera.transform.localPosition.y;
            }
            set
            {
                camera.transform.SetLocalPositionY(value);
            }
        }

        [SerializeField]
        protected float panRange = 20f;
        public float PanRange { get { return panRange; } }

        [SerializeField]
        protected float panSpeed = 1f;
        public float PanSpeed { get { return panSpeed; } }

        protected virtual void Update()
        {
            UpdateHeight();

            UpdatePan();
        }

        protected virtual void UpdateHeight()
        {
            var input = Input.GetAxis("Mouse ScrollWheel");

            if (Mathf.Approximately(input, 0f)) return;
            else if(input > 0f)
            {
                MoveHeight(heightRange.Min, input);
            }
            else
            {
                MoveHeight(heightRange.Max, input);
            }
        }
        protected virtual void MoveHeight(float target, float input)
        {
            Height = Mathf.MoveTowards(Height, target, Mathf.Abs(input) * heightMoveSpeed * Time.deltaTime);
        }

        protected virtual void UpdatePan()
        {
            if(Input.GetMouseButton(1))
                MovePan(GetMousePanInput());
        }
        protected virtual Vector2 GetMousePanInput()
        {
            return new Vector2()
            {
                x = -Input.GetAxis("Mouse X"),
                y = -Input.GetAxis("Mouse Y")
            };
        }
        protected virtual void MovePan(Vector2 input)
        {
            var position = transform.localPosition;

            Vector3 offset = new Vector3(input.x, 0f, input.y);

            offset *= panSpeed * Time.deltaTime;

            position += offset;

            position.x = Mathf.Clamp(position.x, -panRange, panRange);
            position.z = Mathf.Clamp(position.z, -panRange, panRange);

            transform.localPosition = position;
        }
    }
}