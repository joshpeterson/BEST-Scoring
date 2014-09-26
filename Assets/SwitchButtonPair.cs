using UnityEngine;

namespace BESTScoring
{
	public enum SwitchButtonPairOrientation
	{
		Vertical,
		Horizontal
	}

	public class SwitchButtonPair
	{
		private string _label;
		private SwitchButtonPairOrientation _orientation;

		private bool _firstSelected = true;
		
		public SwitchButtonPair (string label, SwitchButtonPairOrientation orientation)
		{
			_label = label;
			_orientation = orientation;
		}

		public bool FirstSelected
		{
			get { return _firstSelected; }
		}

		public void Draw(int left, int top, int width, int height, int buttonSpacing)
		{
			var groupWidth = width + 2 * buttonSpacing;
			var groupHeight = 2 * height + 3* buttonSpacing;
			if (_orientation == SwitchButtonPairOrientation.Horizontal)
			{
				groupWidth = 2 * width + 3 * buttonSpacing;
				groupHeight = height + 2 * buttonSpacing;
			}

			GUI.BeginGroup(new Rect(left, top, groupWidth, groupHeight));

			var initialColor = GUI.color;

			if (_firstSelected)
				GUI.color = Color.red;
			else
				GUI.color = Color.gray;

			if (GUI.Button(new Rect(0, 0, width, height), _label))
				_firstSelected = true;

			if (!_firstSelected)
				GUI.color = Color.blue;
			else
				GUI.color = Color.gray;

			var secondButtonLeft = 0;
			var secondButtonTop = height + buttonSpacing;
			if (_orientation == SwitchButtonPairOrientation.Horizontal)
			{
				secondButtonLeft = 0 + width + buttonSpacing;
				secondButtonTop = 0;
			}

			if (GUI.Button(new Rect(secondButtonLeft, secondButtonTop, width, height), _label))
				_firstSelected = false;

			GUI.color = initialColor;

			GUI.EndGroup();
		}
	}
}

