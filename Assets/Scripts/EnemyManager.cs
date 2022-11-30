using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private List<GameObject> enemies = new List<GameObject>();

    public GameObject player;

    public float flyingEnemySpeed;
    public float flyingEnemyMaxSpeed;
    public float groundEnemySpeed;
    public float groundEnemyMaxSpeed;

    public float wanderDelay;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }



    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public float GetDistanceTo(Vector3 from, Vector3 to)
    {
        return (to - from).magnitude;
    }

    public GameObject GetClosestEnemy(Vector3 position, float range)
    {
        GameObject closestEnemy;
        float closestDistance;

        if (enemies.Count <= 0)
        {
            return null;
        }
        
        closestEnemy = enemies[0];
        closestDistance = GetDistanceTo(position, closestEnemy.transform.position);

        foreach (GameObject enemy in enemies)
        {
            float distance = GetDistanceTo(position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestDistance > range)
        {
            return null;
        }
        return closestEnemy;
    }
}