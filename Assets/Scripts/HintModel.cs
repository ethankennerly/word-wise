namespace Finegamedesign.Utils
{
	public sealed class HintModel
	{
		public int availableAttempt = 10;
		public int availableAgain = 3;
		public int count = 3;
		public int index = -1;
		public bool isAvailable = false;
		// State is useful to animate.
		// none:  Never introduced.
		// begin:  Introducting hint button.  On last frame is still.
		// end:  Hiding hint button.  On last frame is hidden.
		public string state = "none";
		private int attemptCount = 0;

		public void Attempt()
		{
			if (!isAvailable)
			{
				attemptCount++;
			}
			if (0 <= availableAttempt)
			{
				int excess = attemptCount - availableAttempt;
				SetIsAvailable(0 <= excess
					&& 0 == (excess % availableAgain)
					&& index + 1 < count);
			}
		}

		public void SetIsAvailable(bool isAvailableNow)
		{
			isAvailable = isAvailableNow;
			if (isAvailableNow)
			{
				state = "begin";
			}
			else if ("begin" == state)
			{
				state = "end";
			}
		}

		public void Show()
		{
			if (isAvailable && index + 1 < count)
			{
				if (availableAttempt < attemptCount)
				{
					attemptCount = availableAttempt;
				}
				index++;
				SetIsAvailable(false);
			}
		}

		public void Reset()
		{
			attemptCount = 0;
			index = -1;
			isAvailable = false;
			state = "none";
		}
	}
}
