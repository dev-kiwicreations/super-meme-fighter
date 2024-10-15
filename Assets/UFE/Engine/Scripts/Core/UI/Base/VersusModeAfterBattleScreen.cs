﻿using UnityEngine;
using System;
using System.Reflection;

namespace UFE3D
{
	public class VersusModeAfterBattleScreen : UFEScreen
	{
		#region protected enum definitions
		protected enum Option
		{
			RepeatBattle = 0,
			CharacterSelectionScreen = 1,
			StageSelectionScreen = 2,
			MainMenu = 3,
		}
		#endregion

		#region public instance methods
		public virtual void GoToCharacterSelectionScreen(bool restart = false)
		{
			this.SelectOption((int)VersusModeAfterBattleScreen.Option.CharacterSelectionScreen, UFE.GetLocalPlayer());
		}

		public virtual void GoToMainMenu()
		{
			this.SelectOption((int)VersusModeAfterBattleScreen.Option.MainMenu, UFE.GetLocalPlayer());
		}

		public virtual void GoToStageSelectionScreen()
		{
			this.SelectOption((int)VersusModeAfterBattleScreen.Option.StageSelectionScreen, UFE.GetLocalPlayer());
		}

		public virtual void RepeatBattle()
		{
			this.SelectOption((int)VersusModeAfterBattleScreen.Option.RepeatBattle, UFE.GetLocalPlayer());
		}
		#endregion

		#region public override methods
		public override void SelectOption(int option, int player)
		{

			VersusModeAfterBattleScreen.Option selectedOption = (VersusModeAfterBattleScreen.Option)option;
			if (selectedOption == VersusModeAfterBattleScreen.Option.CharacterSelectionScreen)
			{
				UFE.StartCharacterSelectionScreen();
			}
			else if (selectedOption == VersusModeAfterBattleScreen.Option.MainMenu)
			{
                UFE.StopMusic();
                UFE.StartMainMenuScreen();
			}
			else if (selectedOption == VersusModeAfterBattleScreen.Option.StageSelectionScreen)
			{
				UFE.StartStageSelectionScreen();
			}
			else if (selectedOption == VersusModeAfterBattleScreen.Option.RepeatBattle)
			{
                Debug.Log("Repeating Battle");/*
                UFE.StartLoadingBattleScreen();*/
				UFE.RestartMatch();
			}
		}
		#endregion
	}
}