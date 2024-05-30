using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider enemyCountSld;
    public TextMeshProUGUI overkillTxt;
    public TextMeshProUGUI overkillAmountTxt;
    public TextMeshPro floatingTxtPrefab;
    public GameObject winPanel;
    public GameObject losePanel;


    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyCountSld.maxValue = CombatManager.Instance.enemiesPerLevel;
        enemyCountSld.value = CombatManager.Instance.enemiesDefeated + 1;
        CombatManager.Instance.player.OnDamageDealth += FloatingTxtEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FloatingTxtEnemy(float damage)
    {
        Vector3 loc = new Vector3(0.5f, 5, 0) + CombatManager.Instance.currentEnemy.GetComponentInParent<Transform>().position;
        TextMeshPro floatingTxt = Instantiate(floatingTxtPrefab, loc, Quaternion.identity);
        floatingTxt.text = ((int)damage).ToString();
        floatingTxt.transform.localScale = Vector3.zero;
        floatingTxt.transform.DOScale(1f, 1f);
        floatingTxt.transform.DOMoveY(loc.y + 1, 1f).OnComplete(() => {
            floatingTxt.transform.DOScale(0f, 0.5f);
            floatingTxt.gameObject.SetActive(false);
        });
        overkillTxt.text = "OverKill: " + CombatManager.Instance.currentEnemy.OverKillAmount;
    }
    public void FloatingTxtPlayer(float damage)
    {
        Vector3 loc = new Vector3(0.5f, 5, 0) + CombatManager.Instance.player.GetComponentInParent<Transform>().position;
        TextMeshPro floatingTxt = Instantiate(floatingTxtPrefab, loc, Quaternion.identity);
        floatingTxt.text = ((int)damage).ToString();
        floatingTxt.transform.localScale = Vector3.zero;
        floatingTxt.transform.DOScale(1f, 1f);
        floatingTxt.transform.DOMoveY(loc.y + 1, 1f).OnComplete(() => {
            floatingTxt.transform.DOScale(0f, 0.5f);
            floatingTxt.gameObject.SetActive(false);
        });
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
