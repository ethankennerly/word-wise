using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	public sealed class PromptModel
	{
		public static string empty = "";

		public static bool ShowNextLetter(PromptModel[] prompts)
		{
			bool isNow = false;
			int letterMax = DataUtil.Length(prompts[0].answerTexts);
			for (int letter = 0; !isNow && letter < letterMax; letter++)
			{
				for (int row = 0; !isNow && row < DataUtil.Length(prompts); row++)
				{
					PromptModel prompt = prompts[row];
					int length = DataUtil.Length(prompt.answerText);
					if (empty == prompt.answerTexts[letter] 
					&& letter < length)
					{
						if (letter == length - 1)
						{
							prompt.ShowAnswer(true);
						}
						else
						{
							prompt.ShowLetter(letter, true);
						}
						isNow = true;
					}
				}
			}
			return isNow;
		}

		public string[] answerTexts = new string[0];
		public string answerText = "";
		public bool isAnswerVisible = false;
		public bool isAnswerVisibleNow = false;
		public string promptText = "";

		public void PopulateAnswer(string answer, int letterMax)
		{
			answerTexts = new string[letterMax];
			answerText = answer;
			ShowAnswer(false);
		}

		private void ShowLetter(int letter, bool isVisible)
		{
			if (isVisible)
			{
				answerTexts[letter] = answerText[letter].ToString();
			}
			else
			{
				answerTexts[letter] = empty;
			}
		}

		public void ShowAnswer(bool isVisible)
		{
			isAnswerVisible = isVisible;
			isAnswerVisibleNow = isVisible;
			for (int letter = 0; letter < DataUtil.Length(answerTexts); letter++)
			{
				ShowLetter(letter, 
					isVisible && letter < DataUtil.Length(answerText));
			}
		}
	}
}
