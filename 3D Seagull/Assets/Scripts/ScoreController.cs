using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
	public Text scoreText;
	public PlayerController _playerController;

	float score = 0f;
	float pointsIncreasedPerSecond;

	void Start()
	{
		score = 0f;
		pointsIncreasedPerSecond = 1f;
	}

	void Update()
	{
		if(_playerController.gameStarted == true)
		{
			score += pointsIncreasedPerSecond * Time.deltaTime;		// Increase the score.
			scoreText.text = score.ToString("0");					// Update the Score Text.
		}

		else if (_playerController.gameStarted == false)
		{
			score = 0f;		// Set the score to "0" if the game hasn't started yet.
		}
	}
}
