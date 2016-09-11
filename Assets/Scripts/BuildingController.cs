using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class BuildingController
	{
		public BuildingModel model = new BuildingModel();
		public BuildingView view;
		public ButtonController buttons = new ButtonController();

		public void Setup()
		{
			view = (BuildingView) SceneNodeView.FindObjectOfType(typeof(BuildingView));
			int length = DataUtil.Length(view.cellButtons);
			model.cellCount = length < model.cellCount ? length : model.cellCount;
			model.Setup();
			for (int index = 0; index < length; index++)
			{
				buttons.view.Listen(view.cellButtons[index]);
			}
			buttons.view.Listen(view.completeSessionButton);
		}

		public void UpdateButtons()
		{
			buttons.Update();
			int index = DataUtil.IndexOf(view.cellButtons, buttons.view.target);
			if (0 <= index)
			{
				model.Select(index);
			}
		}

		public void Update()
		{
			UpdateButtons();
			for (int index = 0; index < DataUtil.Length(view.cellStates); index++)
			{
				AnimationView.SetState(view.cellStates[index], model.cellStates[index]);
			}
			UpdateCompleteAll();
		}

		private void UpdateCompleteAll()
		{
			if (view.completeSessionButton == buttons.view.target)
			{
				model.CompleteSession();
			}
			AnimationView.SetState(view.completeSession,
				model.completeState);
		}
	}
}
