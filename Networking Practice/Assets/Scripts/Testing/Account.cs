using UnityEngine;
using System.Collections;

public class Account
{
	public string Username;
	public string Password;
	public AccountType? AType;
	public string FirstName = "No First Name";
	public string LastName = "No Last Name";
	public int GradeLevel = 1;

	// Student Variables
	private int Mathlevel = 1;
	private int Sciencelevel = 1;
	private int Historylevel = 1;
	private int Englishlevel = 1; 
	//
	// Teacher Variables
    
	//

	public enum AccountType
	{
		Teacher,
		Student
	}

	public Account(string user, string pass, AccountType? type, string firstName, string lastName, int grade)
	{
		Username = user;
		Password = pass;
		AType = type;
		if (firstName != "")
		{
			FirstName = firstName;
		}
		if (lastName != "")
		{
			LastName = lastName;
		}
		if (grade >= 9 && grade <= 12)
		{
			GradeLevel = grade;
		}
	}

	public int MathLevel
	{
		get
		{
			return Mathlevel;
		}
		set
		{
			if (AType == AccountType.Student)
				Mathlevel = value;
		}
	}
	public int ScienceLevel
	{
		get
		{
			return Sciencelevel;
		}
		set
		{
			if (AType == AccountType.Student)
				Sciencelevel = value;
		}
	}
	public int HistoryLevel
	{
		get
		{
			return Historylevel;
		}
		set
		{
			if (AType == AccountType.Student)
				Historylevel = value;
		}
	}
	public int EnglishLevel
	{
		get
		{
			return Englishlevel;	
		}
		set
		{
			if (AType == AccountType.Student)
				Englishlevel = value;
		}
	}
}
