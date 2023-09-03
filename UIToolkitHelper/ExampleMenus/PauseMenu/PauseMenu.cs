using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIToolkitHelper;
using UnityEngine.UIElements;
using System;

public class PauseMenu : MenuBase
{
	public static Action OnResumeClicked;
	public static Action OnOptionsClicked;
	public static Action OnMainMenuClicked;
	public static Action OnQuitToDesktopClicked;
	
	[SerializeField] private int animatonDuration;
	[SerializeField] private Color pauseMenuBackgroundColor;
	[SerializeField] [TextArea(20, 10)] private string additionalTextString;
	
	private bool isPauseMenuShowing = false;
	
	private VisualElement leftPanel;
	
	private Vector3 startPosition;
	private Vector3 targetPosition;
	
	protected void Awake()
	{
		MenuManager.Instance.RegisterMenu(nameof(PauseMenu), this);
		isPauseMenuShowing = false;
	}
	
	protected void Update()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(!isPauseMenuShowing)
			{
				StartInitializingGUI();
				isPauseMenuShowing = true;
			}
			else
			{
				StartHidingGUI();
				isPauseMenuShowing = false;
			}
		}
	}
	
	public override IEnumerator GenerateGUI()
	{
		yield return null;
		
		leftPanel = Create("leftPanel", "left-panel");
		leftPanel.style.backgroundColor = pauseMenuBackgroundColor;
		root.Add(leftPanel);
		
		startPosition = GetDefaultStartPosition(leftPanel, -310, StartPositionDirection.RIGHT);
		targetPosition = GetDefaultTargetPosition(leftPanel);

		AnimateElement(leftPanel, startPosition, targetPosition, animatonDuration);
		
		// Header section
		var headerSection = Create("headerSection");
		headerSection.AddToClassList("header-section");
		leftPanel.Add(headerSection);

		var pauseMenuTitle = Create<Label>("pauseMenuTitle", "pauseMenuTitle");
		pauseMenuTitle.text = "Pause Menu";
		headerSection.Add(pauseMenuTitle);

		// Controls section
		var controlsSection = Create("controlsSection");
		controlsSection.AddToClassList("controls-section");
		leftPanel.Add(controlsSection);

		var resumeButton = Create<Button>("resumeButton");
		resumeButton.clicked += () =>
		{
			OnResumeClicked?.Invoke();
			StartHidingGUI();
		};
		resumeButton.text = "Resume";
		controlsSection.Add(resumeButton);
    
		var optionsButton = Create<Button>("optionsButton");
		optionsButton.clicked += () => { OnOptionsClicked?.Invoke(); };
		optionsButton.text = "Options";
		controlsSection.Add(optionsButton);
    
		var mainMenuButton = Create<Button>("mainMenupButton");
		mainMenuButton.clicked += () => 
		{
			OnMainMenuClicked?.Invoke();
		};	
		mainMenuButton.text = "Main Menu";
		controlsSection.Add(mainMenuButton);
		
		var quitToDesktopButton = Create<Button>("quitToDesktopButton");
		quitToDesktopButton.clicked += () => 
		{
			OnQuitToDesktopClicked?.Invoke();
		};	
		quitToDesktopButton.text = "Quit To Desktop";
		controlsSection.Add(quitToDesktopButton);
    
		// Additional Text section
		var additionalSection = Create("additionalSection");
		additionalSection.AddToClassList("additional-section");
		leftPanel.Add(additionalSection);

		var additionalText = Create<Label>("additionalText", "additionalText");
		additionalText.text = additionalTextString;
		additionalSection.Add(additionalText);
	}
	
	public override IEnumerator HideGUIRoutine()
	{
		isPauseMenuShowing = false;
		
		AnimateElement(leftPanel, targetPosition, startPosition, animatonDuration);
		
		yield return new WaitForSeconds(animatonDuration);
		
		root.Clear();
	}
}
