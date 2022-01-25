using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject[] ShadowBlockerVolumes;
    public GameObject[] ShadowBlockerMeshes;
    public int ScoreTarget;
    public float LevelTimer;

    private int m_currentScore;

    private void Update()
    {
        ScoreTimeHandler();
    }

    private void ScoreTimeHandler()
    {
        if (m_currentScore >= ScoreTarget)
        {
            Debug.Log("You Win");
        }

        LevelTimer -= Time.deltaTime;

        if (LevelTimer <= 0.0f)
        {
            Debug.Log("You Lose");
        }
    }
}