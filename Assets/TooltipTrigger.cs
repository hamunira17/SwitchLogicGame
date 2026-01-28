using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 子要素にある「パネル」をインスペクターで指定する
    [SerializeField] private GameObject myTooltipPanel;
    [SerializeField] private float delayTime = 0.3f;

    private Coroutine delayCoroutine;

    void Awake()
    {
        // 念のため、開始時は隠しておく
        if (myTooltipPanel != null) myTooltipPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (delayCoroutine != null) StopCoroutine(delayCoroutine);
        delayCoroutine = StartCoroutine(ShowAfterDelay());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (delayCoroutine != null) StopCoroutine(delayCoroutine);
        if (myTooltipPanel != null) myTooltipPanel.SetActive(false);
    }

    private IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delayTime);
        if (myTooltipPanel != null) myTooltipPanel.SetActive(true);
    }
}