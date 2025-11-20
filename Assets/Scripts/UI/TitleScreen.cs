using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private SceneField scene;
    public void PlayGame()
    {
        GameManager.Instance.LoadScene(scene.SceneName, GameState.Gameplay);
    }

    public void OpenSettings()
    {
        UIManager.Instance.OpenMenu(UIManager.Instance.SettingsMenu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
