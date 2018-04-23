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
	public class WaveSystem : MonoBehaviour
	{
        public int waveNumber = 1;
        public int GetWaveValue(int perWaveValue)
        {
            return perWaveValue & waveNumber;
        }

        public int enemiesPerWave = 20;

        public float spawnDelay = 3f;
        public float waveDelay = 8f;

        public Transform[] spawnPoints;
        public GameObject[] prefabs;

        public AudioClip beginSFX;
        public AudioClip endSFX;

        public void Init()
        {
            waveNumber = 1;

            StartCoroutine(Procedure());
        }

        IEnumerator Procedure()
        {
            yield return new WaitForSeconds(waveDelay);

            while (true)
            {
                yield return WaveProcedure();

                waveNumber++;

                yield return new WaitForSeconds(waveDelay);
            }
        }

        IEnumerator WaveProcedure()
        {
            Level.Current.SFXManager.Play(beginSFX);
            Level.Current.Menu.HUD.WavePopup.ShowStart();

            var enemiesCount = enemiesPerWave + (waveNumber * 4);
            var deathCount = 0;

            Debug.Log(enemiesCount);

            for (int i = 0; i < enemiesCount; i++)
            {
                var entity = Spawn();

                entity.OnDied += (IDamager damager) =>
                {
                    deathCount++;
                };

                yield return new WaitForSeconds(GetSpawnDelay());
            }

            while(deathCount != enemiesCount)
            {
                yield return new WaitForEndOfFrame();
            }

            Level.Current.SFXManager.Play(endSFX);
            Level.Current.Menu.HUD.WavePopup.ShowEnd();
        }
        float GetSpawnDelay()
        {
            return spawnDelay + waveNumber / 5;
        }

        Entity Spawn()
        {
            var spawnPoint = GetSpawnPoint();

            var instance = Instantiate(GetSpawnPrefab(), spawnPoint.position, spawnPoint.rotation).GetComponent<Entity>();

            return instance;
        }
        GameObject GetSpawnPrefab()
        {
            var maxRange = 1;

            if (waveNumber > 5) maxRange = 2;
            if (waveNumber > 10) maxRange = 3;

            return prefabs[Random.Range(0, maxRange)];
        }
        Transform GetSpawnPoint()
        {
            var maxRange = 1;

            if (waveNumber > 5) maxRange = 2;
            if (waveNumber > 10) maxRange = 3;

            return spawnPoints[Random.Range(0, maxRange)];
        }
    }
}