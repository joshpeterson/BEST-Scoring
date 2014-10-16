using UnityEngine;

public class LabelOutputPair
{
	private string _label;
	private Color _color;

	private string _output;

	public LabelOutputPair (string label, Color color)
	{
		_label = label;
		_color = color;
	}

	public void SetOutput(string output)
	{
		_output = output;
	}

	public void Draw(int left, int top, int width, int height)
	{
		GUI.BeginGroup(new Rect(left, top, width, height));
		var style = new GUIStyle();
		style.fontSize = 50;
		style.normal.textColor = _color;

		var shadowStyle = new GUIStyle(style);
		shadowStyle.normal.textColor = Color.grey;

		GUI.Label (new Rect(1,height/5+1, 100, 40), string.Format ("{0}:", _label), shadowStyle);
		GUI.Label (new Rect(0,height/5, 100, 40), string.Format ("{0}:", _label), style);
		GUI.Label (new Rect(width/3+1, height*4/7+1, 100, 40), _output, shadowStyle);
		GUI.Label (new Rect(width/3, height*4/7, 100, 40), _output, style);
		GUI.EndGroup();
	}
}
