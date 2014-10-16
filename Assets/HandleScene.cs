using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BESTScoring
{

public class HandleScene : MonoBehaviour
{
	public GUIStyle buttonOneStyle;
	public GUIStyle buttonTwoStyle;
	public GUIStyle buttonThreeStyle;
	public GUIStyle buttonFourStyle;
	public GUIStyle buttonFiveStyle;

	private const int ButtonSpacing = 20;

	private SwitchButtonPair _blade1Buttons;
	private SwitchButtonPair _blade2Buttons;
	private SwitchButtonPair _blade3Buttons;
	private SwitchButtonPair _towerButtons;

	private LabelOutputPair _redTeamScore;
	private LabelOutputPair _blueTeamScore;

	void Start ()
	{
		_blade1Buttons = new SwitchButtonPair("Blade 1", SwitchButtonPairOrientation.Vertical, buttonOneStyle, buttonOneStyle);
		_blade2Buttons = new SwitchButtonPair("Blade 2", SwitchButtonPairOrientation.Vertical, buttonTwoStyle, buttonTwoStyle);
		_blade3Buttons = new SwitchButtonPair("Blade 3", SwitchButtonPairOrientation.Vertical, buttonThreeStyle, buttonThreeStyle);
		_towerButtons = new SwitchButtonPair("TEST", SwitchButtonPairOrientation.Horizontal, buttonFourStyle, buttonFiveStyle);

		_redTeamScore = new LabelOutputPair("Left team score", Color.red);
		_blueTeamScore = new LabelOutputPair("Right team score", Color.blue);
	}
	
	void OnGUI()
	{
		var verticalMiddle = Screen.width / 2;

		var buttonWidth = Screen.width/5;
		var buttonHeight = buttonWidth/3;
		
		var verticalOrientationTotalWidth = buttonWidth + 2 * ButtonSpacing;
		var verticalOrientationTotalHeight = 2 * buttonHeight + 3 * ButtonSpacing;
		var horizontalOrientationTotalWidth = 2 * buttonWidth + 3 * ButtonSpacing;
		var horizontalOrientationTotalHeight = buttonHeight + 2 * ButtonSpacing;

		var bladeButtonsTop = Screen.height / 15;
		var bladeButtons1Left = verticalMiddle - verticalOrientationTotalWidth/2 - verticalOrientationTotalWidth;
		var bladeButtons2Left = bladeButtons1Left + verticalOrientationTotalWidth;
		var bladeButtons3Left = bladeButtons2Left + verticalOrientationTotalWidth;
	
		var towerButtonsTop = bladeButtonsTop + verticalOrientationTotalHeight - 20;  //location of top of tower buttons
		var towerButtonsLeft = verticalMiddle - horizontalOrientationTotalWidth/2;

		var teamsScoreHeight = horizontalOrientationTotalHeight * 3 / 2;
		var teamsScoreWidth = 2 * verticalOrientationTotalWidth;
		var redTeamsScoreLeft = verticalMiddle - teamsScoreWidth * 1 + 18;
		var blueTeamScoreLeft = verticalMiddle + 100; //+ teamsScoreWidth * 1/2;
		var redTeamScoreTop = towerButtonsTop + horizontalOrientationTotalHeight - 10;
		var blueTeamScoreTop = towerButtonsTop + horizontalOrientationTotalHeight - 10;

		var initialNumberOfRedTeamBlades = NumberOfRedTeamBlades();
		var initialNumberOfBlueTeamBlades = NumberOfBlueTeamBlades();
		var initialUseRedTower = UseRedTower();

		Texture2D background = Resources.Load ("Trionix Green", typeof(Texture2D)) as Texture2D;
		GUI.DrawTexture (new Rect (Screen.width/2 - Screen.height/2, 0, Screen.height, Screen.height), background);

		Texture2D QR = Resources.Load ("Trionix QR", typeof(Texture2D)) as Texture2D;
		GUI.DrawTexture (new Rect (Screen.width * 5/6, Screen.height - Screen.width * 1/6, Screen.width/6, Screen.width/6), QR);

		Texture2D logo = Resources.Load ("Best", typeof(Texture2D)) as Texture2D;
		GUI.DrawTexture (new Rect (960, 200, 250, 200), logo);

		GUIStyle style = new GUIStyle ();
		style.fontSize = 25;
		style.normal.textColor = Color.white;

		GUIStyle shadowStyle = new GUIStyle ();
		shadowStyle.fontSize = 25;
		shadowStyle.normal.textColor = Color.grey;

		GUI.Label (new Rect (21, 571, 300, 200), "Instructions:  Select Co-Op or Single,\n then change the number of blades for\n each team, and check out the scores!", shadowStyle);
		GUI.Label (new Rect (20, 570, 300, 200), "Instructions:  Select Co-Op or Single,\n then change the number of blades for\n each team, and check out the scores!", style);

		style.fontSize = 40;
		GUI.Label (new Rect (31, 261, 300, 200), "Team #602", style);
		GUI.Label (new Rect (30, 260, 300, 200), "Team #602", style);

		//                   x coord,             ycoord,         width,         height
		_blade1Buttons.Draw(bladeButtons1Left, bladeButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_blade2Buttons.Draw(bladeButtons2Left, bladeButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_blade3Buttons.Draw(bladeButtons3Left, bladeButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_towerButtons.Draw(towerButtonsLeft, towerButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_redTeamScore.Draw(redTeamsScoreLeft, redTeamScoreTop, teamsScoreWidth, teamsScoreHeight);
		_blueTeamScore.Draw(blueTeamScoreLeft, blueTeamScoreTop, teamsScoreWidth, teamsScoreHeight);

		var finalNumberOfRedTeamBlades = NumberOfRedTeamBlades();
		var finalNumberOfBlueTeamBlades = NumberOfBlueTeamBlades();
		var finalUseRedTower = UseRedTower();
		
		if (SomethingChanged (initialNumberOfRedTeamBlades, initialNumberOfBlueTeamBlades, initialUseRedTower, finalNumberOfRedTeamBlades, finalNumberOfBlueTeamBlades, finalUseRedTower))
		{
			CalculateNewScores (finalNumberOfRedTeamBlades, finalNumberOfBlueTeamBlades, finalUseRedTower);
		}
	}

	static bool SomethingChanged(int initialNumberOfRedTeamBlades, int initialNumberOfBlueTeamBlades, bool initialUseRedTower, int finalNumberOfRedTeamBlades, int finalNumberOfBlueTeamBlades, bool finalUseRedTower)
	{
		return initialNumberOfRedTeamBlades != finalNumberOfRedTeamBlades || initialNumberOfBlueTeamBlades != finalNumberOfBlueTeamBlades || initialUseRedTower != finalUseRedTower;
	}

	private void CalculateNewScores(int finalNumberOfRedTeamBlades, int finalNumberOfBlueTeamBlades, bool finalUseRedTower)
	{
		var redScore = ComputeScoreChanges.RedTeamScore (finalNumberOfRedTeamBlades, finalNumberOfBlueTeamBlades, finalUseRedTower);
		var blueScore = ComputeScoreChanges.BlueTeamScore (finalNumberOfRedTeamBlades, finalNumberOfBlueTeamBlades, finalUseRedTower);
		_redTeamScore.SetOutput (redScore.ToString ());
		_blueTeamScore.SetOutput (blueScore.ToString ());
	}
	
	private int NumberOfRedTeamBlades()
	{
		int numberOfRedTeamBlades = 0;
		if (_blade1Buttons.FirstSelected)
			numberOfRedTeamBlades++;
		if (_blade2Buttons.FirstSelected)
			numberOfRedTeamBlades++;
		if (_blade3Buttons.FirstSelected)
			numberOfRedTeamBlades++;

		return numberOfRedTeamBlades;
	}

	private int NumberOfBlueTeamBlades()
	{
		int numberOfBlueTeamBlades = 0;
		if (!_blade1Buttons.FirstSelected)
			numberOfBlueTeamBlades++;
		if (!_blade2Buttons.FirstSelected)
			numberOfBlueTeamBlades++;
		if (!_blade3Buttons.FirstSelected)
			numberOfBlueTeamBlades++;
		
		return numberOfBlueTeamBlades;
	}
	
	private bool UseRedTower()
	{
		return _towerButtons.FirstSelected;
	}
}

}

