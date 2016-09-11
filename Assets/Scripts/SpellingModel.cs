using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class SpellingModel
	{
		public static SpellingModel[] contents;
		public static int contentCount;

		public string[][] table;
		public int topicColumn = 0;
		public int lettersColumn = 1;
		public int promptColumn = 2;
		public int contentIndex = 0;
		public string empty = PromptModel.empty;
		public int letterMax = 8;
		public int promptMax = 4;
		public int score = 4000;
		public int scorePerHint = -20;
		public bool isExitNow = false;
		public bool isAnswerAllNow = false;

		public string[] letterButtonTexts;
		public PromptModel[] promptAndAnswers;
		public PromptModel selected;
		public bool[] isLetterSelects;
		public int[] letterButtonsSelected;
		public int answerCount = 0;
		public string topicText = "";
		public HintModel hint = new HintModel();

		public void Setup()
		{
			letterButtonTexts = new string[letterMax];
			promptAndAnswers = new PromptModel[promptMax];
			selected = new PromptModel();
			selected.answerTexts = new string[letterMax];
			if (null != table)
			{
				contentCount = DataUtil.Length(table) - 1;
				contents = new SpellingModel[contentCount];
				for (int index = 0; index < contentCount; index++)
				{
					var content = new SpellingModel();
					content.Setup();
					content.PopulateRow(GetRow(index));
					contents[index] = content;
				}
			}
		}

		private string[] GetRow(int contentIndex)
		{
			return table[contentIndex + 1];
		}

		public void Populate()
		{
			var content = contents[contentIndex];
			ReferTo(content);
			ClearSelected();
			hint.Reset();
		}

		private void PopulateRow(string[] row)
		{
			PopulateLetterButtons(row);
			PopulatePrompts(row);
			topicText = row[topicColumn];
			ClearSelected();
		}

		private void ReferTo(SpellingModel content)
		{
			answerCount = content.answerCount;
			isLetterSelects = content.isLetterSelects;
			letterButtonTexts = content.letterButtonTexts;
			letterButtonsSelected = content.letterButtonsSelected;
			promptAndAnswers = content.promptAndAnswers;
			selected = content.selected;
			topicText = content.topicText;
		}

		private void PopulateLetterButtons(string[] row)
		{
			string letters = row[lettersColumn];
			int index;
			for (index = 0; index < DataUtil.Length(letterButtonTexts); index++)
			{
				if (index < DataUtil.Length(letters))
				{
					letterButtonTexts[index] = letters[index].ToString();
				}
				else
				{
					letterButtonTexts[index] = empty;
				}
			}
		}

		private void PopulatePrompts(string[] row)
		{
			answerCount = 0;
			for (int p = 0; p < DataUtil.Length(promptAndAnswers); p++)
			{
				var prompt = new PromptModel();
				int index = p * 2 + promptColumn;
				if (index < DataUtil.Length(row))
				{
					prompt.promptText = row[index];
					string answer = row[index + 1];
					prompt.PopulateAnswer(answer, letterMax);
				}
				promptAndAnswers[p] = prompt;
			}
		}

		// Do not count answer when entering text of a visible answer.
		// Test case:  iamevn says ...  Aug 28, 2016 @ 1:15pm "Got a bug, I'm on 1927 in the alphabet category and I can't input what I think is the answer for CUNEIFORM because it starts with the answer to SUMMONS"  
		public void UpdateAnswer()
		{
			string answer = selected.answerText;
			int index;
			bool isAnswerNow = false;
			PromptModel prompt;
			for (index = 0; index < DataUtil.Length(promptAndAnswers); index++)
			{
				prompt = promptAndAnswers[index];
				if (answer == prompt.answerText && answer != empty 
				&& !prompt.isAnswerVisible)
				{
					isAnswerNow = true;
					prompt.ShowAnswer(true);
					ClearSelected();
				}
				else if (prompt.isAnswerVisibleNow)
				{
					isAnswerNow = true;
					prompt.isAnswerVisibleNow = false;
				}
				else
				{
					prompt.isAnswerVisibleNow = false;
				}
			}
			answerCount = 0;
			for (index = 0; index < DataUtil.Length(promptAndAnswers); index++)
			{
				prompt = promptAndAnswers[index];
				if (prompt.isAnswerVisible)
				{
					answerCount++;
				}
			}
			if (isAnswerNow)
			{
				isAnswerAllNow = true;
				for (index = 0; index < DataUtil.Length(promptAndAnswers); index++)
				{
					prompt = promptAndAnswers[index];
					if (!prompt.isAnswerVisible && empty != prompt.answerText)
					{
						isAnswerAllNow = false;
					}
				}
			}
		}

		public void Update()
		{
			UpdateAnswer();
		}

		private void ClearSelected()
		{
			DataUtil.Clear(selected.answerTexts);
			isLetterSelects = new bool[letterMax];
			letterButtonsSelected = new int[letterMax];
			for (int index = 0; index < DataUtil.Length(letterButtonsSelected); index++)
			{
				isLetterSelects[index] = false;
				letterButtonsSelected[index] = -1;
			}
			selected.answerText = "";
		}

		public void AddScore(int amount)
		{
			score += amount;
			if (score < 0)
			{
				score = 0;
			}
		}

		public bool Toggle(int letterButtonIndex)
		{
			bool isSelectedNext = !(isLetterSelects[letterButtonIndex]);
			string letter = letterButtonTexts[letterButtonIndex];
			int length = DataUtil.Length(selected.answerText);
			if (isSelectedNext)
			{
				selected.answerText += letter;
				selected.answerTexts[length] = letter;
				letterButtonsSelected[length] = letterButtonIndex;
				AddScore(-1);
				hint.Attempt();
			}
			else
			{
				// int last = DataUtil.LastIndexOf(selected.answerText, letter);
				int last = DataUtil.LastIndexOf(letterButtonsSelected, letterButtonIndex);
				if (0 <= last)
				{
					for (int after = last; after < letterMax; after++)
					{
						int select = letterButtonsSelected[after];
						if (-1 != select)
						{
							isLetterSelects[select] = false;
						}
						letterButtonsSelected[after] = -1;
					}
					selected.answerText = StringUtil.Remove(selected.answerText, last);
				}
				else
				{
					DebugUtil.Log("SpellingModel.Toggle: Did not expect <" 
						+ letter + "> was not in <" + selected.answerText + ">");
				}
				selected.answerTexts[length - 1] = empty;
				letterButtonsSelected[length - 1] = -1;
			}
			isLetterSelects[letterButtonIndex] = isSelectedNext;
			return isSelectedNext;
		}

		public void Hint()
		{
			if (PromptModel.ShowNextLetter(promptAndAnswers))
			{
				AddScore(scorePerHint);
				hint.Show();
			}
		}

		public void Exit()
		{
			isExitNow = true;
		}
	}
}
