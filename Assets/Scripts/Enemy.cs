using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	private Transform player;


	private Vector2 targetPosition;
	public float    smoothing =3;
	private Rigidbody2D rigidbody;
	private BoxCollider2D collider;
	private Animator animator;

	public int lossFood = 10;

	void Start()
	{
		player=GameObject.FindGameObjectWithTag("Player").transform;
		rigidbody=GetComponent<Rigidbody2D>();
		collider=GetComponent<BoxCollider2D>();
		targetPosition=transform.position;
		animator=GetComponent<Animator>();
		GameManager.Instance.enemyList.Add(this);

	}


	void Update()
	{
		Vector2 position=Vector2.Lerp(transform.position,targetPosition,smoothing*Time.deltaTime);
		rigidbody.MovePosition(position);
	}

	public void Move()
	{
		Vector2 offset= player.position-transform.position;

		//攻击
		if(offset.magnitude<1.1f)
		{
			animator.SetTrigger("Attack");
			player.SendMessage("TakeDamage",lossFood);
		}
		//追击
		else
		{
			float x =0; float y=0;
			if(Mathf.Abs(offset.y)>Mathf.Abs(offset.x))
			{
				if(offset.y<0)
				{
					y=-1;
				}
				else
				{
					y=1;
				}
			}
			else
			{
				if(offset.x<0)
				{
					x=-1;
				}
				else
				{
					x=1;
				}
				
			}

			//设置目标之前先做检测
			collider.enabled = false;
			RaycastHit2D hit = Physics2D.Linecast(targetPosition,targetPosition+new Vector2(x,y));
			collider.enabled = true;
			if(hit.transform == null)
			{
				targetPosition+=new Vector2(x,y);
			}
			else
			{
				if(hit.collider.tag=="Food"||hit.collider.tag=="Soda")
				{
					targetPosition+=new Vector2(x,y);
				}
			}

		}
	}
}
