using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
	public GameObject _gamePanel;
	public Text[] _nums;
	public string[] _answer;

	void Awake()
	{
		_gamePanel.SetActive(false);
	}

	public void OnResultPanel()
	{
		int i = 0;

		foreach (Text num in _nums)
		{
			if (num != null && _answer != null)
			{
				num.text = _answer[i];
			}
			i++;
		}
	}

	public void OnClickExit()
	{
		_gamePanel.SetActive(false);
	}
}

