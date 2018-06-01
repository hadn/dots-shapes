using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayArea : MonoBehaviour {

	public float Area {
		set ; get;
	}

	public void OnMouseDown()
	{
		Debug.Log ("Area is " + Area);
	}
}
