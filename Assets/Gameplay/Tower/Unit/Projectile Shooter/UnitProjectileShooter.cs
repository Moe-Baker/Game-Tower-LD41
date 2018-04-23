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
	public class UnitProjectileShooter : Unit.Module
	{
		[SerializeField]
        protected GameObject projectile;
        public GameObject Projectile { get { return projectile; } }

        [SerializeField]
        protected float force = 40f;
        public float Force { get { return force; } }

        [SerializeField]
        protected ShootSetupData shootSetup;
        public ShootSetupData ShootSetup { get { return shootSetup; } }
        [Serializable]
        public class ShootSetupData
        {
            [SerializeField]
            protected Transform pivot;
            public Transform Pivot { get { return pivot; } }

            [SerializeField]
            protected Transform point;
            public Transform Point { get { return point; } }

            public virtual void AimPivot(Vector3 position)
            {
                var direction = (position - pivot.position).normalized;

                direction.y = 0f;

                pivot.rotation = Quaternion.LookRotation(direction);
            }

            public virtual void AimPoint(Vector3 position)
            {
                var direction = (position - point.position).normalized;

                point.rotation = Quaternion.LookRotation(direction);
            }
        }

        void Update()
        {
            if (!Link)
                return;

            var enemy = Tower.FindEnemy(20);

            if(enemy)
            {
                AimAt(enemy.transform.position, enemy.AI.Navigator.Velocity);

                if (!shooting)
                    StartCoroutine(ShootProcedure());
            }
        }

        bool shooting = false;
        IEnumerator ShootProcedure()
        {
            shooting = true;
            Shoot();

            yield return new WaitForSeconds(1);

            shooting = false;
        }

        public virtual void AimAt(Vector3 position, Vector3 velocity)
        {
            var travelTime = Vector3.Distance(position, shootSetup.Point.transform.position) / force;

            var positonEstimate = position + velocity * travelTime;

            AimAt(positonEstimate);
        }
        public virtual void AimAt(Vector3 position)
        {
            position += Vector3.up * 1f;

            shootSetup.AimPivot(position);
            shootSetup.AimPoint(position);
        }

        public virtual void Shoot()
        {
            var instance = Instantiate(projectile).GetComponent<UnitProjectile>();

            instance.transform.position = shootSetup.Point.position;
            instance.transform.rotation = shootSetup.Point.rotation;

            instance.Init(Link);
            instance.rigidbody.AddForce(instance.transform.forward * force, ForceMode.VelocityChange);
        }
    }
}