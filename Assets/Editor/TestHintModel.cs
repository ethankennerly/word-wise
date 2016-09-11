using NUnit.Framework;

namespace Finegamedesign.Utils
{
	public sealed class TestHintModel
	{
		[Test]
		public void AttemptSpamShowTwiceAndReset()
		{
			HintModel hint = new HintModel();
			hint.availableAttempt = 3;
			hint.availableAgain = 2;
			hint.count = 2;
			for (int trial = 0; trial < 3; trial++)
			{
				string message = "trial " + trial.ToString();
				Assert.AreEqual(false, hint.isAvailable, message);
				Assert.AreEqual("none", hint.state, message);
				hint.Attempt();
				hint.Attempt();
				Assert.AreEqual(false, hint.isAvailable, message);
				Assert.AreEqual(-1, hint.index, message);
				hint.Attempt();
				Assert.AreEqual(true, hint.isAvailable, message);
				Assert.AreEqual(-1, hint.index, message);
				Assert.AreEqual("begin", hint.state, message);
				hint.Attempt();
				Assert.AreEqual(true, hint.isAvailable, message);
				Assert.AreEqual(-1, hint.index, message);
				Assert.AreEqual("begin", hint.state, message);
				hint.Show();
				hint.Show();
				Assert.AreEqual(false, hint.isAvailable, message);
				Assert.AreEqual(0, hint.index, message);
				Assert.AreEqual("end", hint.state, message);
				hint.Attempt();
				Assert.AreEqual(false, hint.isAvailable, message);
				hint.Attempt();
				Assert.AreEqual(true, hint.isAvailable, message);
				Assert.AreEqual("begin", hint.state, message);
				hint.Show();
				Assert.AreEqual(false, hint.isAvailable, message);
				Assert.AreEqual(1, hint.index, message);
				Assert.AreEqual("end", hint.state, message);
				hint.Attempt();
				hint.Attempt();
				Assert.AreEqual(false, hint.isAvailable, message);
				Assert.AreEqual(1, hint.index, message);
				hint.Reset();
			}
		}

		[Test]
		public void SetIsAvailable()
		{
			HintModel hint = new HintModel();
			Assert.AreEqual(false, hint.isAvailable);
			Assert.AreEqual("none", hint.state);
			hint.SetIsAvailable(false);
			Assert.AreEqual(false, hint.isAvailable);
			Assert.AreEqual("none", hint.state);
			hint.SetIsAvailable(true);
			Assert.AreEqual(true, hint.isAvailable);
			Assert.AreEqual("begin", hint.state);
			hint.SetIsAvailable(false);
			Assert.AreEqual(false, hint.isAvailable);
			Assert.AreEqual("end", hint.state);
			hint.SetIsAvailable(false);
			Assert.AreEqual(false, hint.isAvailable);
			Assert.AreEqual("end", hint.state);
			hint.SetIsAvailable(true);
			Assert.AreEqual(true, hint.isAvailable);
			Assert.AreEqual("begin", hint.state);
			hint.SetIsAvailable(false);
			Assert.AreEqual(false, hint.isAvailable);
			Assert.AreEqual("end", hint.state);
		}
	}
}
