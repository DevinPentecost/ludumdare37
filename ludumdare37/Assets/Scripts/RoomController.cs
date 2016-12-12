using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
	private float roomEnterDistance = 2.5f;
	private float roomHalfWidth = 10.5f;
	public EnumHelper.RoomEnum roomEnum;

	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 getCameraPosition()
	{
		return this.gameObject.transform.FindChild(ToolboxSingleton.NAME_CENTER_POINT).transform.position;
	}

	public float getEnterX(bool enterFromRight)
	{
		Vector3 roomCenterPoint = getCameraPosition();
		// absolute X distance from center point
		float distanceFromCenter = roomHalfWidth - roomEnterDistance;

		// If entering from left we need to take the negative of the abs.
		if (!enterFromRight)
		{
			distanceFromCenter *= -1;
		}
		return roomCenterPoint.x + distanceFromCenter;
	}
}
