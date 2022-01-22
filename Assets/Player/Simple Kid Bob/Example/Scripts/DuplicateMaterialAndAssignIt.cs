using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class DuplicateMaterialAndAssignIt : MonoBehaviour {

    public Material mainShader;
    int Counter = 0;
    Renderer[] rend;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SaveObjectToFile()
    {
        mainShader = GetComponentInChildren<Renderer>().sharedMaterials[0];


        string textureName = "ForGeneratedChar" + Counter;
        Material newMaterial = new Material(mainShader);
        newMaterial.CopyPropertiesFromMaterial(mainShader);

        string matPath = "Assets/" + textureName + ".mat";

        
        //check if file already exists
        while(System.IO.File.Exists(matPath))
        {
            Counter++;
           textureName = "ForGeneratedChar" + Counter;
            matPath = "Assets/" + textureName + ".mat";
        }
        /*

        AssetDatabase.CreateAsset(newMaterial, matPath);
        newMaterial = (Material)AssetDatabase.LoadAssetAtPath(matPath, typeof(Material));

        newMaterial.SetTexture(textureName, mainShader.mainTexture);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        */
        //lets Assign it to For Duplicate
        Renderer[] rend = GetComponentsInChildren<Renderer>();
        foreach (Renderer Check in rend)
        {
            Check.enabled = true;
            Check.sharedMaterial = newMaterial;
        }

        //RemoveAllTheMods VerySpecificTo Simple lady susan ough Well...
        GameObjectsContainer[] StuffToDestroy = GetComponentsInParent<GameObjectsContainer>();
        foreach (GameObjectsContainer ComponentToDestroy in StuffToDestroy)
        {
            DestroyImmediate(ComponentToDestroy);
        }
        DestroyImmediate(GetComponentInParent<ChangeBlendShapeOfSelectedObject>());
        DestroyImmediate(GetComponentInParent<GlassesTrans>());
        DestroyImmediate(GetComponentInParent<ChangeShader>());
        DestroyImmediate(GetComponentInParent<ChangeMaterialTexture>());
        DestroyImmediate(GetComponentInParent<RandomCharacter>());
        DestroyImmediate(GetComponentInParent<DuplicateMaterialAndAssignIt>());
    }
}
