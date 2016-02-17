using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour 
{
	public List<Account> allAccounts = new List<Account>();
	public Image CreateNewAccountPanel;
	public Image LoginPanel;
	public Text AccountCreationErrorText;
	public Text LoginErrorText;
	public InputField AC_usernameInput;
	public InputField AC_passwordInput;
	public Dropdown AC_accounttypeInput;
	public InputField AC_firstNameInput;
	public InputField AC_lastNameInput;
	public Dropdown AC_gradeInput;
	public InputField LOGIN_usernameInput;
	public InputField LOGIN_passwordInput;
	string path = "Accounts.txt";

	void Start()
	{
		ReadAccountFile ();
		print(Application.persistentDataPath);
		if (GameObject.Find("Persistant") == null) // If the persistant object does not exist, make it
		{
			GameObject Persistant = new GameObject ("Persistant");
			Persistant.AddComponent<PersistantScript> (); // This script allows it to persist.
		}
		CreateNewAccountPanel.gameObject.SetActive (false);
		LoginPanel.gameObject.SetActive (true);
		AccountCreationErrorText.gameObject.SetActive (false);
		LoginErrorText.gameObject.SetActive (false);
		CreateFile ();
	}

	public void CreateFile()
	{
		if (File.Exists (path))
		{
			return; // If it already exists, do nothing.
		} 
		else 
		{
			File.CreateText (path).Dispose (); // Create a file called Accounts.txt
		}
	}

	public void ReadAccountFile()
	{
		print ("Reading file");
		if (File.Exists (path))
		{
			allAccounts.Clear ();
			using (StreamReader AccountFile = new StreamReader(path))
			{
				List<string> lines = new List<string>();
				string line;
				line = AccountFile.ReadLine();
				while (line != null)
				{
					lines.Add(line);
					line = AccountFile.ReadLine();
				}
				if (lines.Count != 0)
				{
					FormatData(lines);
				}
				AccountFile.Close ();
			}
		}
	}

	public void FormatData(List<string> info) // Reads each line of the file and looks for the " , " which signifies
	{                                         // the split between account fields.
		print ("Formatting data read from file");
		if (info[0] != "" && info.Count != 0)
		{
			for (int i = 0; i < info.Count; i++)
			{
				char[] splits = { ',' };
				string[] accountArray = info [i].Split (splits);
				string username = accountArray[0];
				string password = accountArray[1];
				Account.AccountType? type = null;
				int Type = 0;
				int.TryParse (accountArray [2], out Type);
				if (Type == 0)
					type = null;
				else if (Type == 1)
					type = Account.AccountType.Teacher;
				else if (Type == 2)
					type = Account.AccountType.Student;
				allAccounts.Add (new Account (username, password, type,"","",0));
			}
		}
	}

	public void WriteAccountToFile(Account account) // Writes a new account to the file, all on one line in this format:
	{                                               // username,password,int  where int is the account type
		print ("Writing account to file");
		if (File.Exists(path))
		{
			using (StreamWriter AccountFile = new StreamWriter(path,true))
			{
				int type = 0;
				if (account.AType == Account.AccountType.Teacher)
				{
					type = 1;
				}
				if (account.AType == Account.AccountType.Student)
				{
					type = 2;
				}
				string accountString = account.Username + "," + account.Password + "," + type.ToString();
				print ("Account saved to file: " + accountString);
				AccountFile.WriteLine (accountString);
				AccountFile.Close ();
			}
		}
	}

	public void CreateNewAccountButton() // Called from main menu script
	{
		CreateNewAccountPanel.gameObject.SetActive (!CreateNewAccountPanel.gameObject.activeSelf);
		LoginPanel.gameObject.SetActive (!LoginPanel.gameObject.activeSelf);
		LoginErrorText.gameObject.SetActive (false);
		AccountCreationErrorText.gameObject.SetActive (false);
		AC_accounttypeInput.value = 0;
		AC_gradeInput.value = 0;
		AC_usernameInput.text = "";
		AC_passwordInput.text = "";
		AC_firstNameInput.text = "";
		AC_lastNameInput.text = "";
		LOGIN_usernameInput.text = "";
		LOGIN_passwordInput.text = "";
		LoginErrorText.text = "";
		AccountCreationErrorText.text = "";
	}

	public void FinalizeAccountCreation() // Button Callback, checks for all fields entered
	{
		AccountCreationErrorText.gameObject.SetActive (false); // Make sure the error text isn't showing by default
		ReadAccountFile();
		Account newAccount; // The temp account
		bool err = false;
		string errmessage = "";
		string username = AC_usernameInput.text; // Required field
		string password = AC_passwordInput.text; // Required field
		string firstname = null; // Non required field
		string lastname = null; // Non required field
		if (AC_firstNameInput.text != "" || AC_firstNameInput != null) {
			firstname = AC_firstNameInput.text;
		} else {
			firstname = "";
		}
		if (AC_lastNameInput.text != "" || AC_lastNameInput.text != null) {
			lastname = AC_lastNameInput.text;
		} else {
			lastname = "";
		}
		int grade = 0;
		int GradeInputValue = AC_gradeInput.value; // Getting the user inputted grade from dropdown, setting to 0 if nothing chosen
		if (GradeInputValue == 1) {
			grade = 9;
		} else if (GradeInputValue == 2) {
			grade = 10;
		} else if (GradeInputValue == 3) {
			grade = 11;
		} else if (GradeInputValue == 4) {
			grade = 12;
		}
		int math = 1; // Initialize class levels to "1"
		int science = 1;
		int history = 1;
		int english = 1;
		Account.AccountType? type = null; // Getting the user inputted account type from dropdown
		int AccountTypeValue = AC_accounttypeInput.value;
		if (AccountTypeValue == 1) {
			type = Account.AccountType.Teacher;
		} else if (AccountTypeValue == 2) {
			type = Account.AccountType.Student;
		}
		if (username == "") // Check if they entered a username
		{
			errmessage += "Please enter a username\n";
			err = true;
		}
		for (int i = 0; i < allAccounts.Count; i++) // Check username against other usernames
		{
			Account current = allAccounts[i];
			if (username == current.Username)
			{
				errmessage = "Username is already used\n";
				err = true;
				break;
			}
		}
		if (password == "") // Check if they entered a password
		{
			errmessage += "Please enter a password\n";
			err = true;
		}
		if (type == null) // Check if they selected a grade
		{
			errmessage += "Please select an account type\n";
			err = true;
		}

		// These two ifs check the username/password for a " , " or " = " or " * ", they are the only characters not allowed due to my method for writing the account to a file.
		if (username.Contains(",") || username.Contains("=") || username.Contains("*"))
		{
			errmessage += "The characters ' , ', ' = ', and ' * ' are not allowed. Please enter a new username\n";
			err = true;
		}
		if (password.Contains(",") || password.Contains("=") || password.Contains("*"))
		{
			errmessage += "The characters ' , ', ' = ', and ' * ' are not allowed. Please enter a new password";
			err = true;
		}
		if (err)
		{  // If an error happened, set error text active and make it equal the error string
			AccountCreationErrorText.text = errmessage;
			AccountCreationErrorText.gameObject.SetActive(true);
		}
		else
		{  // Create new account with info given
			newAccount = new Account(username, password, type, firstname, lastname, grade);
			WriteAccountToFile (newAccount); // Puts the account in the account file
			string newAccountPath = username + ".txt"; // Path for the new file
			if (File.Exists(newAccountPath)) // Check if the file already exists, if not - make it
			{
				// do nothing
			}
			else
			{
				File.CreateText (newAccountPath).Dispose();
			}
			using (StreamWriter sw = new StreamWriter(newAccountPath))
			{
				sw.WriteLine ("Username=" + username );
				sw.WriteLine ("Password=" + password);
				sw.WriteLine ("FirstName=" + firstname);
				sw.WriteLine ("LastName=" + lastname);
				sw.WriteLine ("AccountType=" + AccountTypeValue);
				sw.WriteLine ("* The following lines only apply to student accounts *");
				sw.WriteLine ("Grade=" + grade);
				sw.WriteLine ("Math=" + math);
				sw.WriteLine ("Science=" + science);
				sw.WriteLine ("History=" + history );
				sw.WriteLine ("English=" + english);
			}
			CreateNewAccountButton (); // Closes the new account panel
		}
	}

	public void LoginButton() // Button Callback to log in.  Checks information against account database.
	{
		print ("Login button pressed");
		LoginErrorText.gameObject.SetActive (false);
		ReadAccountFile ();
		Account account = null;
		bool err = true;
		bool usernameInputted = false;
		bool passwordInputted = false;
		string errmessage = "";
		string usernameInput = LOGIN_usernameInput.text;
		string passwordInput = LOGIN_passwordInput.text;
		for (int i = 0; i < allAccounts.Count; i++) // Sort through all accounts
		{
			errmessage = "";
			if (usernameInput == allAccounts [i].Username) // If the username matches an account, check the user inputted password against that same account
			{
				if (passwordInput == allAccounts [i].Password) 
				{
					err = false; // Allow to login
					account = allAccounts[i];
					passwordInputted = true;
					break;
				} 
				else 
				{
					err = true;
					break;
				}
			} 
			else 
			{
				err = true;
			}
		}
		if (err) 
		{
			if (usernameInputted && !passwordInputted)
			{
				errmessage = "Incorrect password entered";
				LoginErrorText.text = errmessage;
				LoginErrorText.gameObject.SetActive (true);
			} 
			else
			{
				errmessage = "Incorrect username and/or password entered";
				LoginErrorText.text = errmessage;
				LoginErrorText.gameObject.SetActive (true);
			}
		} 
		else 
		{
			GameObject persistant = GameObject.Find ("Persistant");
			persistant.GetComponent<PersistantScript> ().loggedIn = account;
			persistant.GetComponent<PersistantScript> ().AccountDatabase = allAccounts;
			if (account.AType == Account.AccountType.Student)
			{
				SceneManager.LoadScene ("Student");
			}
			else if (account.AType == Account.AccountType.Teacher)
			{
				SceneManager.LoadScene ("Teacher");
			}
		}
	}
}
