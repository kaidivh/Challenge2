using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{	
	public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;
	
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
	
	public Text winText;
	
	public Text livesText;
	
	private int level = 1;
	
	private int livesValue = 3;

    private int scoreValue = 0;
	
	private bool facingRight = true;
	
	Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
		winText.text = "";
		livesText.text = "Lives: " + livesValue.ToString();
		musicSource.clip = musicClipOne;
		musicSource.Play();
		anim = GetComponent<Animator>();
		anim.SetBool("isIdle", true);
    }

    // Update is called once per frame
    void Update()
	{
		
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
		
		if (scoreValue == 4 && level == 1)
		{
			level += 1;
            scoreValue = 0;
			score.text = "Score: " + scoreValue.ToString();
			livesValue = 3;
			livesText.text = "Lives: " + livesValue.ToString();
            transform.position = new Vector3(40, 0, 0);
            //set movement 0
            rd2d.velocity = new Vector2(0,0);
            rd2d.angularVelocity = 0;
		}
		
		if (scoreValue == 4 && level == 2)
		{
			level += 1; //so it doesn't continously stop and restart the win music
			winText.text = "You Win! Game by Collin Conner!";
			musicSource.Stop();
			musicSource.clip = musicClipTwo;
			musicSource.Play();
		}
		
		if (livesValue == 0)
		{
			winText.text = "You Lose! :-( Game by Collin Conner!";
			gameObject.SetActive(false);
		}
		
		if ((Input.GetKeyDown(KeyCode.A)) || Input.GetKeyDown(KeyCode.D)){ // running
				anim.SetBool("isIdle", false);
		}
		
		if ((Input.GetKeyUp(KeyCode.A)) || Input.GetKeyUp(KeyCode.D)){ // idle
				anim.SetBool("isIdle", true);
		}
		
    }
	
	void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
		
		if (facingRight == false && hozMovement > 0){
			Flip();
		}
		else if (facingRight == true && hozMovement < 0){
    		Flip();
		}
		
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

    }*/
	
	void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
			other.gameObject.SetActive(false);
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
        }
		
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            livesValue = livesValue - 1;
            livesText.text = "Lives: " + livesValue.ToString();
            if (livesValue == 0)
            {
                winText.text = "You lose :-( Game created by Collin Conner!";
                gameObject.SetActive(false);
                //endgame
            }
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
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
			
			/*if ((Input.GetKeyDown(KeyCode.A)) || Input.GetKeyDown(KeyCode.D)){ // running
				if (collision.collider.tag == "Ground"){
				anim.SetBool("isIdle", false);
				}
			}
			
			if ((Input.GetKeyUp(KeyCode.A)) || Input.GetKeyUp(KeyCode.D)){ // idle
				if (collision.collider.tag == "Ground"){
				anim.SetBool("isIdle", true);
				}
			}*/
			
        }
	}
	
	private void OnCollisionExit2D(Collision2D collision){
		if (collision.collider.tag == "Ground"){
				anim.SetBool("isJumping", true);
		}
	}
	
	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.tag == "Ground"){
				anim.SetBool("isJumping", false);
		}
	}
	
}