using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
	public void OnEnable()
	{
		MainMenuScreen.PlayButtonPressed += Play;
		MainMenuScreen.SettingsButtonPressed += OpenSettings;
		MainMenuScreen.ExitButtonPressed += Exit;
	}
	
	public void OnDisable()
	{
		MainMenuScreen.PlayButtonPressed -= Play;
		MainMenuScreen.SettingsButtonPressed -= OpenSettings;
		MainMenuScreen.ExitButtonPressed -= Exit;
	}
	
	public void Play()
	{
		SceneManagment.Instance.FadeAndLoadScene("MainScene");
		gameObject.SetActive(false);
	}
	
	public void OpenSettings()
	{
		
	}
	
	public void Exit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
		Application.Quit();
	}
}
