using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public GameObject WinLabel;
    public GameObject LoseLabel;
    private GameplayController gameController;

    void Start()
    {
        var controllerObj = GameObject.FindWithTag("GameController");
        if (!controllerObj)
        {
            throw new ArgumentNullException("No GameplayController in scene!");
        }

        gameController = controllerObj.GetComponent<GameplayController>();
    }
    
    void Update()
    {
        if (gameController.CurrentScore >= gameController.ScoreTarget) {
            WinLabel.SetActive(true);
            StartCoroutine(FinishScreen());
        }
        if (gameController.CurrentTime <= 0f) {
            LoseLabel.SetActive(true);
            gameController.CurrentTime = 0.0f;
            StartCoroutine(FinishScreen());
        }
    }

    private IEnumerator FinishScreen() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Frontend");
    }
}