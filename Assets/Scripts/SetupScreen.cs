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

	public Image Color1;
	public Image Color2;

	public int width = 3 ;
	public int height = 3;
	public bool allowDiagonals = true;
	public bool allowFreeMode = true;

	void Start () {
		widthText.text = width.ToString();
		heightText.text = height.ToString();
		toggleAllowFreeMode();
		toggleAllowDiagonals();
	}

	public void changeWidth  (int increment) {
		width += increment;
		width = Mathf.Clamp(width, 2,int.MaxValue);
		widthText.text = width.ToString();
	}

    public void changeHeight(int increment)
    {
        height += increment;
        height = Mathf.Clamp(height, 2, int.MaxValue);
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

	public void getRandomColor () {
		float randomH = Random.Range (0f, 1f);
		float saturation = 1;
		float randomV = Random.Range (.5f,1f);
		Color primary =  Color.HSVToRGB(randomH,saturation,randomV);
		Color complementary = Color.HSVToRGB( (randomH + .5f) % 1,saturation,randomV);

		Color1.color = primary;
		Color2.color = complementary;
	}
}
