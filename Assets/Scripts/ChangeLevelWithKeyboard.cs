using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;

public class ChangeLevelWithKeyboard : MonoBehaviour
{
    public string[] scenes;

    void Update()
    {
        Keyboard kboard = Keyboard.current;

        if (kboard.anyKey.wasPressedThisFrame)
        {
            foreach (KeyControl k in kboard.allKeys)
            {
                if (k.wasPressedThisFrame)
                {
                    switch (k.keyCode)
                    {
                        case Key.Digit1:
                            LoadLevel(scenes[0]);
                            break;
                        case Key.Digit2:
                            LoadLevel(scenes[1]);
                            break;
                    }
                }
            }
        }
    }

    private void LoadLevel(string sceneName)
    {
        var currentScene = SceneManager.GetActiveScene().name;
        if (currentScene != sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
