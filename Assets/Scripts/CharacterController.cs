using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float lateralSmoothSpeed = 10f; //Yumuþak geçiþ hýzým.
    [SerializeField] GameObject menuPanel;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoretext;
    [SerializeField] TMP_Text cheeseText;

    [SerializeField] AudioSource effectSource,musicSource;
    [SerializeField] AudioClip cheeseClip, deathClip;


    private float[] xPosition = {0f, 0.368f, 0.736f };
    //Baþlangýç pozisyonum.
    int currentXpositionIndex = 0;
    Vector3 targetPosition;

    public bool isAlive = true;

    private float score;
    private int cheeseCount = 0;


    void Start()
    {
        targetPosition = transform.position; //Baþlangýç hedefimizi belirliyor.

        // High Score'u yükle
        float savedHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
        highScoretext.text = "High Score: " + savedHighScore.ToString("f1");

        cheeseText.text = "x 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            score += Time.deltaTime;
            scoreText.text = "Score: " + score.ToString("f1");

            if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && currentXpositionIndex > 0)
            {
                currentXpositionIndex--;
                UpdateLateralPosition();
            }
            else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && currentXpositionIndex < 2)
            {
                currentXpositionIndex++;
                UpdateLateralPosition();
            }
        }

    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Vector3 forwardMove = Vector3.forward * speed * Time.fixedDeltaTime;

            //Hedef noktasý pozisyonuna doðru yumuþak bir geçiþ yap
            Vector3 currentPosition = rb.position;
            Vector3 lateralMove = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * lateralSmoothSpeed);

            //Ýleri ve yanal hareketi birleþtirelim
            Vector3 combineMove = new Vector3(lateralMove.x, transform.position.y, rb.position.z) + forwardMove;
            rb.MovePosition(combineMove);

        }

    }

    void UpdateLateralPosition()
    {
        //Hedef pozisyonu çekilen x konumuna göre güncelleyecek
        targetPosition = new Vector3(xPosition[currentXpositionIndex], transform.position.y, transform.position.z);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cars"))
        {
            rb.isKinematic = true;
            isAlive = false;
            animator.avatar = null;
            animator.SetBool("Die", true);
            musicSource.Stop();
            effectSource.clip = deathClip;
            effectSource.Play();
            menuPanel.SetActive(true);

            // High Score kontrolü ve güncelleme
            float savedHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
            if (score > savedHighScore)
            {
                PlayerPrefs.SetFloat("HighScore", score);
                PlayerPrefs.Save(); // Verileri kalýcý olarak kaydet
            }

            // Menüde güncel high score göster
            highScoretext.text = "High Score: " + PlayerPrefs.GetFloat("HighScore").ToString("f1");
        }
    }

    //Peynir topladýkça olacaklar
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Cheese"))
    //    {
    //        score += 5;
    //        speed += 0.2f;
    //    }
    //}

    public void CollectCheese()
    {
        score += 5;
        speed += 0.1f;
        cheeseCount++; // Peynir sayýsýný artýr
        cheeseText.text = "x " + cheeseCount.ToString();
        effectSource.PlayOneShot(cheeseClip);
    }

}
