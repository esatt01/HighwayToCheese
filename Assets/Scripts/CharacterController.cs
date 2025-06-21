using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float lateralSmoothSpeed = 10f; //Yumu�ak ge�i� h�z�m.
    [SerializeField] GameObject menuPanel;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoretext;
    [SerializeField] TMP_Text cheeseText;

    [SerializeField] AudioSource effectSource,musicSource;
    [SerializeField] AudioClip cheeseClip, deathClip;


    private float[] xPosition = {0f, 0.368f, 0.736f };
    //Ba�lang�� pozisyonum.
    int currentXpositionIndex = 0;
    Vector3 targetPosition;

    public bool isAlive = true;

    private float score;
    private int cheeseCount = 0;


    void Start()
    {
        targetPosition = transform.position; //Ba�lang�� hedefimizi belirliyor.

        // High Score'u y�kle
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

            //Hedef noktas� pozisyonuna do�ru yumu�ak bir ge�i� yap
            Vector3 currentPosition = rb.position;
            Vector3 lateralMove = Vector3.Lerp(currentPosition, targetPosition, Time.fixedDeltaTime * lateralSmoothSpeed);

            //�leri ve yanal hareketi birle�tirelim
            Vector3 combineMove = new Vector3(lateralMove.x, transform.position.y, rb.position.z) + forwardMove;
            rb.MovePosition(combineMove);

        }

    }

    void UpdateLateralPosition()
    {
        //Hedef pozisyonu �ekilen x konumuna g�re g�ncelleyecek
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

            // High Score kontrol� ve g�ncelleme
            float savedHighScore = PlayerPrefs.GetFloat("HighScore", 0f);
            if (score > savedHighScore)
            {
                PlayerPrefs.SetFloat("HighScore", score);
                PlayerPrefs.Save(); // Verileri kal�c� olarak kaydet
            }

            // Men�de g�ncel high score g�ster
            highScoretext.text = "High Score: " + PlayerPrefs.GetFloat("HighScore").ToString("f1");
        }
    }

    //Peynir toplad�k�a olacaklar
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
        cheeseCount++; // Peynir say�s�n� art�r
        cheeseText.text = "x " + cheeseCount.ToString();
        effectSource.PlayOneShot(cheeseClip);
    }

}
