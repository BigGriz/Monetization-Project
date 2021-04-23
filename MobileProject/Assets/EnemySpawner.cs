using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;
    public List<GameObject> bossPrefabs;
    public GameObject portalPrefab;
    public int pointsToSpend;
    public Vector2 spacing;
    Vector2 offsetTiling = Vector2.zero;
    int rand;
    GameObject portal;
    // should really be called gameinfo
    GameSettings settings;

    public List<GameObject> spawnedEnemies;

    private void Awake()
    {
        // Seed RNG
        Random.InitState(Mathf.RoundToInt(Time.time));
    }

    private void Start()
    {
        settings = CallbackHandler.instance.settings;

        SetupEnemies();

        CallbackHandler.instance.spawnPortal += SpawnPortal;
        CallbackHandler.instance.nextLevel += NewLevel;
        // setup points to spend based on level and zone i guess
    }
    private void OnDestroy()
    {
        CallbackHandler.instance.spawnPortal -= SpawnPortal;
        CallbackHandler.instance.nextLevel -= NewLevel;
    }

    public void NewLevel()
    {
        offsetTiling = Vector2.zero;
        CleanUp();
        SetupEnemies();
    }

    public void CleanUp()
    {
        foreach(GameObject n in spawnedEnemies)
        {
            Destroy(n);
        }
        spawnedEnemies.Clear();
        Destroy(portal);
    }

    public void SpawnPortal()
    {
        GameObject port = Instantiate(portalPrefab, this.transform);
        port.transform.position += (Vector3)offsetTiling;
        offsetTiling += Random.Range(1.0f, 5.0f) * spacing;
        portal = port;
    }

    void SetupEnemies()
    {
        pointsToSpend = (((int)settings.area) + 1) * 14 + settings.stage * 6;

        while(pointsToSpend > 0)
        {
            rand = Random.Range(0, enemyPrefabs.Count);

            GameObject temp = Instantiate(enemyPrefabs[rand], this.transform);
            temp.transform.position += (Vector3)offsetTiling;
            offsetTiling += Random.Range(1.0f, 5.0f) * spacing;

            // apply buffs to enemies
            EnemyController enemy = temp.GetComponent<EnemyController>();
            pointsToSpend -= enemy.points;

            spawnedEnemies.Add(temp);
        }

        rand = Random.Range(0, bossPrefabs.Count);

        GameObject boss = Instantiate(bossPrefabs[rand], this.transform);
        boss.transform.position += (Vector3)offsetTiling;
        offsetTiling += Random.Range(1.0f, 5.0f) * spacing;

        spawnedEnemies.Add(boss);
    }

}
