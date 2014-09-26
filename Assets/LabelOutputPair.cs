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
		GUI.Box(new Rect(0, 0, width - 40, height), string.Empty);
		var style = new GUIStyle();
		style.fontSize = 30;
		style.normal.textColor = _color;
		GUI.Label (new Rect(5,height/3, 100, 40), string.Format ("{0}:", _label), style);
		GUI.Label (new Rect(300, height/3, 100, 40), _output, style);
		GUI.EndGroup();
	}
}
