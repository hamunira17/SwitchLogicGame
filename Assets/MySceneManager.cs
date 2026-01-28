using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    // どこからでも呼び出せるようにする「魔法の合言葉」
    public static MySceneManager Instance { get; private set; }

    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private string selectSceneName = "StageSelectScene";

    private void Awake()
    {
        // シングルトンの定型文（これ一回書けば、1年前の自分にドヤ顔できます）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PrepareTransition() => Time.timeScale = 1f;

    public void GoToTitle() { PrepareTransition(); SceneManager.LoadScene(titleSceneName); }
    public void GoToSelect() { PrepareTransition(); SceneManager.LoadScene(selectSceneName); }

    public void LoadStage(int stageNum)
    {
        PrepareTransition();
        SceneManager.LoadScene("Stage" + stageNum);
    }

    public void RestartCurrentStage()
    {
        PrepareTransition();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}