using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private SwitchType type;
    [SerializeField] private KeyCode debugKey; // インスペクターでG, H, J, Kを設定

    // プレイヤーの接触などで呼ぶためのメソッド
    public void Activate()
    {
        if (SwitchManager.Instance != null)
        {
            SwitchManager.Instance.ToggleSwitch(type);
        }
    }

    private void Update()
    {
        // 指定されたキーが押されたら発火（テスト・デバッグ用）
        if (Input.GetKeyDown(debugKey))
        {
            Activate();
        }
    }

    // 視覚的な確認用（ギミック実装までの繋ぎ）
    private void OnEnable() => SwitchManager.OnWorldStateChanged += HandleVisualChange;
    private void OnDisable() => SwitchManager.OnWorldStateChanged -= HandleVisualChange;

    private void HandleVisualChange(SwitchType effectiveState)
    {
        // 自分が今「有効な状態」に含まれているかチェック
        bool isActive = (effectiveState & type) != 0;
        // ここでスイッチの光の色を変えるなどの演出を入れると美しい
    }
}