using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public GameObject[] ShadowBlockerVolumes;
    public GameObject[] LightBlockerVolumes;
    public GameObject[] LightMaskMeshes;
    public int ScoreTarget;
    
    [Range(60, 120)]
    public float LevelTime;
    [HideInInspector]
    public float CurrentTime;
    
    [Range(1,6)]
    [Tooltip("The higher the number, the more difficult the level. Not adjustable at runtime due to timings.")]
    public int GameDifficulty = 6;

    [HideInInspector] public bool ShouldSeekLight = true;

    public float CurrentScore { get; set; }
    public bool IsInShadow { get; set; }
    public bool IsInScoringZone { get; set; }

    // Timers
    private bool m_shouldToggleBlocker;
    private int m_secsPerBlockerInterval;
    private int m_blockerIntervalCounter = 1;
    
    private bool m_shouldToggleLightSwitch;
    private float m_secsPerLightswitchInterval;
    private int m_lightswitchIntervalCounter = 1;

    private void Awake()
    {
        CalculateLightblockerIntervals(); // This sets up the total number times the game makes a decision to play with a light blockers
        CurrentTime = LevelTime; // So we can keep a hard ref to the level's time
        m_secsPerLightswitchInterval = 10f - GameDifficulty;
    }

    private void Update()
    {
        CurrentTime -= Time.deltaTime;
        ToggleRandomBlockerHandler();
        LightswitchHandler();
    }

    private void LightswitchHandler()
    {
        /*if (CurrentTime < LevelTime / 4) { // Last quarter of the level the switching speeds up x2 for more difficulty
            m_secsPerLightswitchInterval /= 2;
        }*/
        
        if (CurrentTime <= LevelTime - (m_secsPerLightswitchInterval * m_lightswitchIntervalCounter)) {
            m_shouldToggleLightSwitch = true;
        }
        
        if (m_shouldToggleLightSwitch) {
            for (int lightswitch = 0; lightswitch < LightMaskMeshes.Length; lightswitch++) {
                LightMaskMeshes[lightswitch].SetActive(!LightMaskMeshes[lightswitch].activeSelf);
                LightBlockerVolumes[lightswitch].SetActive(!LightBlockerVolumes[lightswitch].activeSelf);
                ShadowBlockerVolumes[lightswitch].SetActive(!ShadowBlockerVolumes[lightswitch].activeSelf);
            }
            ShouldSeekLight ^= true;
            
            // Cleanup
            m_shouldToggleLightSwitch = false;
            m_lightswitchIntervalCounter++;    
        }
    }

    private void ToggleRandomBlockerHandler()
    {
        if (CurrentTime <= LevelTime - (m_secsPerBlockerInterval * m_blockerIntervalCounter)) {
            m_shouldToggleBlocker = true;
        }
        
        if (m_shouldToggleBlocker) { // This toggles a shadow and light blocker volume at random, inverts the light and shadow mask
            var randomBlocker = Mathf.RoundToInt(Random.Range(0, ShadowBlockerVolumes.Length));
            LightMaskMeshes[randomBlocker].SetActive(!LightMaskMeshes[randomBlocker].activeSelf);
            LightBlockerVolumes[randomBlocker].SetActive(!LightBlockerVolumes[randomBlocker].activeSelf);
            ShadowBlockerVolumes[randomBlocker].SetActive(!ShadowBlockerVolumes[randomBlocker].activeSelf);

            // Cleanup
            m_shouldToggleBlocker = false;
            m_blockerIntervalCounter++;
        }
    }

    private void CalculateLightblockerIntervals() {
        var totalBlocks = LightMaskMeshes.Length;
        var minTimePerBlocker = LevelTime / totalBlocks;
        m_secsPerBlockerInterval = Mathf.RoundToInt(minTimePerBlocker - GameDifficulty);
    }
}