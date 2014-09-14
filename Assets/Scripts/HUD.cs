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
			GUI.DrawTexture(new Rect(0,
			                         characterGradientTop[i].height * i,
			                         characterGradientTop[i].width,
			                         characterGradientTop[i].height),
			                characterGradientTop[i]);
			/*GUI.DrawTexture(new Rect(0,
			                         characterGradientTop[i].height + ((characterGradientTop[i].height - characterHPFillTop[i].height) / 2) * i,
			                         characterHPFillTop[i].width,
			                         characterHPFillTop[i].height),
			                characterHPFillTop[i]);
			GUI.DrawTexture(new Rect(0,
			                         characterGradientTop[i].height + ((characterGradientTop[i].height - characterMPFillTop[i].height) / 2) * i,
			                         characterMPFillTop[i].width,
			                         characterMPFillTop[i].height),
			                characterMPFillTop[i]);*/
			GUI.DrawTexture(new Rect(0,
			                         (characterGradientTop[i].height + ((characterGradientTop[i].height - characterHPOutlineTop[i].height) / 2)) * i,
			                         characterHPOutlineTop[i].width,
			                         characterHPOutlineTop[i].height),
			                characterHPOutlineTop[i]);
			GUI.DrawTexture(new Rect(225,
			                         (characterGradientTop[i].height + ((characterGradientTop[i].height - characterMPOutlineTop[i].height) / 2)) * i,
			                         characterMPOutlineTop[i].width,
			                         characterMPOutlineTop[i].height),
			                characterMPOutlineTop[i]);
		}

		for (int i = 0; i < maxCharacters / 2; i++)
		{
			GUI.DrawTexture(new Rect(0, Screen.height - characterGradientBottom[i].height + (characterGradientBottom[i].height * -i), characterGradientBottom[i].width, characterGradientBottom[i].height), characterGradientBottom[i]);
			GUI.DrawTexture(new Rect(0, Screen.height - characterHPFillBottom[i].height + (characterHPFillBottom[i].height * -i), characterHPFillBottom[i].width, characterHPFillBottom[i].height), characterHPFillBottom[i]);
			GUI.DrawTexture(new Rect(0, Screen.height - characterMPFillBottom[i].height + (characterMPFillBottom[i].height * -i), characterMPFillBottom[i].width, characterMPFillBottom[i].height), characterMPFillBottom[i]);
			GUI.DrawTexture(new Rect(0, Screen.height - characterHPOutlineBottom[i].height + (characterHPOutlineBottom[i].height * -i), characterHPOutlineBottom[i].width, characterHPOutlineBottom[i].height), characterHPOutlineBottom[i]);
			GUI.DrawTexture(new Rect(0, Screen.height - characterMPOutlineBottom[i].height + (characterMPOutlineBottom[i].height * -i), characterMPOutlineBottom[i].width, characterMPOutlineBottom[i].height), characterMPOutlineBottom[i]);
		}
	}
}
