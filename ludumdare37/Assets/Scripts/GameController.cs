using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public Dictionary<EnumHelper.RoomEnum, RoomController> rooms = new Dictionary<EnumHelper.RoomEnum, RoomController>();

	private PlayerController _playerController;
	private static float playerEnterTime = 1.5f;
	private static float roomFadeTime = 0.5f;
	private SpriteRenderer fadeImageSprite;
	private Camera gameControllerCamera;
	public Sprite EndScreen;

	// Use this for initialization
	void Start () {
		//Get the player controller
		this._playerController = GameObject.FindGameObjectWithTag(ToolboxSingleton.TAG_PLAYER).GetComponent<PlayerController>();
		this.fadeImageSprite = GameObject.FindGameObjectWithTag(ToolboxSingleton.TAG_FADE_IMAGE).GetComponent<SpriteRenderer>();
		gameControllerCamera = this.gameObject.GetComponent<Camera>();
		getRooms();
	}
	
	// Update is called once per frame
	void Update () {

		//Did the player want to quit?
		if (Input.GetKeyDown(ToolboxSingleton.KEY_ESCAPE))
		{
			//Quit
			Debug.Log("Quitting!");
			Application.Quit();
		}

	}

	private void getRooms()
	{
		GameObject[] Rooms = GameObject.FindGameObjectsWithTag(ToolboxSingleton.TAG_ROOM);
		foreach (GameObject child in Rooms)
		{
			RoomController roomController = child.GetComponent<RoomController>();
			EnumHelper.RoomEnum roomEnum = roomController.roomEnum;
			rooms.Add(roomEnum,roomController);
		}
	}

	public void enterRoom(EnumHelper.RoomEnum targetRoom, bool isMovingLeft)
	{
		RoomController targetRoomController = rooms[targetRoom];
		Vector3 targetCameraPosition = targetRoomController.getCameraPosition();
		_playerController.isPaused = true;

		//Get the texture from the camera
		Texture2D previousRoomImage = this.renderCameraToImage();

		//Create a sprite from that texture and display it

		//Fade it in, set the sprite, and then fade it out
		LeanTween.alpha(fadeImageSprite.gameObject, 1, 0);
		fadeImageSprite.sprite = Sprite.Create(previousRoomImage,new Rect(0,0,previousRoomImage.width,previousRoomImage.height),new Vector2(0.5f,0.5f));
		LeanTween.alpha(fadeImageSprite.gameObject, 0, roomFadeTime);


		// get X position for player to move to, isMovingLeft==entering from right
		float targetX = targetRoomController.getEnterX(isMovingLeft);
		// Tween player to the target x enter position and unpause inputs when tween complete
		LeanTween.moveX(_playerController.gameObject, targetX, playerEnterTime).setDelay(roomFadeTime).setOnComplete(enterRoomComplete);
		this._playerController.currentRoom = targetRoom;
		Camera.main.transform.position = targetCameraPosition;

	}

	public void gameEnd()
	{
		LeanTween.alpha(fadeImageSprite.gameObject, 0, 0);
		fadeImageSprite.sprite = this.EndScreen;
		LeanTween.alpha(fadeImageSprite.gameObject, 1, roomFadeTime);
		_playerController.isPaused = true;
	}
	public void enterRoomComplete()
	{
		this._playerController.isPaused = false;
	}
	public Texture2D renderCameraToImage()
	{

		//Get the height of the camera (in pixels)
		var height = (int)((2 * Camera.main.orthographicSize) * 100);
		var width = (int)((height * Camera.main.aspect));

		//Store the old render textures (Active and Camera)
		RenderTexture previousRenderTexture = RenderTexture.active;
		RenderTexture previousCameraRenderTexture = Camera.main.targetTexture;

		//Create a temporary render texture to use as our scratch space
		RenderTexture newRenderTexture = RenderTexture.GetTemporary(width, height, 16);

		//Apply this render texture to our active and to the camera
		Camera.main.targetTexture = newRenderTexture;
		RenderTexture.active = newRenderTexture;

		//Render the camera into the render texture
		Camera.main.Render();

		// Creating a new Texture2D with the dimensions of the camera
		Texture2D image = new Texture2D(width,height);
		// This new image reads the pixels from the active render texture
		image.ReadPixels(new Rect(0,0, width,height),0,0);
		// Applying the read pixels to the image
		image.Apply();

		//Restore it
		RenderTexture.active = previousRenderTexture;
		Camera.main.targetTexture = previousCameraRenderTexture;

		//Release the temporary render texture in case anyone needs it in the future
		RenderTexture.ReleaseTemporary(newRenderTexture);

		//Finally return that image!
		return image;
	}

}
