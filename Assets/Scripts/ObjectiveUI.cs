using System;
using UnityEngine;
using TMPro;

public class ObjectiveUI : MonoBehaviour
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

    void Update()
    {
        textRenderer.text = gameController.ShouldSeekLight ? "Stay in the light!" : "Stay in the shadows!";
    }
}