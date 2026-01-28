using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMainPanel;
    [SerializeField] private GameObject settingsPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            var state = GameMaster.Instance.CurrentState;
            if (state == GameMaster.GameState.Playing) Pause();
            else if (state == GameMaster.GameState.Pause) Resume();
            else if (state == GameMaster.GameState.Settings) CloseSettings();
        }
    }

    // --- 外注先A：GameMaster (状態と時間の管理) ---
    public void Pause()
    {
        pauseMainPanel.SetActive(true);
        GameMaster.Instance.PauseGame(); // 時間を止めるのは社長の仕事
    }

    public void Resume()
    {
        pauseMainPanel.SetActive(false);
        settingsPanel.SetActive(false);
        GameMaster.Instance.ResumeGame(); // 時間を動かすのも社長の仕事
    }

    public void OpenSettings()
    {
        pauseMainPanel.SetActive(false);
        settingsPanel.SetActive(true);
        GameMaster.Instance.OpenSettingsState();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pauseMainPanel.SetActive(true);
        GameMaster.Instance.PauseGame();
    }

    // --- 外注先B：MySceneManager (シーン移動の管理) ---
    public void OnRestartButton() => MySceneManager.Instance.RestartCurrentStage();
    public void OnSelectButton() => MySceneManager.Instance.GoToSelect();
    public void OnTitleButton() => MySceneManager.Instance.GoToTitle();
}