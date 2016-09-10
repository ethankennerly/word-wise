using NUnit.Framework;
using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class TestPromptModel
	{
		[Test]
		public void ShowNextLetterSecondPromptEmpty()
		{
			PromptModel[] prompts = new PromptModel[2];
			prompts[0] = new PromptModel();
			prompts[1] = new PromptModel();
			prompts[0].answerText = "AB";
			prompts[0].answerTexts = new string[]{"A", "B"};
			PromptModel.ShowNextLetter(prompts);
			Assert.AreEqual("", prompts[1].answerText);
			Assert.AreEqual(0, DataUtil.Length(prompts[1].answerTexts));
		}
	}
}
