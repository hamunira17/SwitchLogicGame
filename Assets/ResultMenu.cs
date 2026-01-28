using UnityEngine;
using UnityEngine.SceneManagement; // これを忘れると「SceneManagerって何？」とPCに怒られます

public class ResulMenu : MonoBehaviour
{
    public void BackToStageSelect()
    {
        // 魔法の合言葉「Instance」を使って、副社長に命令！
        MySceneManager.Instance.GoToSelect();
    }
}