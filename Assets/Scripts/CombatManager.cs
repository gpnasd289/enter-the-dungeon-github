using DG.Tweening;
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
    public CellField Field;
    public int atkTime;
    public int multipleDmg = 1;
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

    public void OnPlayerAnimationComplete(int atkTime)
    {
        player.DealDamageToEnemy(currentEnemy, atkTime);
    }

    public void OnEnemyAnimationComplete()
    {
        Debug.Log("anim enemy complete");
        currentEnemy.DealDamageToPlayer(player);
        currentEnemy.OnTurnEnd += Field.ResetAllHighlightAndCol;
    }

    public void OnEnemyDefeated()
    {
        Debug.Log("Enemy Defeated");
        enemiesDefeated++;

        
        if (enemiesDefeated < enemiesPerLevel)
        {
            currentEnemy.OnTurnEnd -= Field.ResetAllHighlightAndCol;
            currentEnemy.transform.DOScale(0f, 1f).OnComplete(() => {
                Destroy(currentEnemy.gameObject);
                SpawnEnemy();
                UIManager.Instance.enemyCountSld.value++;
                Field.ResetAllHighlightAndCol();
            });
            //MovePlayerToNextEnemy();
        }
        else
        {
            // Handle level completion
            UIManager.Instance.winPanel.SetActive(true);
            Debug.Log("Level Complete");
        }
    }

    public void OnPlayerDefeated()
    {
        UIManager.Instance.losePanel.SetActive(true);
        Debug.Log("Player Defeated");
        // Handle player defeat logic
    }

    private void SpawnEnemy()
    {
        Debug.Log("spawn enemy");
        UIManager.Instance.overkillTxt.text = "Overkill";
        currentEnemy = Instantiate(enemyPrefab, enemySpawnPoint);
        currentEnemy.OnDamageDealth += UIManager.Instance.FloatingTxtPlayer;
        enemyAnim = currentEnemy.enemyAnim;
        currentEnemy.healthBar = enemyHealthBar;
        currentEnemy.Initialize();
        currentEnemy.transform.localScale = Vector3.zero;
        currentEnemy.transform.DOScale(1f, 1f).OnComplete(() => currentEnemy.OnTurnEnd += Field.ResetAllHighlightAndCol);
        //currentEnemy.Initialize(); // Assuming an Initialize method to set up the enemy
    }

    private void MovePlayerToNextEnemy()
    {
        // Implement logic to move the player towards the new enemy
        // This can be an animation or a simple position update
        player.transform.position = Vector3.MoveTowards(player.transform.position, currentEnemy.transform.position, 1f);
    }
}
