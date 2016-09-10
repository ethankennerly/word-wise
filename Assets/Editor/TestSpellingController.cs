using NUnit.Framework;
using UnityEngine.UI;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestSpellingController
	{
		[Test]
		public void SetupLetterButtonTextsIsNotNull()
		{
			var controller = new SpellingController();
			controller.Setup();
			Assert.AreEqual(true, null != controller.view.letterButtonTexts);
		}

		[Test]
		public void PopulateMatchesFirstLetter()
		{
			var controller = new SpellingController();
			controller.Setup();
			controller.Populate();
			Assert.AreEqual(controller.model.letterButtonTexts[0],
				TextView.GetText(controller.view.letterButtonTexts[0]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(controller.view.letterButtons[7]));
			Assert.AreEqual(controller.model.promptAndAnswers[0].promptText,
				TextView.GetText(controller.view.promptAndAnswers[0].promptText));
			Assert.AreEqual(controller.model.empty,
				TextView.GetText(controller.view.promptAndAnswers[0].answerTexts[0]));
			Assert.AreEqual(true,
				SceneNodeView.GetVisible(
					controller.view.promptAndAnswers[0].answers[0]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.promptAndAnswers[3].answers[7]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
		}

		[Test]
		public void Load()
		{
			string[][] table = SpellingController.Load();
			Assert.AreEqual("topic", table[0][0]);
			Assert.AreEqual("letters", table[0][1]);
			Assert.AreEqual("prompt", table[0][2]);
			Assert.AreEqual("answer", table[0][3]);
		}

		private void AssertLetterSelected(SpellingController controller, int index, int length)
		{
			Assert.AreEqual(true,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[length]));
			Assert.AreEqual(controller.model.letterButtonTexts[index],
				TextView.GetText(
					controller.view.selected.answerTexts[length]));
			Assert.AreEqual(true,
				ToggleView.IsOn(
					controller.view.letterButtons[index]));
		}

		private SpellingController AssertButtonSelectedToggles()
		{
			var controller = new SpellingController();
			controller.model.score = 2000;
			controller.Setup();
			controller.Populate();
			controller.Update();
			Assert.AreEqual("2000",
				TextView.GetText(
					controller.view.scoreText));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			var button0 = controller.view.letterButtons[0];
			controller.buttons.view.Down(button0);
			controller.Update();
			AssertLetterSelected(controller, 0, 0);
			Assert.AreEqual("1999",
				TextView.GetText(
					controller.view.scoreText),
				"Each letter selected decrements score.");
			controller.buttons.view.Down(button0);
			controller.Update();
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			Assert.AreEqual("1999",
				TextView.GetText(
					controller.view.scoreText));
			controller.buttons.view.Down(button0);
			controller.Update();
			AssertLetterSelected(controller, 0, 0);
			controller.buttons.view.Down(button0);
			controller.Update();
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			return controller;
		}

		[Test]
		public void UpdateButtonSelectedToggles()
		{
			AssertButtonSelectedToggles();
		}

		[Test]
		public void UpdateButtonTogglesLettersAfter()
		{
			var controller = AssertButtonSelectedToggles();
			var buttons = controller.view.letterButtons;
			controller.buttons.view.Down(buttons[3]);
			controller.Update();
			AssertLetterSelected(controller, 3, 0);
			controller.buttons.view.Down(buttons[2]);
			controller.Update();
			AssertLetterSelected(controller, 2, 1);
			controller.buttons.view.Down(buttons[1]);
			controller.Update();
			AssertLetterSelected(controller, 1, 2);
			controller.buttons.view.Down(buttons[2]);
			controller.Update();
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[2]));
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[1]));
			Assert.AreEqual(false,
				ToggleView.IsOn(
					controller.view.letterButtons[1]));
			AssertLetterSelected(controller, 3, 0);
			controller.buttons.view.Down(buttons[3]);
			controller.Update();
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
			controller.buttons.view.Down(buttons[1]);
			controller.Update();
			AssertLetterSelected(controller, 1, 0);
			controller.buttons.view.Down(buttons[1]);
			controller.Update();
			Assert.AreEqual(false,
				SceneNodeView.GetVisible(
					controller.view.selected.answers[0]));
		}
	}
}
