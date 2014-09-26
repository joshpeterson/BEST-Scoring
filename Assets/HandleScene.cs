using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BESTScoring
{

public class HandleScene : MonoBehaviour
{
	private const int ButtonSpacing = 20;

	private SwitchButtonPair _blade1Buttons;
	private SwitchButtonPair _blade2Buttons;
	private SwitchButtonPair _blade3Buttons;
	private SwitchButtonPair _towerButtons;

	private LabelOutputPair _redTeamScore;
	private LabelOutputPair _blueTeamScore;
		
	void Start ()
	{
		Debug.Log ("Here");

		_blade1Buttons = new SwitchButtonPair("Blade 1", SwitchButtonPairOrientation.Vertical);
		_blade2Buttons = new SwitchButtonPair("Blade 2", SwitchButtonPairOrientation.Vertical);
		_blade3Buttons = new SwitchButtonPair("Blade 3", SwitchButtonPairOrientation.Vertical);
		_towerButtons = new SwitchButtonPair("Tower", SwitchButtonPairOrientation.Horizontal);

		_redTeamScore = new LabelOutputPair("Red team score", Color.red);
		_blueTeamScore = new LabelOutputPair("Blue team score", Color.blue);
	}
	
	void OnGUI()
	{
		var verticalMiddle = Screen.width / 2;

		var buttonWidth = Screen.width/5;
		var buttonHeight = buttonWidth;
		
		var verticalOrientationTotalWidth = buttonWidth + 2 * ButtonSpacing;
		var verticalOrientationTotalHeight = 2 * buttonHeight + 3 * ButtonSpacing;
		var horizontalOrientationTotalWidth = 2 * buttonWidth + 3 * ButtonSpacing;
		var horizontalOrientationTotalHeight = buttonHeight + 2 * ButtonSpacing;

		var bladeButtonsTop = Screen.height / 10;
		var bladeButtons1Left = verticalMiddle - verticalOrientationTotalWidth/2 - verticalOrientationTotalWidth;
		var bladeButtons2Left = bladeButtons1Left + verticalOrientationTotalWidth;
		var bladeButtons3Left = bladeButtons2Left + verticalOrientationTotalWidth;
	
		var towerButtonsTop = bladeButtonsTop + verticalOrientationTotalHeight + 10;
		var towerButtonsLeft = verticalMiddle - horizontalOrientationTotalWidth/2;

		var teamsScoreHeight = horizontalOrientationTotalHeight;
		var teamsScoreWidth = 3 * verticalOrientationTotalWidth;
		var teamsScoreLeft = verticalMiddle - teamsScoreWidth / 2;
		var redTeamScoreTop = towerButtonsTop + horizontalOrientationTotalHeight + 10;
		var blueTeamScoreTop = redTeamScoreTop + teamsScoreHeight + 10;

		var initialNumberOfRedTeamBlades = NumberOfRedTeamBlades();
		var initialNumberOfBlueTeamBlades = NumberOfBlueTeamBlades();
		var initialUseRedTower = UseRedTower();

		_blade1Buttons.Draw(bladeButtons1Left, bladeButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_blade2Buttons.Draw(bladeButtons2Left, bladeButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_blade3Buttons.Draw(bladeButtons3Left, bladeButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_towerButtons.Draw(towerButtonsLeft, towerButtonsTop, buttonWidth, buttonHeight, ButtonSpacing);
		_redTeamScore.Draw(teamsScoreLeft, redTeamScoreTop, teamsScoreWidth, teamsScoreHeight);
		_blueTeamScore.Draw(teamsScoreLeft, blueTeamScoreTop, teamsScoreWidth, teamsScoreHeight);

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
		var blueScore = ComputeScoreChanges.RedTeamScore (finalNumberOfRedTeamBlades, finalNumberOfBlueTeamBlades, finalUseRedTower);
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
