using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// http://wiki.unity3d.com/index.php/Toolbox
/// </summary>
public class ToolboxSingleton : Singleton<ToolboxSingleton> {
	protected ToolboxSingleton() { } // guarantee this will be always a singleton only - can't use the constructor!

	//Setup a bunch of global variables

	#region GLOBALS

	#region DEBUG
	//Are we on DEBUG?
	public static readonly bool DEBUG = false;
	public static readonly bool NO_GUI = false;
	#endregion

	#region SCENES
	//Various strings for scenes
	public static readonly string SCENE_MAIN = "room";
	#endregion

	#region TAGS
	public static readonly string TAG_PLAYER = "Player";
	public static readonly string TAG_GAMECONTROLLER = "GameController";
	public static readonly string TAG_LADDER = "ladder";
	public static readonly string TAG_KEY = "key";
	public static readonly string TAG_BOUND = "bound";
	public static readonly string TAG_ROOM = "room";
	public static readonly string TAG_FADE_IMAGE = "FadeImage";
	#endregion

	#region ENUMS
	public static readonly EnumHelper.RoomEnum START_ROOM = EnumHelper.RoomEnum.ROOM2;
	#endregion

	#region CONSTANTS
	//Level construction constants
	public readonly float TOWER_WIDTH = 12;
	public readonly float OBSTACLE_REGION_WIDTH = 4;
	//Control buttons
	public static readonly string KEY_LEFT="a";
	public static readonly string KEY_RIGHT="d";
	public static readonly string KEY_UP="w";
	public static readonly string KEY_DOWN="s";
	public static readonly string KEY_SPACE="space";
	public static readonly string KEY_ESCAPE = "escape";

	//Game object names
	public static readonly string NAME_CENTER_POINT = "CenterPoint";
	public static readonly string NAME_SPOOKY_PARTICLES = "SpookyGhostParticles";
	public static readonly string NAME_SPOOKY_SPRITE = "SpookyGhostSprite";


	//Camera constants
	public readonly float MIN_CAMERA_POSISITON = 3;
	public readonly float CAMERA_DAMPING_FACTOR = 8;
	#endregion


	#endregion

	//Instance variables
	#region INSTANCE VARIABLES
	//The fade helper
	FadeHelper fadeHelper;
	#endregion
	void Awake()
	{
		// Your initialization code here

		//Create a fade helper
		ToolboxSingleton.RegisterComponent<FadeHelper>();
	}

	// (optional) allow runtime registration of global objects
	static public T RegisterComponent<T>() where T : Component
	{
		return Instance.GetOrAddComponent<T>();
	}

	#region METHODS


	#region HELPER FUNCTIONS
	public PlayerController FindPlayerController()
	{
		//Get the playercontroller by tag and return it
		return GameObject.FindGameObjectWithTag(ToolboxSingleton.TAG_PLAYER).GetComponent<PlayerController>();
	}
/*	public GameController FindGameController()
	{
		//Get the Game Controller by tag and return it
		return GameObject.FindGameObjectWithTag(ToolboxSingleton.TAG_GAMECONTROLLER).GetComponent<GameController>();
	}*/


	#endregion

	#region GAME FLOW
	//When the user wants to quit
	public void QuitGame()
	{
		//Just quit
		Application.Quit();
	}

	//Fade to black
	public IEnumerator FadeToBlack()
	{
		//Get the fade handler
		FadeHelper fadeHelper = this.GetComponent<FadeHelper>();

		//Figure out how long to wait
		float waitTime = fadeHelper.BeginFade(FadeHelper.FADE_OUT);

		//Now wait that long
		yield return new WaitForSeconds(waitTime);
	}

	//Handle fading out
	private IEnumerator FadeOut(string targetScene)
	{
		//Wait for the fade to black
		yield return this.FadeToBlack();

		//Now load the scene
		SceneManager.LoadScene(targetScene);
	}

	public void FadeToScene(string targetScene)
	{
		//Start the coroutine
		StartCoroutine(this.FadeOut(targetScene));
	}

	//When we quit
	void OnApplicationQuit()
	{
		//Do something
	}
	#endregion

	#endregion
}

[System.Serializable]
public class Language
{
	public string current;
	public string lastLang;
}