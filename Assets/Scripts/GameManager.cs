using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] CharacterController characterController;

    [SerializeField] List<GameObject> Roads = new List<GameObject>();

    [SerializeField] Transform playerPrefab;
    [SerializeField] Transform carSpawn;

    [SerializeField] GameObject pausePanel;

    float previousPlayerZ;
    float roadLenght = 3.17f;

    int count = 5;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        //Ýlk zemini üret
        Instantiate(Roads[0],transform.position,transform.rotation);
        
        for (int i = 0; i < count; i++)
        {
            CreateRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPrefab.position.z > roadLenght - 3.17f * count)
        {
            CreateRoad();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !pausePanel.activeSelf;
            pausePanel.SetActive(isActive);

            if (isActive)
            {
                characterController.isAlive = false;
                //Time.timeScale = 0f; // Oyunu durdur
            }
            else
            {
                characterController.isAlive = true;
                //Time.timeScale = 1f; // Oyunu devam ettir
            }
        }
    }

    private void FixedUpdate()
    {
        //Oyuncunun z pozisyonundaki deðiþimini  hesapla
        float deltaZ = playerPrefab.position.z - previousPlayerZ;

        //Car spawner Z eksenini yukarýdaki deðiþim kadar arttýr
        carSpawn.position += new Vector3(0,0,deltaZ);

        //Bir sonraki kare için önceki z pozisyonunu gencelle
        previousPlayerZ = playerPrefab.position.z;
    }


    void CreateRoad()
    {
        Instantiate(Roads[Random.Range(0, Roads.Count)], transform.forward * roadLenght, transform.rotation);
        roadLenght += 3.17f;
    }

    public void RestartGame()
    {
        characterController.isAlive = true;
        isActive = true;
        //Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        characterController.isAlive = true;
        //Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Continue()
    {
        characterController.isAlive = true;
        isActive = true;
        pausePanel.SetActive(false);
        //Time.timeScale = 1f;
    }
}
