using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject[] ShadowBlockerVolumes;
    public GameObject[] ShadowBlockerMeshes;
    public int ScoreTarget;
    
    [Min(30)]
    public float LevelTime;
    [HideInInspector]
    public float CurrentTime;
    
    [Range(1,6)]
    [Tooltip("The higher the number, the more difficult the level. Not adjustable at runtime due to timings.")]
    public int GameDifficulty = 6;

    [HideInInspector] public float CurrentScore { get; set; }
    private bool m_canCountdown = true;
    private int m_secsPerInterval;
    private bool m_shouldToggleBlocker;
    private int m_intervalCounter = 1;

    private void Awake()
    {
        CalculateRandomIntervals(); // This sets up the total number times the game makes a decision to play with a light blockers
        CurrentTime = LevelTime; // So we can keep a hard ref to the level's time
    }

    private void Update()
    {
        ScoreTimeHandler();
        ToggleRandomBlockerHandler();
    }

    private void CalculateRandomIntervals() {
        var totalBlocks = ShadowBlockerMeshes.Length;
        var minTimePerBlocker = LevelTime / totalBlocks;
        m_secsPerInterval = Mathf.RoundToInt(minTimePerBlocker - GameDifficulty);
    }

    private void ToggleRandomBlockerHandler()
    {
        if (CurrentTime <= LevelTime - (m_secsPerInterval * m_intervalCounter)) {
            m_shouldToggleBlocker = true;
        }
        
        if (m_shouldToggleBlocker) { // This toggles the shadow blocker volumes at random
            var randomBlocker = Mathf.RoundToInt(Random.Range(0, ShadowBlockerMeshes.Length));
            ShadowBlockerMeshes[randomBlocker].SetActive(!ShadowBlockerMeshes[randomBlocker].activeSelf);
            ShadowBlockerVolumes[randomBlocker].SetActive(!ShadowBlockerVolumes[randomBlocker].activeSelf);
            Debug.Log($"Blocker {randomBlocker} toggled.");
            
            // Cleanup
            m_shouldToggleBlocker = false;
            m_intervalCounter++;
        }
    }

    private void ScoreTimeHandler()
    {
        if (CurrentScore >= ScoreTarget)
        {
            Debug.Log("You Win");
        }

        if (CurrentTime > 0.0f)
        {
            m_canCountdown = true;
            CurrentTime -= Time.deltaTime;
        }
        else
        {
            m_canCountdown = false;
            CurrentTime = 0.0f;
            Debug.Log("You Lose");
        }
    }
}