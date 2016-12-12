using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (BoxCollider2D))]

public class CameraBoundController : MonoBehaviour {
	BoxCollider2D boxCollider2D;
	public EnumHelper.RoomEnum roomLeft;
	public EnumHelper.RoomEnum roomRight;

	// Use this for initialization
	void Start () {
		boxCollider2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public EnumHelper.RoomEnum getRoom(bool isLeft)
	{
		if (isLeft)
		{
			return this.roomLeft;
		}
		else
		{
			return this.roomRight;
		}
	}
}
