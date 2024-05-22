using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public Transform enemyParent;
    // Start is called before the first frame update
    void Start()
    {
        GenerateEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateEnemy()
    {
        if (enemyList.Count > 0)
        {
            GameObject enemy = Instantiate(enemyList[^1], enemyParent);

        }
    }
}
