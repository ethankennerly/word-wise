using NUnit.Framework;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestBuildingModel
	{
		[Test]
		public void SetupCellCount()
		{
			var model = new BuildingModel();
			model.cellCount = 2;
			model.Setup();
			Assert.AreEqual("available", model.cellStates[0]);
			Assert.AreEqual("none", model.cellStates[1]);
		}

		[Test]
		public void Select()
		{
			var model = new BuildingModel();
			model.cellCount = 2;
			model.Setup();
			Assert.AreEqual("building", model.state);
			Assert.AreEqual(-1, model.selectedIndex);
			Assert.AreEqual(false, model.isSelectNow);
			model.Select(0);
			Assert.AreEqual("spelling", model.state);
			Assert.AreEqual(0, model.selectedIndex);
			Assert.AreEqual(true, model.isSelectNow);
		}

		[Test]
		public void Complete()
		{
			var model = new BuildingModel();
			model.cellCount = 2;
			model.Setup();
			model.Select(0);
			model.Complete();
			Assert.AreEqual("complete", model.cellStates[0]);
			Assert.AreEqual("available", model.cellStates[1]);
		}

		[Test]
		public void UnlockAdjacentHorizontal()
		{
			var model = new BuildingModel();
			model.cellCount = 6;
			model.columnCount = 6;
			model.Setup();
			model.Select(0);
			model.UnlockAdjacent();
			Assert.AreEqual("available", model.cellStates[0]);
			Assert.AreEqual("available", model.cellStates[1]);
			Assert.AreEqual("none", model.cellStates[2]);
			Assert.AreEqual("none", model.cellStates[3]);
			model.Select(1);
			model.UnlockAdjacent();
			Assert.AreEqual("available", model.cellStates[2]);
			Assert.AreEqual("none", model.cellStates[3]);
			model.cellStates[5] = "available";
			model.Select(5);
			model.UnlockAdjacent();
			Assert.AreEqual("available", model.cellStates[4]);
			Assert.AreEqual("none", model.cellStates[3]);
			model.Complete();
			model.Select(4);
			model.UnlockAdjacent();
			Assert.AreEqual("complete", model.cellStates[5]);
		}

		[Test]
		public void UnlockAdjacentVertical()
		{
			var model = new BuildingModel();
			model.cellCount = 6;
			model.columnCount = 3;
			model.Setup();
			model.cellStates[2] = "available";
			model.Select(2);
			model.UnlockAdjacent();
			Assert.AreEqual("available", model.cellStates[1]);
			Assert.AreEqual("available", model.cellStates[5]);
			Assert.AreEqual("none", model.cellStates[3]);
			model.Setup();
			model.cellStates[3] = "available";
			model.Select(3);
			model.UnlockAdjacent();
			Assert.AreEqual("available", model.cellStates[0]);
			Assert.AreEqual("available", model.cellStates[4]);
			Assert.AreEqual("none", model.cellStates[2]);
		}
	}
}
