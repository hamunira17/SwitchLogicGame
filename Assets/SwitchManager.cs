using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum SwitchType
{
    None = 0,
    Gravity = 1 << 0,    // G
    TimeStop = 1 << 1,   // H
    Existence = 1 << 2,  // J
    Inversion = 1 << 3   // K
}

public class SwitchManager : MonoBehaviour
{
    public static SwitchManager Instance { get; private set; }

    [Header("State Settings")]
    [SerializeField] private List<SwitchType> activeSwitches = new List<SwitchType>();

    // ギミックへ通知するイベント（引数は計算済みの最終状態）
    public static event Action<SwitchType> OnWorldStateChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ToggleSwitch(SwitchType type)
    {
        if (activeSwitches.Contains(type))
        {
            activeSwitches.Remove(type);
        }
        else
        {
            activeSwitches.Add(type);
            if (activeSwitches.Count > 2) activeSwitches.RemoveAt(0);
        }

        UpdateWorldState();
    }

    private void UpdateWorldState()
    {
        SwitchType rawState = SwitchType.None;
        foreach (var s in activeSwitches) rawState |= s;

        SwitchType effectiveState = rawState;

        // 位相反転のロジック（XORゲートの美学）
        if ((rawState & SwitchType.Inversion) != 0)
        {
            // 反転対象のビットマスク（NoneとInversion以外）
            SwitchType targetMask = SwitchType.Gravity | SwitchType.TimeStop | SwitchType.Existence;
            // 指定されたビットだけを反転させ、Inversion自体は維持
            effectiveState = (rawState ^ targetMask);
        }

        OnWorldStateChanged?.Invoke(effectiveState);
        Debug.Log($"<color=cyan>[Manager]</color> Active: {string.Join(", ", activeSwitches)} | Final State: {effectiveState}");
    }
}