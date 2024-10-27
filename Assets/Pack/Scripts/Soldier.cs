﻿using UnityEngine;
using System.Collections;
/// <summary>
/// Soldiers sprite movements
/// </summary>
public class Soldier: MonoBehaviour {
	private float maxspeed; //walk speed
	Animator anim;
	private bool faceright; //face side of sprite activated
	private bool jumping=false;
	private bool isdead=false;
	//--
	void Start () {
		maxspeed=2f;//Set walk speed
		faceright=true;//Default right side
		anim = this.gameObject.GetComponent<Animator> ();
		anim.SetBool ("walk", false);//Walking animation is deactivated
		anim.SetBool ("dead", false);//Dying animation is deactivated
		anim.SetBool ("jump", false);//Jumping animation is deactivated
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground"){//################Important, the floor Tag must be "Ground" to detect the collision!!!!
			jumping=false;
			anim.SetBool ("jump", false);
		}
	}

	void Update () {
		if(isdead==false){
			//--DYING
			//if(Input.GetKey ("k")){//###########Change the dead event, for example: life bar=0
			//	anim.SetBool ("dead", true);
			//	isdead=true;
			//}
			//--END DYING

			//--JUMPING
			//if (Input.GetButtonDown("Jump")){//Saltar
			//	if(jumping==false){//only once time each jump
			//		GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,200));
			//		jumping=true;
			//		anim.SetBool ("jump", true);
			//	}
			//}
			//--END JUMPING

			//--WALKING 
			float move_x = Input.GetAxis ("Horizontal");
			GetComponent<Rigidbody2D>().velocity = new Vector2(move_x * maxspeed, GetComponent<Rigidbody2D>().velocity.y);
			if(move_x>0){//Go right
				anim.SetBool ("walk", true);//Walking animation is activated
				if(faceright==false){
					Flip ();
				}
			}		
			if((move_x<0)){//Go left
				anim.SetBool ("walk", true);
				if(faceright==true){
					Flip ();
				}
			}

			float move_y = Input.GetAxis("Vertical");
			GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, move_y * maxspeed);
			if (move_y > 0)
			{//Go down
				anim.SetBool("walk", true);//Walking animation is activated
				
			}
			if ((move_y < 0))
			{//Go left
				anim.SetBool("walk", true);
				
			}
			if (move_x == 0 && move_y == 0)
			{//Stop
				anim.SetBool("walk", false);
			}



		}
	}

	void Flip(){
		faceright=!faceright;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
