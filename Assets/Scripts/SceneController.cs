using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Button m_Level1, m_Level2, m_quit;
    void Start()
    {
        m_Level1.onClick.AddListener(delegate { LoadLevel(m_Level1.name); });
        m_Level2.onClick.AddListener(delegate { LoadLevel(m_Level2.name); });
        //m_quit.onClick.AddListener(() => ButtonClicked(42));
        m_quit.onClick.AddListener(ApplicationQuit);
    }

    public void ApplicationQuit() {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }
}