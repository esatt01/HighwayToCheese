using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{

    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject[] carPrefabs; //Üretilecek olan arabalar
    [SerializeField] GameObject[] cheeses; //Üretilecek peynirler

    //Min ve Max üretme aralýðý
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
        while (true)  // sonsuz döngü
        {
            if (characterController.isAlive)
            {
                float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(randomTime);

                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomIndex];
                Instantiate(
                  carPrefabs[Random.Range(0, carPrefabs.Length)],
                  spawnPoint.position,
                  spawnPoint.rotation
                );
            }
            else
            {
                // Oyun duraksamýþ, bir frame bekle ve sonra tekrar kontrol et
                yield return null;
            }
        }
    }

    IEnumerator SpawnCheeses()
    {
        while (true)
        {
            if (characterController.isAlive)
            {
                //Rastgele bir süre beklet
                float randomTime = Random.Range(cheeseSpawnMin, cheeseSpawnMax);
                yield return new WaitForSeconds(randomTime);

                //Rastgele bir referans noktasý seç
                int randomIndex = Random.Range(0, spawnPoints.Length);
                Transform spawnPoint = spawnPoints[randomIndex];

                //Peyniri üretmek
                Instantiate(cheeses[0],
                    spawnPoint.position + new Vector3(0, 0.01f, 0), Quaternion.Euler(0, 200f, 0));
            }
            else
            {
                // Oyun duraksamýþ, bir frame bekle ve sonra tekrar kontrol et
                yield return null;
            }
        }
    }
}
