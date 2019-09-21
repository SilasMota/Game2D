using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {

    public float thrust;
    public float knockTime;
    public float damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player") && other.isTrigger && !other.gameObject.CompareTag(this.gameObject.tag))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (hit.gameObject.CompareTag("enemy") )
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit,knockTime,damage);
                } else
                {
                    if (other.GetComponent<PlayerMovment>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovment>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovment>().Knock(knockTime, damage);
                    }
                }                
            }
        }
    }

}
