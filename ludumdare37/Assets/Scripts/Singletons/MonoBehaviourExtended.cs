﻿using UnityEngine;

static public class MethodExtensionForMonoBehaviourTransform
{
	/// <summary>
	/// http://wiki.unity3d.com/index.php/Toolbox
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public T GetOrAddComponent<T>(this Component child) where T : Component
	{
		T result = child.GetComponent<T>();
		if (result == null)
		{
			result = child.gameObject.AddComponent<T>();
		}
		return result;
	}
}