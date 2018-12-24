using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
	public int thrust = 20; // Upwards Force.
	public int moveForward;	// Forward Speed.
	public Rigidbody rb;	// Players Rigidbody

	public bool gameStarted = false;
	public bool gameEnded = false;

    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate()
	{
		ForwardMovement();
	}

	void Update()
    {
		GameStart();
    }

	void GameStart()
	{
		// The Game has not Started.
		if (gameStarted == false)
		{
			// Freeze all Constraints.
			rb.constraints = RigidbodyConstraints.FreezeAll;

			if (Input.GetMouseButtonDown(0))	// If the game has not started and the Mouse Button is pressed.
			{
				gameStarted = true;				// Start the game.					
				Debug.Log("Game Has Started.");
			}
		}

		// The Game has started and its not GameOver.
		else if (gameStarted == true && gameEnded == false)
		{
			if (Input.GetMouseButton(0))								// If the game has started and the mouse button is pressed.
			{
				rb.constraints = RigidbodyConstraints.FreezeRotation;	// Freeze the rotation and UnFreeze the position.
				rb.AddForce(transform.up * thrust);						// Apply the Upwards Force.
			}
		}
	}

	void ForwardMovement()
	{
		if (gameStarted == true)
		{
			rb.AddForce(transform.forward * moveForward);
		}
	}

	void GameOver()
	{
		gameStarted = false;
		gameEnded = true;

		Debug.Log("Game Over!");

	}

	void OnCollisionEnter(Collision col)
	{
		switch (col.gameObject.tag)
		{
			case "Ground":
				GameOver();
				Debug.Log ("You Hit the Ground, You Suck!");
				break;

			case "Obstacle":
				GameOver();
				Debug.Log("You Hit an obstacle. You Suck!");
				break;

			case "Ring":
				// Write code to add points to the score. (Is this case needed here or in OnTriggerEnter????)
				Debug.Log("You Passed through a Ring, Good Job!");
				break;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		switch (col.tag)
		{
			case "Ring":
				// Write code to add points to the score. (Is this case needed here or in OnCollitionEnter????)
				Debug.Log("You passed through the Ring.");
				break;
		}
	}

}
