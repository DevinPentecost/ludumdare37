using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookyGhostController : MonoBehaviour {
	//Components
	private SpriteRenderer _targetSpriteRenderer;

	//What key does this ghost represent
	public EnumHelper.KeyEnum targetKey = EnumHelper.KeyEnum.NONE;

	//What are the possible ranges for the X and Y, around the origin
	private Vector2 _xFloatRange = new Vector2(-0.5f, 0.5f);
	private Vector2 _yFloatRange = new Vector2(-0.5f, 0.5f);

	//What is the posible range of time for it to take to move to a point
	private Vector2 _floatTimeRange = new Vector2(1f, 1.75f);

	//What easing to use?
	private LeanTweenType _easing = LeanTweenType.easeInOutQuad;
	private ParticleSystem _targetParticleSystem;

	// Use this for initialization
	void Start () {
		//Get our components
		_targetSpriteRenderer =
			this.transform.FindChild(ToolboxSingleton.NAME_SPOOKY_SPRITE).GetComponent<SpriteRenderer>();
		_targetParticleSystem =
			this.transform.FindChild(ToolboxSingleton.NAME_SPOOKY_PARTICLES).GetComponent<ParticleSystem>();

		//Start by kicking off movements
		FloatX();
		FloatY();
	}
	
	// Update is called once per frame
	void Update () {
		//Update the color
		this.UpdateColor();
	}

	void FloatX()
	{
		//We want to get a random x position to float to, as well as the time to get there
		var targetX = Random.Range(_xFloatRange.x, _xFloatRange.y);
		var targetTime = Random.Range(_floatTimeRange.x, _floatTimeRange.y);


		//Now we tween ourselves locally to that x, using easing and kicking off another float after it's done
		LeanTween.moveLocalX(this.gameObject, targetX, targetTime)
			.setEase(this._easing)
			.setOnComplete(this.FloatX);
	}

	void FloatY()
	{
		//We want to get a random x position to float to, as well as the time to get there
		var targetY = Random.Range(_yFloatRange.x, _yFloatRange.y);
		var targetTime = Random.Range(_floatTimeRange.x, _floatTimeRange.y);


		//Now we tween ourselves locally to that x
		LeanTween.moveLocalY(this.gameObject, targetY, targetTime)
			.setEase(this._easing)
			.setOnComplete(this.FloatY);
	}

	private void UpdateColor()
	{
		//Get the expected color and set the sprite's color to match
		Color targetColor = EnumHelper.KeyColorMap[targetKey];
		_targetSpriteRenderer.color = targetColor;

		//Also update the color of the particle system
		_targetParticleSystem.startColor = targetColor;
	}

}
