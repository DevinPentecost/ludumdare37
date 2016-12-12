using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]

public class KeyController : MonoBehaviour {
	BoxCollider2D boxCollider2D;
	SpriteRenderer spriteRenderer;
	public EnumHelper.KeyEnum RequiredKey = EnumHelper.KeyEnum.NONE;

	// Use this for initialization
	void Start () {
		boxCollider2D = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();

		UpdateColor();
	}

	// Update is called once per frame
	void Update () {

	}

	public void UpdateColor()
	{
		Color targetColor = EnumHelper.KeyColorMap[RequiredKey];
		spriteRenderer.color = targetColor;
	}
}
