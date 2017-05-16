using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public struct WaveInfo
{
    public int SpiderCount;
    public float SpiderSpeed;
    public float SpiderScale;
    public int SpiderHealth;
}

public class EnemyManager : MonoBehaviour
{

    public PlayerHealth PlayerHealth;
    public GameObject Enemy;
    public float SpawnTime = 3f;
    public Transform[] SpawnPoints;
    public List<WaveInfo> WaveNumber;
    public List<GameObject> CurrentSpiders;

    private int _waveNumberIndex;
    private int _spiderCount;

    // Use this for initialization
    void Start()
    {
        _waveNumberIndex = 0;
        WaveNumber = new List<WaveInfo>();

        var waveOne = new WaveInfo { SpiderCount = 24, SpiderSpeed = 1.5f, SpiderScale = 0.15f, SpiderHealth = 100 };
        WaveNumber.Add(waveOne);

        var waveTwo = new WaveInfo { SpiderCount = 16, SpiderSpeed = 1.0f, SpiderScale = 0.20f, SpiderHealth = 200 };
        WaveNumber.Add(waveTwo);

        var waveThree = new WaveInfo { SpiderCount = 8, SpiderSpeed = 0.5f, SpiderScale = 0.50f, SpiderHealth = 400 };
        WaveNumber.Add(waveThree);

        var waveFour = new WaveInfo { SpiderCount = 1, SpiderSpeed = 0.2f, SpiderScale = 1.15f, SpiderHealth = 800 };
        WaveNumber.Add(waveFour);

        _spiderCount = WaveNumber[_waveNumberIndex].SpiderCount;
        InvokeRepeating("Spawn", SpawnTime, SpawnTime);
    }

    private void Spawn()
    {
        if (PlayerHealth.CurrentHealth <= 0f)
        {
            return;
        }

        if (_spiderCount > 0)
        {
            int spawnPointIndex = UnityEngine.Random.Range(0, SpawnPoints.Length);

            var enemy = Instantiate(Enemy, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
            CurrentSpiders.Add(enemy);

            enemy.GetComponent<NavMeshAgent>().speed = WaveNumber[_waveNumberIndex].SpiderSpeed;
            enemy.GetComponent<EnemyHealth>().CurrentHealth = WaveNumber[_waveNumberIndex].SpiderHealth;
            enemy.transform.localScale = new Vector3(WaveNumber[_waveNumberIndex].SpiderScale, WaveNumber[_waveNumberIndex].SpiderScale, WaveNumber[_waveNumberIndex].SpiderScale);

            _spiderCount -= 1;
        }
        else if (_waveNumberIndex < WaveNumber.Count - 1)
        {
            if (CurrentSpiders.Count == 0)
            {
                _waveNumberIndex++;
                _spiderCount = WaveNumber[_waveNumberIndex].SpiderCount;
            }
        }
        else
        {
            CancelInvoke("Spawn");
        }
    }
}
