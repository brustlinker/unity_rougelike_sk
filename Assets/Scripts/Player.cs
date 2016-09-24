using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {



	private int posx = 1;
	private int posy = 1;

	private const string xAxis="Horizontal";
	private const string yAxis="Vertical";


	[HideInInspector]public Vector2 targetPos=new Vector2(1,1);
	private Rigidbody2D rigidbody;
	private BoxCollider2D collider;
	private Animator animator;
	public float smoothing = 1;
	public float restTime = 1;
	public float restTimer = 0;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
		collider = GetComponent<BoxCollider2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {


		rigidbody.MovePosition(Vector2.Lerp(transform.position,targetPos,smoothing*Time.deltaTime));


		if(GameManager.Instance.food<0||GameManager.Instance.isEnd==true)
		{
			return ;
		}


		restTimer+=Time.deltaTime;
		if(restTimer<restTime) return;

		float h = Input.GetAxisRaw(xAxis);

		//float h = 1;
		float v = Input.GetAxisRaw(yAxis);
		if( h > 0 )
		{
			v=0;
		}



		if(h!=0||v!=0)
		{
			GameManager.Instance.ReduceFood(1);
			//检测
			collider.enabled = false;
			RaycastHit2D hit = Physics2D.Linecast(targetPos,targetPos+new Vector2(h,v));
			collider.enabled = true;

			//没有碰撞到东西
			if(hit.transform==null)
			{
				
				targetPos += new Vector2(h,v);
			}
			else
			{
				switch(hit.collider.tag)
				{
					case "OutWall":
						break;
					case "Wall":
						animator.SetTrigger("Attack");
						hit.collider.SendMessage("TakeDamage");
						break;
					case "Food":
						GameManager.Instance.AddFood(10);
						targetPos += new Vector2(h,v);
						Destroy(hit.transform.gameObject);
						break;
					case "Soda":
						GameManager.Instance.AddFood(20);
						targetPos += new Vector2(h,v);
						Destroy(hit.transform.gameObject);
						break;
					case "Enemy":
						break;
				}
			}
			GameManager.Instance.OnPlayerMove();
			//无论攻击或者移动都需要休息
			restTimer = 0;
		}





	}

	public void TakeDamage(int lossFood)
	{
		GameManager.Instance.ReduceFood(lossFood);
		animator.SetTrigger("Damage");
	}
}
