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

    [SerializeField] AudioSource effectSource,musicSource;
    [SerializeField] AudioClip cheeseClip, deathClip;


    private float[] xPosition = {0f, 0.368f, 0.736f };
    //Baþlangýç pozisyonum.
    int currentXpositionIndex = 0;
    Vector3 targetPosition;

    public bool isAlive = true;

    float score;
    void Start()
    {
        targetPosition = transform.position; //Baþlangýç hedefimizi belirliyor.
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            score += Time.deltaTime;
            scoreText.text = "Score: " + score.ToString("f1");
            if (Input.GetKeyDown(KeyCode.A) && currentXpositionIndex > 0)
            {
                currentXpositionIndex--; //Mevcut deðeri 1 azaltmak için.
                UpdateLateralPosition();
            }
            else if (Input.GetKeyDown(KeyCode.D) && currentXpositionIndex < 2)
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
        }
    }

    //Peynir topladýkça olacaklar
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cheese"))
        {
            score += 5;
            speed += 0.2f;
            effectSource.clip = cheeseClip;
            effectSource.Play();
            Destroy(other.gameObject);
        }
    }
}
