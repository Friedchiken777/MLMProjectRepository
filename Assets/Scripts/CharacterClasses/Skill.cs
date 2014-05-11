public class Skill : ModifiedStat {

	private bool known;

	public Skill(){
		known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}

	public bool Known{
		get{return known;}
		set{ known = value;}
	}
}

public enum SkillName{
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
	