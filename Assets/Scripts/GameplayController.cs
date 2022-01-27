using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject[] ShadowBlockerVolumes;
    public GameObject[] ShadowBlockerMeshes;
    public int ScoreTarget;
    public float LevelTimer;

    public float CurrentScore;
    private bool m_canCountdown = true;

    private void Update()
    {
        ScoreTimeHandler();
    }

    private void ScoreTimeHandler()
    {
        if (CurrentScore >= ScoreTarget)
        {
            Debug.Log("You Win");
        }

        if (LevelTimer > 0.0f)
        {
            m_canCountdown = true;
            LevelTimer -= Time.deltaTime;
        }
        else
        {
            m_canCountdown = false;
            LevelTimer = 0.0f;
            Debug.Log("You Lose");
        }
    }
}