using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeSueTalk : MonoBehaviour
{
    public string ObjectName;
    public GameObject BlendShapeMesh;
    int blendShapeCount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;
    float blendCounter = 0f;
    float blendSpeed;
    bool blendFinished = true;
    int CurrentBlend = 0;
    float PreviosBlendCounter = 0f;
    
    int Counter = 0;
    public int TimesToRepeat;
    public float Minimum;
    public float Maximum;



    void Awake()
    {
        skinnedMeshRenderer = BlendShapeMesh.GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = BlendShapeMesh.GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        blendShapeCount = skinnedMesh.blendShapeCount;
        print("blendShapeCount =" + blendShapeCount);
    }


    // Update is called once per frame
    void Update()
    {
        if (!blendFinished)
        {
            if (PreviosBlendCounter > 10f)
            {
               // PreviosBlendCounter -= blendSpeed;
                skinnedMeshRenderer.SetBlendShapeWeight(CurrentBlend - 1, PreviosBlendCounter);
            }
            if (blendCounter < 90f)
            {
                skinnedMeshRenderer.SetBlendShapeWeight(CurrentBlend, blendCounter);
                blendCounter += blendSpeed;
            }
            else
            {
                if (Counter  < TimesToRepeat)
                {
                    Counter++;
                    talk();
                }
                else
                {
                    
                    Counter = 0;
                    blendFinished = true;
                }
            }
        }
    }
    public void talk()
    {
        
        blendSpeed = Random.RandomRange(Minimum, Maximum);
        blendCounter = 0;
        blendFinished = false;
        PreviosBlendCounter = skinnedMeshRenderer.GetBlendShapeWeight(CurrentBlend);
        skinnedMeshRenderer.SetBlendShapeWeight(CurrentBlend, 0f);
        if (CurrentBlend < blendShapeCount)
            CurrentBlend++;
        else
            CurrentBlend = 0;
    }
}
