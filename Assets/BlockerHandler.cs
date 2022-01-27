using UnityEngine;

public class BlockerHandler : MonoBehaviour
{
    public GameplayController GameplayController;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Blocker")) {
            GameplayController.CurrentScore += 10 * Time.deltaTime;
        }
    }
}