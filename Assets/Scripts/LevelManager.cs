using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<EnemyInfo> enemyList = new List<EnemyInfo>();
    public List<CellInfo> cellList = new List<CellInfo>();
    public Transform enemyParent;
    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void InitField()
    {
        
    }
}
