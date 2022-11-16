using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float maxSpeed;
    private float speed;
    private float wanderDelay;
    [SerializeField] private float range;

    public void SetupEnemy(bool canFly)
    {
        EnemyManager enemyManager = EnemyManager.instance;
        if (canFly)
        {
            maxSpeed = enemyManager.flyingEnemyMaxSpeed;
            speed = enemyManager.flyingEnemySpeed;
        } else
        {
            maxSpeed = enemyManager.groundEnemyMaxSpeed;
            speed = enemyManager.groundEnemySpeed;
        }
        wanderDelay = enemyManager.wanderDelay;
        EnemyManager.instance.AddEnemy(gameObject);
    }

    public float GetMaxSpeed()
    {
        return maxSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetWanderDelay()
    {
        return wanderDelay;
    }

    public float GetRange()
    {
        return range;
    }
}
