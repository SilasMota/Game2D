using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovment : MonoBehaviour {

    public PlayerState currentState;
	public float speed;
	private Rigidbody2D playerRigidbody;
	private Vector3 change;
	private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

	// Use this for initialization
	void Start () {
        currentState = PlayerState.walk;
        playerRigidbody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator> ();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
	}
	
	// Update is called once per frame
	void Update () {
		change = Vector3.zero;
		change.x = Input.GetAxisRaw("Horizontal");
		change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("atack") && 
            currentState != PlayerState.attack &&
            currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
	}

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.33f);
        currentState = PlayerState.walk;
    }

	void UpdateAnimationAndMove(){
		if (change != Vector3.zero) {
			MoveCharecter ();
			animator.SetFloat("moveX", change.x);
			animator.SetFloat("moveY", change.y);
			animator.SetBool("moving", true);
		} else {
			animator.SetBool("moving", false);
		}
	}

	void MoveCharecter(){
        change.Normalize();
		playerRigidbody.MovePosition (
			transform.position + change * speed * Time.deltaTime
		);
	}

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        } else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {

        if (playerRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            playerRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            playerRigidbody.velocity = Vector2.zero;
        }
    }
}
