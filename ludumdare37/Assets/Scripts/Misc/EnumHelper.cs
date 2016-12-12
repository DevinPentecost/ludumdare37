using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumHelper {
	public enum KeyEnum {
		NONE,
		KEY0,
		KEY1,
		KEY2,
	}

	public static Dictionary<KeyEnum, Color> KeyColorMap = new Dictionary<KeyEnum, Color>()
	{
		{KeyEnum.NONE, new Color(1, 1, 1)},
		{KeyEnum.KEY0, new Color(1, 0.45f, 0.4f)},
		{KeyEnum.KEY1, new Color(0.5f, 1, 0.6f)},
		{KeyEnum.KEY2, new Color(0.5f, 0.6f, 1)},
	};

	public enum RoomEnum {
		ROOM0,
		ROOM1,
		ROOM2,
		ROOM3,
		ROOM4,
	}
}
