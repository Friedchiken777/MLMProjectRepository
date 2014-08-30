using UnityEngine;
using System.Collections;
using System.Collections.Generic; //used for Lists

public class HUD : MonoBehaviour
{
	public int twilightMaxHP,
			pinkiePieMaxHP,
			applejackMaxHP,
			rainbowDashMaxHP,
			rarityMaxHP,
			fluttershyMaxHP;

	public int twilightMaxMP,
			pinkiePieMaxMP,
			applejackMaxMP,
			rainbowDashMaxMP,
			rarityMaxMP,
			fluttershyMaxMP;

	public int twilightCurrentHP,
			pinkiePieCurrentHP,
			applejackCurrentHP,
			rainbowDashCurrentHP,
			rarityCurrentHP,
			fluttershyCurrentHP;

	public int twilightCurrentMP,
			pinkiePieCurrentMP,
			applejackCurrentMP,
			rainbowDashCurrentMP,
			rarityCurrentMP,
			fluttershyCurrentMP;

	public Texture twilightPortrait,
			pinkiePiePortrait,
			applejackPortrait,
			rainbowDashPortrait,
			rarityPortrait,
			fluttershyPortrait;

	public Texture portraitGradientBase;

	public Color twilightPortraitGradientColor,
			pinkiePiePortraitGradientColor,
			applejackPortraitGradientColor,
			rainbowDashPortraitGradientColor,
			rarityPortraitGradientColor,
			fluttershyPortraitGradientColor;

	public Texture healthBarOutline,
			manaBarOutline;

	public Texture healthBarFill,
			manaBarFill;

	public int maxCharacters;

	public List<Texture> characterGradientTop,
			characterGradientBottom;

	public List<Texture> characterHPOutlineTop,
			characterHPOutlineBottom;

	public List<Texture> characterMPOutlineTop,
			characterMPOutlineBottom;

	public List<Texture> characterHPFillTop,
			characterHPFillBottom;
	
	public List<Texture> characterMPFillTop,
			characterMPFillBottom;

	// Use this for initialization
	void Start ()
	{
		maxCharacters = gameObject.GetComponent<BattleManager>().playerSpawns.Length;

		characterGradientTop = new List<Texture>();
		characterGradientBottom = new List<Texture>();

		characterHPOutlineTop = new List<Texture>();
		characterHPOutlineBottom = new List<Texture>();

		characterMPOutlineTop = new List<Texture>();
		characterMPOutlineBottom = new List<Texture>();

		characterHPFillTop = new List<Texture>();
		characterHPFillBottom = new List<Texture>();
		
		characterMPFillTop = new List<Texture>();
		characterMPFillBottom = new List<Texture>();

		AddHUD();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnGUI()
	{
		DrawHUD();
	}

	void AddHUD()
	{
		for (int i = 0; i < maxCharacters / 2; i++)
		{
			characterGradientTop.Add (portraitGradientBase);
			characterGradientBottom.Add (portraitGradientBase);

			characterHPOutlineTop.Add (healthBarOutline);
			characterHPOutlineBottom.Add (healthBarOutline);

			characterMPOutlineTop.Add (manaBarOutline);
			characterMPOutlineBottom.Add (manaBarOutline);

			characterHPFillTop.Add (healthBarFill);
			characterHPFillBottom.Add (healthBarFill);
			
			characterMPFillTop.Add (manaBarFill);
			characterMPFillBottom.Add (manaBarFill);
		}
	}

	void DrawHUD()
	{
		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(0, characterGradientTop[i].height * i, characterGradientTop[i].width, characterGradientTop[i].height), characterGradientTop[i]);
		}

		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(0, Screen.height - characterGradientBottom[i].height + (characterGradientBottom[i].height * -i), characterGradientBottom[i].width, characterGradientBottom[i].height), characterGradientBottom[i]);
		}

		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterHPOutlineTop[i]);
		}
		
		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterHPOutlineBottom[i]);
		}

		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterMPOutlineTop[i]);
		}
		
		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterMPOutlineBottom[i]);
		}

		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterHPFillTop[i]);
		}
		
		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterHPFillBottom[i]);
		}
		
		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterMPFillTop[i]);
		}
		
		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(), characterMPFillBottom[i]);
		}
	}
}
