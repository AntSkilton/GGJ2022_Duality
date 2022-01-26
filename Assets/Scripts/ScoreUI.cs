using System;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI textRenderer;
    private GameplayController gameController;

    void Awake()
    {
        textRenderer = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        var controllerObj = GameObject.FindWithTag("GameController");
        if (!controllerObj)
        {
            throw new ArgumentNullException("No GameplayController in scene!");
        }

        gameController = controllerObj.GetComponent<GameplayController>();
    }

    // Update is called once per frame
    void Update()
    {
        var score = gameController.CurrentScore;
        var target = gameController.ScoreTarget;
        textRenderer.text = $"Score: {score} / {target}";
    }
}
