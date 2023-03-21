using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public float hozMovement;
    public float vertMovement;
    public Text score;
    public Text lives;
    public Text winText;
    public GameObject winTextObject;

    private int scoreValue;
    private int livesValue;
    public AudioClip bgAudio;

    public AudioClip winAudio;

    public AudioSource bgSource;

    Animator anim;
    private bool facingRight = true;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    void Start()
    {
        scoreValue = 0;
        livesValue = 3;

        rd2d = GetComponent<Rigidbody2D>();

        score.text = "Coins: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();

        winTextObject.SetActive(false);

        bgSource.clip = bgAudio;
        bgSource.Play();
        bgSource.loop = true;

        anim = GetComponent<Animator>();

        bgSource = gameObject.AddComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))

        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))

        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))

        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))

        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))

        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))

        {
            anim.SetInteger("State", 0);
        }


        if (scoreValue == 10)
        {
            winTextObject.SetActive(true);
            speed = 0;
        }

        if (livesValue <= 0)
        {
            winText.text = "You lose!";
            winTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue = livesValue - 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }


    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector2(30, 0);
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
        }
    }
}