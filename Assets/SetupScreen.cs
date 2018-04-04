using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupScreen : MonoBehaviour {
	public Text widthText;
	public Text heightText;
	public GameObject diagonalsToggle;
	public GameObject freeModeToggle;
	public GameObject diagonalsGameObject;

	private int width = 3 ;
	private int height = 3;
	private bool allowDiagonals = true;
	private bool allowFreeMode = true;

	void Start () {
		widthText.text = width.ToString();
		heightText.text = height.ToString();
		toggleAllowFreeMode();
		toggleAllowDiagonals();
	}

	public void changeWidth  (int increment) {
		width += increment;
		width = Mathf.Clamp(width, 1,int.MaxValue);
		widthText.text = width.ToString();
	}

    public void changeHeight(int increment)
    {
        height += increment;
        height = Mathf.Clamp(height, 1, int.MaxValue);
        heightText.text = height.ToString();
    }

	public void toggleAllowDiagonals () {
		allowDiagonals = !allowDiagonals;
		diagonalsToggle.SetActive(allowDiagonals);
	}

	public void toggleAllowFreeMode () {
		allowFreeMode = !allowFreeMode;
		freeModeToggle.SetActive(allowFreeMode);
		diagonalsGameObject.SetActive(!allowFreeMode);
	}

	public void StartGame () {
		GameManager.Instance.startANewGame(null,null,
			new Vector2Int(height,width),allowDiagonals,allowFreeMode);
		gameObject.SetActive(false);
	}
}
