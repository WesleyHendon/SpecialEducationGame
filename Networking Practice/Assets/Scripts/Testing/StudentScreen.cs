using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class StudentScreen : MonoBehaviour 
{
	PersistantScript ps;
	public Text loggedInAs;

	void Start()
	{
		ps = GameObject.Find ("Persistant").GetComponent<PersistantScript> ();
	}

	void Update()
	{
		loggedInAs.text = "Logged in as: " + ps.loggedIn.Username;
	}

	public void Logout()
	{
		ps.LogoutButton ();
	}
}
