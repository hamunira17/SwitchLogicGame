using UnityEngine;
using UnityEngine.Events;

public class Gimmick : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private SwitchType targetSwitch; // どのスイッチに反応するか

    [Header("Events")]
    [SerializeField] private UnityEvent onActivate;   // ONになった時の処理
    [SerializeField] private UnityEvent onDeactivate; // OFFになった時の処理

    private bool isActive = false;

    private void OnEnable()
    {
        // マネージャーの「天の声」を購読開始
        SwitchManager.OnWorldStateChanged += UpdateGimmickState;
    }

    private void OnDisable()
    {
        // 購読解除（メモリリーク防止）
        SwitchManager.OnWorldStateChanged -= UpdateGimmickState;
    }

    private void UpdateGimmickState(SwitchType effectiveState)
    {
        // ビット演算で「自分に関係あるスイッチ」がONか判定
        bool newState = (effectiveState & targetSwitch) != 0;

        // 状態に変化があった時だけ実行（無駄な処理を省く）
        if (newState != isActive)
        {
            isActive = newState;

            if (isActive)
                onActivate.Invoke();
            else
                onDeactivate.Invoke();
        }
    }
}