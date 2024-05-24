using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider enemyCountSld;
    public TextMeshProUGUI overkillTxt;
    public TextMeshProUGUI overkillAmountTxt;
    public TextMeshPro floatingTxtPrefab;


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
    public void FloatingTxtEnemy(int damage)
    {
        Vector3 loc = new Vector3(0.5f, 5, 0) + CombatManager.Instance.currentEnemy.GetComponentInParent<Transform>().position;
        TextMeshPro floatingTxt = Instantiate(floatingTxtPrefab, loc, Quaternion.identity);
        floatingTxt.text = damage.ToString();
        floatingTxt.transform.localScale = Vector3.zero;
        floatingTxt.transform.DOScale(1f, 1f);
        floatingTxt.transform.DOMoveY(loc.y + 1, 1f).OnComplete(() => {
            floatingTxt.transform.DOScale(0f, 0.5f);
            floatingTxt.gameObject.SetActive(false);
        });
    }
    public void FloatingTxtPlayer(int damage)
    {
        Vector3 loc = new Vector3(0.5f, 5, 0) + CombatManager.Instance.player.GetComponentInParent<Transform>().position;
        TextMeshPro floatingTxt = Instantiate(floatingTxtPrefab, loc, Quaternion.identity);
        floatingTxt.text = damage.ToString();
        floatingTxt.transform.localScale = Vector3.zero;
        floatingTxt.transform.DOScale(1f, 1f);
        floatingTxt.transform.DOMoveY(loc.y + 1, 1f).OnComplete(() => {
            floatingTxt.transform.DOScale(0f, 0.5f);
            floatingTxt.gameObject.SetActive(false);
        });
    }
}
