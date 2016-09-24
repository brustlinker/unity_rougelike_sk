using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {



	private int posx = 1;
	private int posy = 1;

	private const string xAxis="Horizontal";
	private const string yAxis="Vertical";


	private Vector2 targetPos=new Vector2(1,1);
	private Rigidbody2D rigidbody;
	public float smoothing = 1;
	public float restTime = 1;
	public float restTimer = 0;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {


		rigidbody.MovePosition(Vector2.Lerp(transform.position,targetPos,smoothing*Time.deltaTime));


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
			targetPos += new Vector2(h,v);
			restTimer = 0;
		}
	}
}
