using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController15 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    int healthCount = 100;
    int coinCount;

    private Rigidbody2D rb2D;
    private Animator animator;

    public Text health;
    public Text coins;

    public AudioClip[] audioClipArr;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float hVelocity = 0;
        float vVelocity = 0;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            audioSource.PlayOneShot(audioClipArr[3]);
            hVelocity = moveSpeed;
            transform.localScale = new Vector3(1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            audioSource.Stop();
            animator.SetFloat("xVelocity", 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            audioSource.PlayOneShot(audioClipArr[3]);
            hVelocity = -moveSpeed;
            transform.localScale = new Vector3(-1, 1, 1);
            animator.SetFloat("xVelocity", Mathf.Abs(hVelocity));
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            audioSource.Stop();
            animator.SetFloat("xVelocity", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            vVelocity = jumpForce;
            animator.SetTrigger("JumpTrigger");
            audioSource.PlayOneShot(audioClipArr[1]);
        }

        hVelocity = Mathf.Clamp(rb2D.velocity.x + hVelocity, -5, 5);

        rb2D.velocity = new Vector2(hVelocity, rb2D.velocity.y + vVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(audioClipArr[2]);
            healthCount -= 10;
            health.text = "Health: " + healthCount;
        }
        if (collision.gameObject.tag == "Coin")
        {   
            audioSource.PlayOneShot(audioClipArr[0]);
            coinCount += 1;
            coins.text = "Coins: " + coinCount;
            Destroy(collision.gameObject);
        }
    }
}
