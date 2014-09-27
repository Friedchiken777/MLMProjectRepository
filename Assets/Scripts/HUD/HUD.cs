using UnityEngine;
using System.Collections;
using System.Collections.Generic; //used for Lists

public class HUD : MonoBehaviour
{
	/*public int twilightMaxHP,
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

	public Texture portraitGradientBase;*/

	public Color twilightPortraitGradientColor,
			pinkiePiePortraitGradientColor,
			applejackPortraitGradientColor,
			rainbowDashPortraitGradientColor,
			rarityPortraitGradientColor,
			fluttershyPortraitGradientColor;

	private List<GameObject> characterOrder;

	public GameObject[] gradientOrder,
						portraitOrder,
						healthbarOrder,
						manabarOrder;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		characterOrder = GameObject.Find("_BattleManager").GetComponent<BattleManager>().fieldPlayers;
		string tempString1;
		string tempString2;
		bool temp;
		int j;

		for (int i = 0; i < characterOrder.Count; i++)
		{
			tempString1 = characterOrder[i].GetComponent<BaseEntity>().charName;

			temp = false;
			j = 0;

			while (! temp)
			{
				tempString2 = gradientOrder[j].GetComponent<ElementEntity>().charName;

				if (tempString1 == tempString2)
				{
					if (i <= ((characterOrder.Count / 2) - 1))
					{
						gradientOrder[j].transform.localPosition = new Vector3(-216.0f, 375.0f - (59.0f * i), 0.0f);
					}
					else
					{
						gradientOrder[j].transform.localPosition = new Vector3(-216.0f, -257.0f - (59.0f * (i - (characterOrder.Count / 2))), 0.0f);
					}

					temp = true;
				}

				j++;
			}

			temp = false;
			j = 0;

			while (! temp)
			{
				tempString2 = portraitOrder[j].GetComponent<ElementEntity>().charName;

				if (tempString1 == tempString2)
				{
					if (i <= ((characterOrder.Count / 2) - 1))
					{
						portraitOrder[j].transform.localPosition = new Vector3(-589.0f, 375.0f - (59.0f * i), 0.0f);
					}
					else
					{
						portraitOrder[j].transform.localPosition = new Vector3(-589.0f, -257.0f - (59.0f * (i - (characterOrder.Count / 2))), 0.0f);
					}

					temp = true;
				}

				j++;
			}

			temp = false;
			j = 0;

			while (! temp)
			{
				tempString2 = healthbarOrder[j].GetComponent<ElementEntity>().charName;

				if (tempString1 == tempString2)
				{
					if (i <= ((characterOrder.Count / 2) - 1))
					{
						healthbarOrder[j].transform.localPosition = new Vector3(-495.0f, 375.0f - (59.0f * i), 0.0f);
					}
					else
					{
						healthbarOrder[j].transform.localPosition = new Vector3(-495.0f, -257.0f - (59.0f * (i - (characterOrder.Count / 2))), 0.0f);
					}

					temp = true;
				}

				j++;
			}

			temp = false;
			j = 0;

			while (! temp)
			{
				tempString2 = manabarOrder[j].GetComponent<ElementEntity>().charName;

				if (tempString1 == tempString2)
				{
					if (i <= ((characterOrder.Count / 2) - 1))
					{
						manabarOrder[j].transform.localPosition = new Vector3(-275.0f, 375.0f - (59.0f * i), 0.0f);
					}
					else
					{
						manabarOrder[j].transform.localPosition = new Vector3(-275.0f, -257.0f - (59.0f * (i - (characterOrder.Count / 2))), 0.0f);
					}

					temp = true;
				}

				j++;
			}
		}
	}
}
