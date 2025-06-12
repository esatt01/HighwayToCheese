using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] carPrefabs; //�retilecek olan arabalar
    [SerializeField] GameObject[] cheeses; //�retilecek peynirler

    //Min ve Max �retme aral���
    [SerializeField] float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 3f;
    [SerializeField] float cheeseSpawnMin = 3f;
    [SerializeField] float cheeseSpawnMax = 10f;

    [SerializeField] CharacterController characterController;

    void Start()
    {
        StartCoroutine(SpawnCars());
        StartCoroutine(SpawnCheeses());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnCars()
    {
        while (characterController.isAlive)
        {
            //Rastgele bir s�re beklet
            float randomTime  = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomTime);

            //Rastgele bir referans noktas� se�
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            //Arabay� �retmek
            Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        }
    }
    IEnumerator SpawnCheeses()
    {
        while (characterController.isAlive)
        {
            //Rastgele bir s�re beklet
            float randomTime = Random.Range(cheeseSpawnMin, cheeseSpawnMax);
            yield return new WaitForSeconds(randomTime);

            //Rastgele bir referans noktas� se�
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            //Peyniri �retmek
            Instantiate(cheeses[0], spawnPoint.position + new Vector3(0,0.01f,0), spawnPoint.rotation);
        }
    }
}
