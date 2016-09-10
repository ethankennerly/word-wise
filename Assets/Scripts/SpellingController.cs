using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class SpellingController
	{
		public static string[][] Load(string path = "words.csv")
		{
			string csv = StringUtil.Read(path);
			return StringUtil.ParseCsv(csv);
		}

		public SpellingModel model = new SpellingModel();
		public SpellingView view;
		public ButtonController buttons = new ButtonController();

		public void Setup()
		{
			model.table = Load();
			model.Setup();
			if (null == view)
			{
				view = (SpellingView) SceneNodeView.FindObjectOfType(typeof(SpellingView));
			}
			for (int index = 0; index < DataUtil.Length(view.letterButtons); index++)
			{
				buttons.view.Listen(view.letterButtons[index]);
			}
			buttons.view.Listen(view.exitButton);
			buttons.view.Listen(view.hintButton);
		}

		public void Populate()
		{
			model.Populate();
			UpdateView();
		}

		private void UpdateButtons()
		{
			buttons.Update();
			int letterButtonIndex = DataUtil.IndexOf(view.letterButtons, buttons.view.target);
			if (0 <= letterButtonIndex)
			{
				model.Toggle(letterButtonIndex);
			}
			else if (view.exitButton == buttons.view.target)
			{
				model.Exit();
			}
			else if (view.hintButton == buttons.view.target)
			{
				model.Hint();
			}
		}

		public void Update()
		{
			UpdateButtons();
			model.Update();
			UpdateView();
		}

		private void UpdateView()
		{
			ViewLetterButtons();
			ViewPrompts();
			ViewPrompt(model.selected, view.selected);
			ViewScore();
			TextView.SetText(view.topicText, model.topicText);
		}

		private void ViewLetterButtons()
		{
			for (int index = 0; index < DataUtil.Length(model.letterButtonTexts); index++)
			{
				string letter = model.letterButtonTexts[index];
				var letterView = view.letterButtonTexts[index];
				TextView.SetText(letterView, letter);
				SceneNodeView.SetVisible(view.letterButtons[index], model.empty != letter);
				ToggleView.SetIsOn(view.letterButtons[index], model.isLetterSelects[index]);
			}
		}

		private void ViewPrompts()
		{
			int index;
			for (index = 0; index < DataUtil.Length(model.promptAndAnswers); index++)
			{
				var prompt = model.promptAndAnswers[index];
				var promptView = view.promptAndAnswers[index];
				ViewPrompt(prompt, promptView);
			}
		}

		// Hide prompts that have no answer.
		private void ViewPrompt(PromptModel prompt, PromptView promptView)
		{
			TextView.SetText(promptView.promptText, prompt.promptText);
			bool isPromptVisible = prompt.answerText != "";
			SceneNodeView.SetVisible(promptView.gameObject, isPromptVisible);
			if (isPromptVisible)
			{
				for (int letter = 0; letter < DataUtil.Length(prompt.answerTexts); letter++)
				{
					string a = prompt.answerTexts[letter];
					TextView.SetText(promptView.answerTexts[letter], a);
					bool isVisible = letter < DataUtil.Length(prompt.answerText);
					SceneNodeView.SetVisible(promptView.answers[letter], isVisible); 
				}
			}
		}

		private void ViewScore()
		{
			TextView.SetText(view.scoreText, model.score.ToString());
		}
	}
}
