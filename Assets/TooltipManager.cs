using UnityEngine;
using TMPro;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance;

    // プレハブではなく、ヒエラルキーに置いたパネルそのものを入れる
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TextMeshProUGUI tooltipText;

    void Awake()
    {
        Instance = this;
        tooltipPanel.SetActive(false); // 最初は隠しておく
    }

    public void Show(string message, Vector3 position)
    {
        tooltipPanel.SetActive(true);
        tooltipPanel.transform.position = position;
        tooltipText.text = message;
    }

    public void Hide()
    {
        if (tooltipPanel != null)
            tooltipPanel.SetActive(false);
    }
}