using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class BuildingController
	{
		public BuildingModel model = new BuildingModel();
		public BuildingView view;
		public ButtonController buttons = new ButtonController();

		public void Setup()
		{
			view = (BuildingView) SceneNodeView.FindObjectOfType(typeof(BuildingView));
			model.Setup();
			for (int index = 0; index < DataUtil.Length(view.cellButtons); index++)
			{
				buttons.view.Listen(view.cellButtons[index]);
			}
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
		}
	}
}
