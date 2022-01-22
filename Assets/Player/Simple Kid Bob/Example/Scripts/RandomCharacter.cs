using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacter : MonoBehaviour {

    private GameObjectsContainer[] gameObjectsContainers;
    private GameObjectsContainer gameObjectsContainer;
    private ChangeMaterialTexture MaterialScript;
    // Use this for initialization
    void Start () {
        MaterialScript = GetComponentInParent<ChangeMaterialTexture>();


    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void GenerateRandomCharacter()
    {
        gameObjectsContainers = GetComponentsInParent<GameObjectsContainer>();
        for (int i = 0; i < gameObjectsContainers.Length; i++)
        {
            gameObjectsContainer = gameObjectsContainers[i];
            for (int b = 0; b < gameObjectsContainer.GameObjects.Length; b++)
            {
                if(gameObjectsContainer.GameObjects[b] != null)
                {
                    gameObjectsContainer.GameObjects[b].SetActive(false);
                }
                
            }
            int RandomValue = Random.Range(0, gameObjectsContainer.GameObjects.Length);
            if(gameObjectsContainer.GameObjects[RandomValue] != null)
            {
                if (gameObjectsContainer.ContainerName != "Glasses")
                    gameObjectsContainer.GameObjects[RandomValue].SetActive(true);
                else gameObjectsContainer.GameObjects[RandomValue].SetActive((Random.value > 0.5f));
                gameObjectsContainer.ObjectSelected = RandomValue;
            }
        }
        MaterialScript.ChangeShaderButtonClicked();
        GetComponentInParent<GlassesTrans>().ChangeTransperancyButtonClicked();
    }
}
