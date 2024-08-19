using UnityEngine;

namespace UFE3D
{
	public class StageReadyScreen : UFEScreen
	{
		public bool fadeBeforeGoingToLoadingBattleScreen = false;
		public float yesSoundDelay;

		public AudioClip yesSound;

		#region public instance methods

		public virtual void GoToStageSelectionScreen()
		{
			this.StartStageSelectionScreen();
		}

		public virtual void GoToLoadingBattleScreen()
		{
			this.StartLoadingBattleScreen();
		}

		#endregion

		#region public override methods
		public override void OnShow()
		{
			base.OnShow();
			//UFE.config.selectedStage = null;
			//UFE.config.selectedStage.stageName = null;
		}
		
		#endregion

		#region protected instance method
		
		protected virtual void StartStageSelectionScreen()
		{
			UFE.StartStageSelectionScreen();
		}

		protected virtual void StartLoadingBattleScreen()
		{
			UFE.PlaySound(yesSound);
			if (this.fadeBeforeGoingToLoadingBattleScreen)
			{
				UFE.StartLoadingBattleScreen();
			}
			else
			{
				UFE.StartLoadingBattleScreen(yesSoundDelay);
			}
		}
		
		#endregion
	}
}