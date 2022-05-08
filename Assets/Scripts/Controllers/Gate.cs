using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

[Serializable]
public struct GetePanel
{
    public Transform transform;
    public Vector3 unlockRotation;
}

public class Gate : MonoBehaviour, IGate
{
    [SerializeField]
    private bool _needKeyToOpen = true;

    public bool NeedKeyToOepn
    {
        get
        {
            return _needKeyToOpen;
        }
    }

    public bool getUnlocked { get; private set; } = false;

    [SerializeField]
    private List<GetePanel> getPanels;

    [SerializeField]
    private float _unlockTime = 0.5f;

    public bool CanOpen(Player player)
    {
        return !getUnlocked && (!_needKeyToOpen || player.keys > 0);
    }

    public bool Open(Player player)
    {
        if (!CanOpen(player)) return false;

        LogService.Log("Door Unlocked");
        getUnlocked = true;

        // Unlock Animation
        foreach (GetePanel getePanel in getPanels)
        {
            getePanel.transform.DOLocalRotate(getePanel.unlockRotation, _unlockTime);
        }

        return _needKeyToOpen;
    }

    private void OnDisable()
    {
        foreach (GetePanel getePanel in getPanels)
        {
            getePanel.transform.DOKill();
        }
    }
}