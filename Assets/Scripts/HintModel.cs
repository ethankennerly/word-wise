namespace Finegamedesign.Utils
{
	public sealed class HintModel
	{
		public int availableAttempt = -1;
		public int availableAgain = -1;
		public int count = 0;
		public int index = -1;
		public bool isAvailable = false;
		private int attemptCount = 0;

		public void Attempt()
		{
			attemptCount++;
			if (0 <= availableAttempt && index + 1 < count)
			{
				int excess = attemptCount - availableAttempt;
				isAvailable = 0 <= excess
					&& 0 == (excess % availableAgain);
			}
		}

		public void Show()
		{
			if (index + 1 < count)
			{
				if (availableAttempt < attemptCount)
				{
					attemptCount = availableAttempt;
				}
				index++;
				isAvailable = false;
			}
		}

		public void Reset()
		{
			attemptCount = 0;
			isAvailable = false;
			index = -1;
		}
	}
}
