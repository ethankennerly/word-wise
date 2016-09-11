using NUnit.Framework;

namespace Finegamedesign.Utils
{
	public sealed class TestHintModel
	{
		[Test]
		public void AttemptShowAndReset()
		{
			HintModel hint = new HintModel();
			for (int trial = 0; trial < 3; trial++)
			{
				Assert.AreEqual(false, hint.isAvailable);
				hint.availableAttempt = 3;
				hint.availableAgain = 2;
				hint.count = 2;
				hint.Attempt();
				hint.Attempt();
				Assert.AreEqual(false, hint.isAvailable);
				Assert.AreEqual(-1, hint.index);
				hint.Attempt();
				Assert.AreEqual(true, hint.isAvailable);
				Assert.AreEqual(-1, hint.index);
				hint.Attempt();
				hint.Show();
				Assert.AreEqual(false, hint.isAvailable);
				Assert.AreEqual(0, hint.index);
				hint.Attempt();
				Assert.AreEqual(false, hint.isAvailable,
					"trial " + trial.ToString());
				hint.Attempt();
				Assert.AreEqual(true, hint.isAvailable);
				hint.Show();
				Assert.AreEqual(false, hint.isAvailable);
				Assert.AreEqual(1, hint.index);
				hint.Attempt();
				hint.Attempt();
				Assert.AreEqual(false, hint.isAvailable);
				Assert.AreEqual(1, hint.index);
				hint.Reset();
			}
		}
	}
}
