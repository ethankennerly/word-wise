using NUnit.Framework;
using UnityEngine.UI;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestMainController
	{
		[Test]
		public void UpdateExitSpelling()
		{
			var controller = new MainController();
			controller.Setup();
			controller.Update();
			// TODO Assert.AreEqual("building", AnimationView.GetState(controller.view.state));
			ButtonView view = controller.building.buttons.view;
			view.Down(controller.building.view.cellButtons[0]);
			controller.Update();
			// TODO Assert.AreEqual("spelling", AnimationView.GetState(controller.view.state));
			view = controller.spelling.buttons.view;
			view.Down(controller.spelling.view.exitButton);
			controller.Update();
			// TODO Assert.AreEqual("spellingToBuilding", AnimationView.GetState(controller.view.state));
		}
	}
}
