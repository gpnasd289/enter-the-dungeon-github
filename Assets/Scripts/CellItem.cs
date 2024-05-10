using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellItem : GOManager
{
    public int id;

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

    public bool Highlighted { get; private set; }
    public void Highlight()
    {
        icon.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
    }
    public void ResetToDefaultLooks()
    {
        icon.transform.DOScale(new Vector3(1, 1, 1), 1f);
        icon.DOFade(255, 1f);
    }
    public void SetGrayedOut()
    {
        icon.DOFade(200, 1f);
    }
    public void FlyToPlayer(float flyDelay, Action onComplete = null)
    {
    }
    public void Disappear()
    {
        icon.transform.DOScale(Vector3.zero, 1f);
        icon.gameObject.SetActive(false);
    }
    public void PopItem()
    {
    }

    public virtual void DisableMaskInteraction()
    {
    }

    protected override void Construct()
    {
    }

    protected override void Destruct()
    {
    }
}
