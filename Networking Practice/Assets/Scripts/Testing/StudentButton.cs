using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class StudentButton : MonoBehaviour 
{
	public Account ThisAccount; // The account associated with this object
	public Button ThisButton; // The button associated with this object
	string NameOnButton; // The name displayed on the button
	public StudentButton( Account account, Button button )
	{
		ThisAccount = account;
		ThisButton = button;
		NameOnButton = account.FirstName + " " + account.LastName;
	}
}
