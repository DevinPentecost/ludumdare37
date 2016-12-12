using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeHelper : MonoBehaviour {

	//Fade directions
	public static readonly int FADE_IN = -1;
	public static readonly int FADE_OUT = 1;

	//The texture to fade with
	public Texture2D fadeOutTexture = new Texture2D(1, 1, TextureFormat.ARGB32, false);

	//The speed we fade with
	private float fadeSpeed = .1f;

	//And where do we draw the fade? Hopefully on top.
	private int drawDepth = -1000;

	//How visible is it
	private float currentAlpha = 1;
	private int fadeDirection = -1;

	//When we are created...
	void Start()
	{
		//We should be black for the texture
		this.fadeOutTexture.SetPixel(0, 0, Color.black);
		this.fadeOutTexture.Apply();
	}

	//When the GUI renders
	void OnGUI()
	{
		//Which way are we fading?
		this.currentAlpha += fadeDirection * fadeSpeed;

		//Cap the alpha
		this.currentAlpha = Mathf.Clamp01(this.currentAlpha);

		//And then set the color for the fade (Just alpha)
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, this.currentAlpha);

		//Draw it at the appropriate depth
		GUI.depth = this.drawDepth;

		//Now draw the texture
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), this.fadeOutTexture);
	}

	//Set the fade direction
	public float BeginFade(int direction)
	{
		//We set the direction
		this.fadeDirection = direction;

		//Tell us how fast we're going
		return fadeSpeed;
	}

	//Scene delegation (Replaces OnLevelWasLoaded)
	void OnEnable()
	{
		//Add the delegate
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}
	void OnDisable()
	{
		//Remove the delegate
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
	//When the level is loaded delegate
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		//Always fade in a level
		BeginFade(FadeHelper.FADE_IN);
	}
}
