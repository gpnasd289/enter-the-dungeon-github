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
    public void Highlight(bool doHighlight)
    {
    }
    private void ResetToDefaultLooks()
    {
    }
    public void SetGrayedOut(bool value, float delay = 0f)
    {

    }
    public void FlyToPlayer(float flyDelay, Action onComplete = null)
    {
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
