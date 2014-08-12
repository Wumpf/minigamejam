using UnityEngine;
using System.Collections;

public class Singelton<T> : MonoBehaviour where T :MonoBehaviour
{
	public static T instance {
		get;
		private set;

	}

	void OnEnable()
	{
		if (instance != null)
		{
			Destroy (this.gameObject);
			return;
		}
		instance = this as T;
	}
}

