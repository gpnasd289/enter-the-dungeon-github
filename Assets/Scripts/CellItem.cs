using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CellItem : GOManager
{
    public int id;

    public bool isSpecial;

    public int multiply;

    public TextMeshPro multiplyTxt;

    public Color glowColor;

    public Color particleColor;

    public SpriteRenderer shadow;

    public SpriteRenderer glow;

    public SpriteRenderer icon;

    public ParticleSystem CollapseCellEffect;

    public ParticleSystem[] CellSelectedEffect;

    public ParticleSystem CellSelectedGlow;

    public ParticleSystem CellSelectedParticles;

    private Material Material;

    public Vector2Int Placement;
    public bool Highlighted { get; private set; }
    public void Highlight()
    {
        icon.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
    }
    public void ResetToDefaultLooks()
    {
        icon.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        icon.DOFade(255, 1f);
    }
    public void SetGrayedOut()
    {
        icon.DOFade(100, 1f);
        icon.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.5f);
    }
    public void FlyToPlayer(float flyDelay, Action onComplete)
    {
        DisableMaskInteraction();
        icon.transform.DOMove(new Vector3(1,11,0), 1f).SetDelay(flyDelay).OnComplete(() => {
            this.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }
    public void Disappear()
    {
        icon.transform.DOScale(Vector3.zero, 0.5f);
        icon.gameObject.SetActive(false);
    }
    public void PopItem(int multiple)
    {
        if (multiple >= 4 && multiple < 6)
        {
            multiply = 3;
        }
        else if (multiple >= 6 && multiple < 9)
        {
            multiply = 5;
        }
        else if (multiple >= 9)
        {
            multiply = 9;
        }
        multiplyTxt.sortingLayerID = SortingLayer.NameToID("Obj Visual");
        multiplyTxt.sortingOrder = 3;
        multiplyTxt.text = "x" + multiply;
        transform.localScale = Vector3.zero;
        transform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
    }

    public virtual void DisableMaskInteraction()
    {
        icon.maskInteraction = SpriteMaskInteraction.None;
    }

    protected override void Construct()
    {
    }

    protected override void Destruct()
    {
    }
}
