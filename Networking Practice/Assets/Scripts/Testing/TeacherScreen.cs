using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class TeacherScreen : MonoBehaviour 
{
	public Image ManageStudentsPanel;
	PersistantScript ps;
	public Text loggedInAs;
	public List<Account> students = new List<Account> ();
	public List<StudentButton> studentButtons = new List<StudentButton> ();

    public Button StudentButtonPrefab;
    public Canvas canvas;
	public Button FirstButtonPos;

	// Panel one objects
	public Image StudentPanelOne;
	public Account StudentOnOne;
	public Image PanelOneEdit;
	public Text StudentNameText;
	public Text GradeText;
	public Text MathLevelText;
	public Text ScienceLevelText;
	public Text HistoryLevelText;
	public Text EnglishLevelText;
	public InputField FirstNameInput;
	public InputField LastNameInput;
	public InputField GradeInput;
	public Toggle MathToggle1;
	public Toggle MathToggle2;
	public Toggle MathToggle3;
	public Toggle MathToggle4;
	public Toggle ScienceToggle1;
	public Toggle ScienceToggle2;
	public Toggle ScienceToggle3;
	public Toggle ScienceToggle4;
	public Toggle HistoryToggle1;
	public Toggle HistoryToggle2;
	public Toggle HistoryToggle3;
	public Toggle HistoryToggle4;
	public Toggle EnglishToggle1;
	public Toggle EnglishToggle2;
	public Toggle EnglishToggle3;
	public Toggle EnglishToggle4;
	public ToggleGroup MathToggleGroup;
	public ToggleGroup ScienceToggleGroup;
	public ToggleGroup HistoryToggleGroup;
	public ToggleGroup EnglishToggleGroup;
	//

	// Panel two objects
	public Image StudentPanelTwo;
	public Account StudentOnTwo;
	public Image PanelTwoEdit;
	public Text StudentNameText_;
	public Text GradeText_;
	public Text MathLevelText_;
	public Text ScienceLevelText_;
	public Text HistoryLevelText_;
	public Text EnglishLevelText_;
	public InputField FirstNameInput_;
	public InputField LastNameInput_;
	public InputField GradeInput_;
	public Toggle MathToggle_1;
	public Toggle MathToggle_2;
	public Toggle MathToggle_3;
	public Toggle MathToggle_4;
	public Toggle ScienceToggle_1;
	public Toggle ScienceToggle_2;
	public Toggle ScienceToggle_3;
	public Toggle ScienceToggle_4;
	public Toggle HistoryToggle_1;
	public Toggle HistoryToggle_2;
	public Toggle HistoryToggle_3;
	public Toggle HistoryToggle_4;
	public Toggle EnglishToggle_1;
	public Toggle EnglishToggle_2;
	public Toggle EnglishToggle_3;
	public Toggle EnglishToggle_4;
	public ToggleGroup MathToggleGroup_;
	public ToggleGroup ScienceToggleGroup_;
	public ToggleGroup HistoryToggleGroup_;
	public ToggleGroup EnglishToggleGroup_;
	//
	int currentdisplay_one = 1;
	int currentdisplay_two = 2;

	void Start()
	{
		//ps = GameObject.Find ("Persistant").GetComponent<PersistantScript> ();
		/*for (int i = 0; i < ps.AccountDatabase.Count; i++)
		{
			if (ps.AccountDatabase[i].AType == Account.AccountType.Student)
			{
				students.Add (ps.AccountDatabase [i]);
			}
		}
		ManageStudentsPanel.gameObject.SetActive (false);*/
        CreateButtons();
	}

	void Update()
	{
		/*if (ps.loggedIn != null)
		{
			loggedInAs.text = "Logged in as: " + ps.loggedIn.Username;
		}*/
		if (ManageStudentsPanel.gameObject.activeSelf)
		{
			UpdateButtonPos ();
		}
	}

	public void UpdateButtonPos() // In case the screen size changes, change button pos
	{
		for (int i = 0; i < studentButtons.Count; i++) 
		{
			studentButtons [i].transform.position = new Vector3 (FirstButtonPos.transform.localPosition.x, FirstButtonPos.transform.localPosition.y-(i*30), 0);
			studentButtons [i].transform.localPosition = studentButtons [i].transform.position;
		}
	}

	public void ManageStudentsButton()
	{
		ManageStudentsPanel.gameObject.SetActive (!ManageStudentsPanel.gameObject.activeSelf);
		if (ManageStudentsPanel.gameObject.activeSelf)
		{
			int length = students.Count;
			if (students [currentdisplay_one - 1] != null) 
			{
				StudentPanelOne.gameObject.SetActive (true);
				StudentOnOne = students [currentdisplay_one - 1];
			} 
			else 
			{
				StudentPanelOne.gameObject.SetActive (false);
			}
			if (students [currentdisplay_two - 1] != null) 
			{
				StudentOnTwo = students [currentdisplay_two - 1];
			}
			else 
			{
				StudentPanelOne.gameObject.SetActive (false);
			}
			UpdateInformation ();
		}
	}

    public void CreateButtons()
    {
        for (int i = 0; i < 5; i++)
        {
			Button button = Instantiate(StudentButtonPrefab, new Vector2(FirstButtonPos.transform.localPosition.x,FirstButtonPos.transform.localPosition.y-(i*30)), Quaternion.Euler(Vector3.zero)) as Button;
			button.transform.SetParent(ManageStudentsPanel.transform);
			button.transform.localPosition = button.transform.position;
			button.GetComponentInChildren<Text> ().text = "Student " + (i+1);
//			studentButtons.Add (button);
        }
		/*for (int i = 0; i < students.Count; i++)
		{
			Button newButton = Instantiate(StudentButtonPrefab, new Vector2(FirstButtonPos.transform.localPosition.x,FirstButtonPos.transform.localPosition.y-(i*30)), Quaternion.Euler(Vector3.zero)) as Button;
			newButton.transform.SetParent(ManageStudentsPanel.transform);
			newButton.transform.localPosition = button.transform.position;
			newButton.GetComponentInChildren<Text> ().text = "Student " + (i+1);
			int temp = i;
			button.onClick.AddListener(() => ChangeStudentOnStudentPanel(students[temp]));
			StudentButton toAdd = new StudentButton(students[i], newButton);
			studentButtons.Add (toAdd);
		}*/
    }

    public void ChangeStudentOnStudentPanel(Account account)
    {

    }

	public void UpdateInformation()
	{
		List<string> stats = new List<string> ();
		stats = ReadStudentFile (StudentOnOne);
		StudentNameText.text = stats[2] + " " + stats[3];
		GradeText.text = "Grade: " + stats[5].ToString ();
		MathLevelText.text = "Math Complexity Level: " + stats[6].ToString ();
		ScienceLevelText.text = "Science Complexity Level: " + stats[7].ToString ();
		HistoryLevelText.text = "History Complexity Level: " + stats[8].ToString ();
		EnglishLevelText.text = "English Complexity Level: " + stats[9].ToString ();
		stats.Clear ();
		stats = ReadStudentFile (StudentOnTwo);
		StudentNameText_.text = stats[2] + " " + stats[3];
		GradeText_.text = "Grade: " + stats[5].ToString ();
		MathLevelText_.text = "Math Complexity Level: " + stats[6].ToString ();
		ScienceLevelText_.text = "Science Complexity Level: " + stats[7].ToString ();
		HistoryLevelText_.text = "History Complexity Level: " + stats[8].ToString ();
		EnglishLevelText_.text = "English Complexity Level: " + stats[9].ToString ();
		stats.Clear ();
	}

	public List<string> ReadStudentFile(Account account)
	{
		List<string> stats = new List<string> ();
		string path = account.Username + ".txt";
		if (File.Exists(path))
		{
			using (StreamReader sr = new StreamReader(path))
			{
				List<string> lines = new List<string> ();
				string line;
				line = sr.ReadLine ();
				while (line != null)
				{
					lines.Add (line);
					line = sr.ReadLine ();
				}
				char[] splits = { '=' };
				for (int i = 0; i < lines.Count; i++)
				{
					if (!lines[i].Contains("*"))
					{
						string[] temp = lines [i].Split (splits);
						stats.Add (temp [1]);
					}
				}
				sr.Close();
			}
		}
		return stats;
	}

	public void EditStudentFile(Account account, string firstName, string lastName, int gradeLevel, int mathLevel, int scienceLevel, int historyLevel, int englishLevel)
	{
		using (StreamWriter sw = new StreamWriter(account.Username + ".txt"))
		{
			int type = 2;
			if (account.AType == Account.AccountType.Student)
			{
				type = 2;
			}
			else if (account.AType == Account.AccountType.Teacher)
			{
				type = 1;
			}
			sw.WriteLine ("Username=" + account.Username );
			sw.WriteLine ("Password=" + account.Password);
			sw.WriteLine ("FirstName=" + firstName);
			sw.WriteLine ("LastName=" + lastName);
			sw.WriteLine ("AccountType= " + type);
			sw.WriteLine ("* The following lines only apply to student accounts *");
			sw.WriteLine ("Grade=" + gradeLevel);
			sw.WriteLine ("Math=" + mathLevel);
			sw.WriteLine ("Science=" + scienceLevel);
			sw.WriteLine ("History=" + historyLevel);
			sw.WriteLine ("English=" + englishLevel);
		}
	}

	public void EditInformationButton (int panel)
	{
		if (panel == 1) 
		{
			List<string> stats = new List<string> ();
			stats = ReadStudentFile (StudentOnOne);
			PanelOneEdit.gameObject.SetActive (!PanelOneEdit.gameObject.activeSelf);
			FirstNameInput.text = stats[2];
			LastNameInput.text = stats[3];
			GradeInput.text = stats[5].ToString ();
			if (PanelOneEdit.gameObject.activeSelf)
			{
				Toggle activeMathToggle;
				Toggle activeScienceToggle;
				Toggle activeHistoryToggle;
				Toggle activeEnglishToggle;
				int math = 1;
				int science = 1;
				int history = 1;
				int english = 1;
				int.TryParse (stats [6], out math);
				int.TryParse (stats [7], out science);
				int.TryParse (stats [8], out history);
				int.TryParse (stats [9], out english);
				activeMathToggle = (math == 1) ? MathToggle1 : (math == 2) ? MathToggle2 : (math == 3) ? MathToggle3 : (math == 4) ? MathToggle4 : MathToggle1;
				activeScienceToggle = (science == 1) ? ScienceToggle1 : (science == 2) ? ScienceToggle2 : (science == 3) ? ScienceToggle3 : (science == 4) ? ScienceToggle4 : ScienceToggle1;
				activeHistoryToggle = (history == 1) ? HistoryToggle1 : (history == 2) ? HistoryToggle2 : (history == 3) ? HistoryToggle3 : (history == 4) ? HistoryToggle4 : HistoryToggle1;
				activeEnglishToggle = (english == 1) ? EnglishToggle1 : (english == 2) ? EnglishToggle2 : (english == 3) ? EnglishToggle3 : (english == 4) ? EnglishToggle4 : EnglishToggle1;
				activeMathToggle.isOn = true;
				activeScienceToggle.isOn = true;
				activeHistoryToggle.isOn = true;
				activeEnglishToggle.isOn = true;

			}
		}
		if (panel == 2) 
		{
			List<string> stats = new List<string> ();
			stats = ReadStudentFile (StudentOnTwo);
			PanelTwoEdit.gameObject.SetActive (!PanelTwoEdit.gameObject.activeSelf);
			FirstNameInput_.text = stats[2];
			LastNameInput_.text = stats[3];
			GradeInput_.text = stats[5].ToString ();
			if (PanelTwoEdit.gameObject.activeSelf)
			{
				Toggle activeMathToggle;
				Toggle activeScienceToggle;
				Toggle activeHistoryToggle;
				Toggle activeEnglishToggle;
				int math = 1;
				int science = 1;
				int history = 1;
				int english = 1;
				int.TryParse (stats [6], out math);
				int.TryParse (stats [7], out science);
				int.TryParse (stats [8], out history);
				int.TryParse (stats [9], out english);
				activeMathToggle = (math == 1) ? MathToggle_1 : (math == 2) ? MathToggle_2 : (math == 3) ? MathToggle_3 : (math == 4) ? MathToggle_4 : MathToggle_1;
				activeScienceToggle = (science == 1) ? ScienceToggle_1 : (science == 2) ? ScienceToggle_2 : (science == 3) ? ScienceToggle_3 : (science == 4) ? ScienceToggle_4 : ScienceToggle_1;
				activeHistoryToggle = (history == 1) ? HistoryToggle_1 : (history == 2) ? HistoryToggle_2 : (history == 3) ? HistoryToggle_3 : (history == 4) ? HistoryToggle_4 : HistoryToggle_1;
				activeEnglishToggle = (english == 1) ? EnglishToggle_1 : (english == 2) ? EnglishToggle_2 : (english == 3) ? EnglishToggle_3 : (english == 4) ? EnglishToggle_4 : EnglishToggle_1;
				activeMathToggle.isOn = true;
				activeScienceToggle.isOn = true;
				activeHistoryToggle.isOn = true;
				activeEnglishToggle.isOn = true;
			}
		}
	}

	public void DeleteStudentButton (int panel)
	{
		/*          Do nothing for now
		if (panel == 1) 
		{
			Account accountToDelete = StudentOnOne;
			for (int i = 0; i < ps.AccountDatabase.Count; i++)
			{
				Account A = ps.AccountDatabase [i];
				if (A.Username == accountToDelete.Username)
				{
					if (A.Password == accountToDelete.Password)
					{
						if (A.FirstName == accountToDelete.FirstName)
						{
							if (A.LastName == accountToDelete.LastName)
							{
								ps.AccountDatabase.Remove (A);
								break;
							}
						}
					}
				}
			}
		}*/
	}

	public void SaveChangesButton(int panel)
	{
		if (panel == 1)
		{
			List<string> stats = new List<string> ();
			stats = ReadStudentFile (StudentOnOne);
			char[] splits = { '.' };
			string firstname;
			string lastname;
			int grade = 1;
			int mathlevel = 1;
			int sciencelevel = 1;
			int historylevel = 1;
			int englishlevel = 1;
			int.TryParse (stats [5], out grade);
			firstname = FirstNameInput.text;
			lastname = LastNameInput.text;
			int.TryParse (GradeInput.text, out grade);
			string[] math = GetActive (MathToggleGroup).name.Split (splits);
			int.TryParse (math [1], out mathlevel);
			string[] science = GetActive (ScienceToggleGroup).name.Split (splits);
			int.TryParse (science [1], out sciencelevel);
			string[] history = GetActive (HistoryToggleGroup).name.Split (splits);
			int.TryParse (history [1], out historylevel);
			string[] english = GetActive (EnglishToggleGroup).name.Split (splits);
			int.TryParse (english [1], out englishlevel);
			EditStudentFile (StudentOnOne,firstname,lastname,grade,mathlevel,sciencelevel,historylevel,englishlevel);
			EditInformationButton (1);
		}
		if (panel == 2)
		{
			List<string> stats = new List<string> ();
			stats = ReadStudentFile (StudentOnOne);
			char[] splits = { '.' };
			string firstname;
			string lastname;
			int grade = 1;
			int mathlevel = 1;
			int sciencelevel = 1;
			int historylevel = 1;
			int englishlevel = 1;
			int.TryParse (stats [5], out grade);
			firstname = FirstNameInput_.text;
			lastname = LastNameInput_.text;
			int.TryParse (GradeInput_.text, out grade);
			string[] math = GetActive (MathToggleGroup_).name.Split (splits);
			int.TryParse (math [1], out mathlevel);
			string[] science = GetActive (ScienceToggleGroup_).name.Split (splits);
			int.TryParse (science [1], out sciencelevel);
			string[] history = GetActive (HistoryToggleGroup_).name.Split (splits);
			int.TryParse (history [1], out historylevel);
			string[] english = GetActive (EnglishToggleGroup_).name.Split (splits);
			int.TryParse (english [1], out englishlevel);
			EditStudentFile (StudentOnTwo,firstname,lastname,grade,mathlevel,sciencelevel,historylevel,englishlevel);
			EditInformationButton (2);
		}
		UpdateInformation ();
	}

	public void CancelChangesButton(int panel)
	{
		EditInformationButton (panel);
	}

	public Toggle GetActive(ToggleGroup group)
	{
		return group.ActiveToggles ().FirstOrDefault ();
	}

	public void NextTwo()
	{
		if (currentdisplay_one+2 <= ps.AccountDatabase.Count && currentdisplay_two+2 <= ps.AccountDatabase.Count)
		{
			currentdisplay_one += 2;
			currentdisplay_two += 2;
		}
		UpdateInformation ();
	}

	public void PreviousTwo()
	{
		if (currentdisplay_one-2 >= 0 || currentdisplay_two-2 >= 0)
		{
			currentdisplay_one -= 2;
			currentdisplay_two -= 2;
		}
		UpdateInformation ();
	}

	public void Logout()
	{
		ps.LogoutButton ();
	}
}
