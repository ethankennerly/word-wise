using NUnit.Framework;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestBuildingController
	{
		[Test]
		public void UpdateAnimation()
		{
			var controller = new BuildingController();
			controller.model.cellCount = 2;
			controller.Setup();
			controller.Update();
			/* TODO
			Assert.AreEqual(controller.model.cellStates[0], 
				AnimationView.GetState(controller.view.cellStates[0]));
			Assert.AreEqual(controller.model.cellStates[1], 
				AnimationView.GetState(controller.view.cellStates[1]));
			 */
		}
	}
}
