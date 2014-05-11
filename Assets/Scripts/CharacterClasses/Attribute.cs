public class Attribute : BaseStat{

	public Attribute(){
		ExpToLevel = 50;
		LevelModifier = 1.05f;
	}

}

public enum AttributeName{
	Power,
	Agility,
	Speed,
	Accuracy,
	Magic,
	Physical_Defense,
	Fire_Defense,
	Lightning_Defense,
	Ice_Defense,
	Earth_Defense
}
