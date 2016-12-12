using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]

public class ObstacleController : MonoBehaviour {
	Rigidbody2D rigidBody2D;
	BoxCollider2D boxCollider2D;
	SpriteRenderer spriteRenderer;

	PlayerController ThePlayer;

	public EnumHelper.KeyEnum RequiredKey = EnumHelper.KeyEnum.NONE;

	// Use this for initialization
	void Start () {
		rigidBody2D = GetComponent<Rigidbody2D>();
		boxCollider2D = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		ThePlayer = (GameObject.FindGameObjectWithTag(ToolboxSingleton.TAG_PLAYER)).GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		this.UpdateColor();
		this.CheckKey();
	}

	private void CheckKey()
	{
		bool keysMatch = (this.RequiredKey == ThePlayer.CurrentKey || this.RequiredKey == EnumHelper.KeyEnum.NONE);

		this.rigidBody2D.simulated = !keysMatch;
	}

	private void UpdateColor()
	{
		Color targetColor = EnumHelper.KeyColorMap[RequiredKey];
		spriteRenderer.color = targetColor;
	}
}
