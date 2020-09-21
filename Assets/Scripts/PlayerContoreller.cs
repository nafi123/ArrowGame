using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContoreller : MonoBehaviour {
	private float mySpeedX;
	[SerializeField] float speed;
	[SerializeField] float jumpPower;
	private Rigidbody2D myBody; 
	private Vector3 defaultLocalScale;
	public bool onGround; //zemin üzerinde mi değil mi 
	private bool canDoubleJump;
	[SerializeField] GameObject arrow;
	[SerializeField] bool attacked;
	[SerializeField] float currentAttacktTimer;
	[SerializeField] float defaultAttacktTimer;
	private Animator myAnimator;
	[SerializeField] int arrowNumber;
	[SerializeField] Text arrowNumberText;
	[SerializeField] AudioClip dieMusic;
	[SerializeField] GameObject winPanel, losePanel;
	// Use this for initialization
	void Start () {
		
		attacked = false;
		myAnimator = GetComponent<Animator>();
		myBody = GetComponent<Rigidbody2D>();
		defaultLocalScale = transform.localScale;
		arrowNumberText.text = arrowNumber.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		mySpeedX = Input.GetAxis("Horizontal"); //get axis eksenleri alıyor horizontal ise x ekseni için kullanlıyor.
		myAnimator.SetFloat("Speed", Mathf.Abs(mySpeedX));
		myBody.velocity = new Vector2(mySpeedX * speed, myBody.velocity.y); //velocity yani sürati x eksenindeki hızını belirliyor
        #region playerın sağ ve sola gittiğinde yüzünün dönmesi
        if (mySpeedX > 0) // eğer sıfırdan büyükse hız bu demektir ki sağa gidiyor 
        {
			transform.localScale = new Vector3(defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.y); //normalde burda defaultLocalScale yerine 1f yazacaktık
        }
		else if (mySpeedX < 0) //sıfırdan küçükse demektir ki sola gidiyor

        {
			transform.localScale = new Vector3(-defaultLocalScale.x, defaultLocalScale.y, defaultLocalScale.z);
        }
        #endregion

        #region playerın zıplamasının kontrol edilmesi
        if (Input.GetKeyDown(KeyCode.Space))
        {
			
			if (onGround == true)
			{
				myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
				canDoubleJump = true;
				myAnimator.SetTrigger("Jump");
			}
			else
                if (canDoubleJump == true)
            {
				myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
				canDoubleJump = false;

			}
		}
        #endregion

        #region playerın ok atmasının kontrolü
        if (Input.GetMouseButtonDown(0) && arrowNumber>0)
        {
            if (attacked == false)
            {
				attacked = true;
				myAnimator.SetTrigger("Attack");
				Invoke("Fire", 0.5f);
			}
			

		}

        #endregion
        if (attacked == true)
        {

			currentAttacktTimer -= Time.deltaTime;
        }
        else
        {
			currentAttacktTimer = defaultAttacktTimer;
        }
        if (currentAttacktTimer < 0)
        {
			attacked = false;
        }
		
    }
	void Fire()
	{
		GameObject okumuz = Instantiate(arrow, transform.position, Quaternion.identity);
		okumuz.transform.parent = GameObject.Find("Arrows").transform;
		okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
		if (transform.localScale.x > 0)
		{
			okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(5f, 0f);
		}
		else
		if (transform.localScale.x < 0)
		{
			Vector3 okumuzScale = okumuz.transform.localScale;
			okumuz.transform.localScale = new Vector3(-okumuzScale.x, okumuzScale.y, okumuzScale.z);
			okumuz.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f, 0f);

		}
		arrowNumber--;
		arrowNumberText.text = arrowNumber.ToString();
	}
	private void OnCollisionEnter2D(Collision2D collision)
    {
		if(collision.gameObject.CompareTag("Enemy"))
        {
			GetComponent<TimeControl>().enabled = false;
			Die();
        }
		else if (collision.gameObject.CompareTag("Finish"))
        {
			Destroy(collision.gameObject);
			StartCoroutine(Wait(true));
        }
    }
	
	public void Die()
    {
		GameObject.Find("Sound Controller").GetComponent<AudioSource>().clip = null;
		GameObject.Find("Sound Controller").GetComponent<AudioSource>().PlayOneShot(dieMusic);
		myAnimator.SetTrigger("Die");
		myAnimator.SetFloat("Speed", 0);
		myBody.constraints = RigidbodyConstraints2D.FreezeAll;
		enabled = false;
		StartCoroutine(Wait(false));
    }
	IEnumerator Wait(bool win)
    {
		yield return new WaitForSecondsRealtime(2f);
		Time.timeScale = 0;
		if(win == true)
        {
			winPanel.SetActive(true);
        }
        else
        {
			losePanel.SetActive(true);
        }
    }
}
