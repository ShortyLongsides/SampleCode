using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;

    private Rigidbody rb;

    private AudioSource playerAudio;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private AudioClip crashSound;

    [SerializeField]
    private ParticleSystem playerExplosion;

    [SerializeField]
    private ParticleSystem playerDirtSplatter;

    [SerializeField]
    public bool gameOver = false;

    [SerializeField]
    private int jumpForce = 10;

    [SerializeField]
    private float gravityModifier = 1;

    [SerializeField]
    private bool isOnGround = true;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        playerJump();

    }

    private void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isOnGround == true))
        {
            if (!gameOver)
            {
                isOnGround = false;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                playerDirtSplatter.Stop();
                playerAudio.PlayOneShot(jumpSound, 2.0f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            playerDirtSplatter.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerAnim.SetInteger("DeathType_int", 1);
            playerAnim.SetBool("Death_b", true);
            playerDirtSplatter.Stop();
            playerExplosion.Play();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
