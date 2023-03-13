using UnityEngine.UI;
using UnityEngine;
using System;


public class RocketEvents : MonoBehaviour
{
	public event Action OnClick;

	private void OnMouseDown()
	{
		if (OnClick != null)
		{
			OnClick();
		}
	}
}