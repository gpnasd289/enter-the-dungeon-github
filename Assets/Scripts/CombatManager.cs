using QuangDM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public PlayerAnim playerAnim;
    public EnemyAnim enemyAnim;
    public HealthBar playerHealthBar;
    public Player player;
    public Enemy enemyPrefab; // Prefab for enemy
    public Transform enemySpawnPoint; // Point where new enemies will spawn
    public int enemiesDefeated = 0;
    public int enemiesPerLevel = 4; // Number of enemies per level
    public Enemy currentEnemy;
    public HealthBar enemyHealthBar;
    public int atkTime;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player.healthBar = playerHealthBar;
        player.Initialize();
        
        SpawnEnemy();
        Observer.Instance.AddObserver(EventName.DoAnim, DoAnim);
    }

    private void DoAnim(object data)
    {
        List<Cell> temp = (List<Cell>)data;
        atkTime = temp.Count;
        PlayerAttack(atkTime);
    }

    public void PlayerAttack(int attackCount)
    {
        playerAnim.PlayAttackAnimation(attackCount);
        //player.SetAttackParameters(attackCount, damage);
    }

    public void EnemyAttack(int attackCount)
    {
        enemyAnim.PlayAttackAnimation(attackCount);
        //currentEnemy.SetAttackParameters(attackCount, damage);
    }

    public void OnPlayerAnimationComplete()
    {
        player.DealDamageToEnemy(currentEnemy);
    }

    public void OnEnemyAnimationComplete()
    {
        Debug.Log("anim enemy complete");
        currentEnemy.DealDamageToPlayer(player);
    }

    public void OnEnemyDefeated()
    {
        Debug.Log("Enemy Defeated");
        enemiesDefeated++;
        Destroy(currentEnemy.gameObject);

        if (enemiesDefeated < enemiesPerLevel)
        {
            SpawnEnemy();
            //MovePlayerToNextEnemy();
        }
        else
        {
            // Handle level completion
            Debug.Log("Level Complete");
        }
    }

    public void OnPlayerDefeated()
    {
        Debug.Log("Player Defeated");
        // Handle player defeat logic
    }

    private void SpawnEnemy()
    {
        Debug.Log("spawn enemy");
        currentEnemy = Instantiate(enemyPrefab, enemySpawnPoint);
        enemyAnim = currentEnemy.enemyAnim;
        currentEnemy.healthBar = enemyHealthBar;
        currentEnemy.Initialize();
        //currentEnemy.Initialize(); // Assuming an Initialize method to set up the enemy
    }

    private void MovePlayerToNextEnemy()
    {
        // Implement logic to move the player towards the new enemy
        // This can be an animation or a simple position update
        player.transform.position = Vector3.MoveTowards(player.transform.position, currentEnemy.transform.position, 1f);
    }
}
