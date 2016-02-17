using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class PersistantScript : MonoBehaviour 
{
	public Account loggedIn;
	public List<Account> AccountDatabase = new List<Account> ();

	Button logoutButton;

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Update()
	{
		if (SceneManager.GetActiveScene().name != "Menu") // Reload main menu if there is nobody logged in.
		{
			if (loggedIn == null)
			{
				SceneManager.LoadScene ("Menu");
			}
		}
	}

	public void LogoutButton()
	{
		loggedIn = null;
	}
}
