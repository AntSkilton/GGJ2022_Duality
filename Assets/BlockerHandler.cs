using UnityEngine;

public class BlockerHandler : MonoBehaviour
{
    public GameplayController GameplayController;

    // Sloppy way to handle it but it'll do!
    private void OnTriggerStay(Collider other)
    {
        GameplayController.IsInScoringZone = false;
        for (int volume = 0; volume < GameplayController.ShadowBlockerVolumes.Length; volume++)
        {
            if (other == GameplayController.ShadowBlockerVolumes[volume].GetComponent<MeshCollider>())
            {
                GameplayController.IsInShadow = true;
                if (!GameplayController.ShouldSeekLight)
                {
                    GameplayController.IsInScoringZone = true;
                    GameplayController.CurrentScore += 10 * Time.deltaTime;
                }
            }
        }

        for (int volume = 0; volume < GameplayController.ShadowBlockerVolumes.Length; volume++)
        {
            if (other == GameplayController.LightBlockerVolumes[volume].GetComponent<MeshCollider>())
            {
                GameplayController.IsInShadow = false;
                if (GameplayController.ShouldSeekLight)
                {
                    GameplayController.IsInScoringZone = true;
                    GameplayController.CurrentScore += 10 * Time.deltaTime;
                }
            }
        }
    }

    // This is a good example for using layers - could use this instead of Unity's event function?
    /*bool m_Started;
    public LayerMask LayerMask;

    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;
    }

    void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, LayerMask);
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < hitColliders.Length)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + hitColliders[i].name + i);
            //Increase the number of Colliders in the array
            i++;
        }
    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }*/
}