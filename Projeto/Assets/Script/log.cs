using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy {

    private Rigidbody2D myRigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator anim;

	// Use this for initialization
	void Start () {
        target = GameObject.FindWithTag("Player").transform;
        homePosition = transform;
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        CheckDistnance();
	}

    void CheckDistnance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius &&
            Vector3.Distance(target.position, transform.position) > attackRadius &&
            (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger))
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnim(temp - transform.position);
            myRigidBody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            anim.SetBool("wakeUp", true);
        } else if (Vector3.Distance(target.position, transform.position) > chaseRadius){
            anim.SetBool("wakeUp", false);
        }
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    private void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newState){
        if(currentState != newState)
        {
            currentState = newState;
        }
    }
}

