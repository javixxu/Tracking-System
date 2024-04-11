using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    [SerializeField]
    Spawner punos;
    [SerializeField]
    Spawner pinchos;
    [SerializeField]
    Spawner powerUp_cohete;
    [SerializeField]
    Spawner powerUp_escudo;

    [SerializeField]
    Shield shield;

    int powerUpToSpawn = 10;
    int itemCont = 0;


    enum ExtraModes
    {
        normal, infinitePain, onlySpikes, onlyLasers
    }

    ExtraModes currentMode = ExtraModes.normal;


    float infiniteObstacleTimer;
    float infiniteDifficultLevel;
    float incrementDifficultySpeed = .2f;

    private void Awake()
    {
        int difficultyLevel = GameManager.GetInstance().GetLevel();

        if (difficultyLevel == 4)
        {
            difficultyLevel = 3;
            currentMode = ExtraModes.infinitePain;
        }
        if (difficultyLevel == 5)
        {
            difficultyLevel = 3;
            currentMode = ExtraModes.onlySpikes;
        }
        else if (difficultyLevel == 6)
        {
            difficultyLevel = 3;
            currentMode = ExtraModes.onlyLasers;
        }

        if (currentMode != ExtraModes.infinitePain)
            InvokeRepeating("SpawnSomething", 0.75f, GetObstacleDelayByDifficultyLevel(difficultyLevel));
        else
        {
            // Dificultad inicial
            infiniteDifficultLevel = 1.5f;
            infiniteObstacleTimer = GetObstacleDelayByDifficultyLevel(infiniteDifficultLevel);

            Invoke("SpawnSomething", .75f);
        }
        //InvokeRepeating("SpawnSomething", .1f, .1f);

        powerUp_cohete.SpawnThis();
    }

    float GetObstacleDelayByDifficultyLevel(float difficultyLevel)
    {
        return 1.38f - (difficultyLevel * 0.38f);
    }

    private void Update()
    {
        if (currentMode == ExtraModes.infinitePain)
            InfiniteMode();
    }

    void SpawnSomething()
    {

        bool spawnPowerUp = itemCont >= powerUpToSpawn;
        if (spawnPowerUp)
        {
            if (!shield.active)
                powerUp_escudo.SpawnThis();
            else
                powerUp_cohete.SpawnThis();
            itemCont = 0;
        }
        else
        {
            // Extra GameModes

            switch (currentMode)
            {
                case ExtraModes.onlySpikes:
                    if (Random.Range(0, 3 + 1) != 0)
                        pinchos.SpawnThis();
                    break;
                case ExtraModes.onlyLasers:
                    if (Random.Range(0, 3 + 1) != 0)
                    {
                        punos.SpawnThis();
                        punos.SpawnThis();
                    }
                    break;

                case ExtraModes.infinitePain:

                    break;

                case ExtraModes.normal:
                default:

                    SpawnNormalObstacles();
                    break;
            }

            itemCont++;
        }

    }

    void SpawnNormalObstacles()
    {
        // Normal GameMode
        int randomInt = Random.Range(0, 5 + 1);
        bool spawnPincho = randomInt == 0;
        if (spawnPincho)
        {
            pinchos.SpawnThis();
            if (Random.Range(0, 4 + 1) == 0)
                pinchos.SpawnThis();
        }
        else
        {
            punos.SpawnThis();
            if (Random.Range(0, 3 + 1) == 0)
                pinchos.SpawnThis();
        }
    }


    private void InfiniteMode()
    {
        if (infiniteObstacleTimer < 0)
        {
            if (infiniteDifficultLevel < 3)
                infiniteDifficultLevel += incrementDifficultySpeed * Time.deltaTime;
            else infiniteDifficultLevel = 3;

            float infiniteObstacleDelay = GetObstacleDelayByDifficultyLevel(infiniteDifficultLevel);
            Invoke("SpawnSomething", infiniteObstacleDelay);
            SpawnNormalObstacles();
            Debug.Log("infiniteObstacleDelay = " + infiniteObstacleDelay);

            infiniteObstacleTimer = infiniteObstacleDelay;
        }
        else
        {
            infiniteObstacleTimer -= Time.deltaTime;
        }

    }
}
