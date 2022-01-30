using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    private TextMeshProUGUI textRenderer;
    private PlayerHealth playerHealth;
    private int lastFrameHealth;
    private List<GameObject> healthHearts = new List<GameObject>();

    public float heartSpacing = 100.0f;
    public GameObject fullHeartPrefab;
    public GameObject halfHeartPrefab;
    public GameObject emptyHeartPrefab;

    void Awake()
    {
        textRenderer = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        var playerObj = GameObject.FindWithTag("Player");
        if (!playerObj)
        {
            throw new ArgumentNullException("No Player in scene!");
        }

        playerHealth = playerObj.GetComponent<PlayerHealth>();
    }

    float CreateHeart(GameObject heartPrefab, float currentPosX)
    {
        var heart = Instantiate(heartPrefab, transform.position, Quaternion.identity) as GameObject;
        heart.transform.SetParent(transform);
        heart.transform.position = new Vector3(currentPosX, transform.position.y, transform.position.z);
        healthHearts.Add(heart);
        var newPosX = currentPosX + heartSpacing;
        return newPosX;
    }

    void Update()
    {
        if (lastFrameHealth != playerHealth.currentHealth)
        {
            foreach (var heart in healthHearts)
            {
                Destroy(heart);
            }

            var totalHearts = Math.Ceiling((float)playerHealth.startingHealth / 2.0f);
            var halfHearts = playerHealth.currentHealth % 2;
            var fullHearts = Math.Floor((float)playerHealth.currentHealth / 2.0f);
            var remainingHearts = totalHearts - halfHearts - fullHearts;
            var currentPosX = 50.0f;
            for (var i = 0; i < fullHearts; i++)
            {
                currentPosX = CreateHeart(fullHeartPrefab, currentPosX);
            }

            if (halfHearts != 0)
            {
                currentPosX = CreateHeart(halfHeartPrefab, currentPosX);
            }

            for (var i = 0; i < remainingHearts; i++)
            {
                currentPosX = CreateHeart(emptyHeartPrefab, currentPosX);
            }
        }

    }
}
