using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// PlayerScript requires the GameObject to have a Rigidbody2D and BoxCollider2D component
[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (SpriteRenderer))]



public class PlayerController : MonoBehaviour {
	Rigidbody2D rigidBody2D;
	BoxCollider2D boxCollider2D;
	SpriteRenderer spriteRenderer;

	private readonly float walkingSpeed = 0.16f;
	private readonly float climbingSpeed = 0.1f;
	private readonly float gravityScale = 4;
	private bool canClimb = false;
	private bool canColorSwap = false;
	private bool spriteLeft = false;
	private GameController _gameController;

	public bool isPaused = false;
	public EnumHelper.RoomEnum currentRoom = ToolboxSingleton.START_ROOM;
	public EnumHelper.KeyEnum CurrentKey = EnumHelper.KeyEnum.NONE;
	KeyController CurrentKeyCollision;

// Use this for initialization
    void Start () {
	    rigidBody2D = GetComponent<Rigidbody2D>();
	    boxCollider2D = GetComponent<BoxCollider2D>();
	    spriteRenderer = GetComponent<SpriteRenderer>();

	    //Get the game controller via the tag
	    this._gameController =
		    GameObject.FindGameObjectWithTag(ToolboxSingleton.TAG_GAMECONTROLLER).GetComponent<GameController>();
    }

	// Update is called once per frame
    void Update()
    {

	    if (!isPaused)
	    {
		    PlayerInput();
	    }

	    this.UpdateColor();
    }

	void PlayerInput()
	{
		bool leftDown = Input.GetKey(ToolboxSingleton.KEY_LEFT);
		bool rightDown = Input.GetKey(ToolboxSingleton.KEY_RIGHT);
		bool spaceDown = Input.GetKeyDown(ToolboxSingleton.KEY_SPACE);


		if (leftDown || rightDown)
		{
			this.MovePlayer(leftDown);
		}

		if (canClimb)
		{
			bool upDown = Input.GetKey(ToolboxSingleton.KEY_UP);
			if (upDown)
			{
				this.ClimbPlayer();
			}
			else
			{
				this.rigidBody2D.gravityScale = this.gravityScale;
			}
		}

		if (spaceDown && canColorSwap)
		{
			swapKey();
		}
	}

    void MovePlayer(bool directionLeft)
    {
	    int directionMagnitude = -1;

	    //Is the user not pressing left? ie pressing right
	    if (!directionLeft)
	    {
		    directionMagnitude = 1;
		    // going right and sprite is left
		    if (spriteLeft)
		    {
			    spriteRenderer.flipX = !spriteLeft;
			    spriteLeft = !spriteLeft;
		    }
	    }
	    // If we're pressing left
	    else
	    {
		    // and the sprite is right
		    if (!spriteLeft)
		    {
			    spriteRenderer.flipX = !spriteLeft;
			    spriteLeft = !spriteLeft;
		    }
	    }

        float playerSpeed = directionMagnitude * this.walkingSpeed;

		//Update our position
		Vector3 currentPosition = this.transform.localPosition;
		currentPosition.x += playerSpeed;

		//Now set the position
		this.transform.localPosition = currentPosition;
    }

	void ClimbPlayer()
	{
		this.rigidBody2D.gravityScale = 0;
		this.rigidBody2D.velocity = new Vector2(0,0);

		//Update our position
		Vector3 currentPosition = this.transform.localPosition;
		currentPosition.y += this.climbingSpeed;

		//Now set the position
		this.transform.localPosition = currentPosition;

	}

	private void UpdateColor()
	{
		Color targetColor = EnumHelper.KeyColorMap[CurrentKey];
		spriteRenderer.color = targetColor;
	}

	private void swapKey()
	{
		EnumHelper.KeyEnum swapKey = CurrentKey;
		EnumHelper.KeyEnum targetKey = CurrentKeyCollision.RequiredKey;
		CurrentKeyCollision.RequiredKey = swapKey;
		CurrentKey = targetKey;
		CurrentKeyCollision.UpdateColor();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == ToolboxSingleton.TAG_LADDER)
		{
			this.canClimb = true;
		}
		if (other.gameObject.tag == ToolboxSingleton.TAG_KEY)
		{
			this.canColorSwap = true;
			CurrentKeyCollision = other.gameObject.GetComponent<KeyController>();
		}
		if (other.gameObject.tag == "GameEndTrigger")
		{
			_gameController.gameEnd();
		}

	}



	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == ToolboxSingleton.TAG_LADDER)
		{
			this.canClimb = false;
			this.rigidBody2D.gravityScale = this.gravityScale;
		}

		if (other.gameObject.tag == ToolboxSingleton.TAG_KEY)
		{
			this.canColorSwap = false;
		}

		if (other.gameObject.tag == ToolboxSingleton.TAG_BOUND)
		{
			roomEntered(other.gameObject.GetComponent<CameraBoundController>());
		}
	}

	public void roomEntered(CameraBoundController bound)
	{
		float playerX = this.transform.position.x;
		float boundX = bound.transform.position.x;
		// True if the player is exiting the bound to the left
		bool isLeft = playerX<boundX;

		EnumHelper.RoomEnum targetRoom = bound.getRoom(isLeft);
		if (targetRoom != currentRoom)
		{
			this._gameController.enterRoom(targetRoom,isLeft);
		}
	}


}
