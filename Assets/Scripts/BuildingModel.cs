using Finegamedesign.Utils;

namespace Finegamedesign.CityOfWords
{
	[System.Serializable]
	public sealed class BuildingModel
	{
		public string[] cellStates;
		public int cellCount = 0;
		public int contentCount = 99999999;
		public int columnCount = 3;
		public int sessionIndex = 0;
		public int selectedIndex = -1;
		public bool isSelectNow = false;
		public bool isFinalSession = false;
		public bool isCompleteAll = false;
		public string completeState = "none";
		public string state = "building";

		public void Setup()
		{
			cellCount = contentCount < cellCount ? contentCount : cellCount;
			cellStates = new string[cellCount];
			for (int index = 0; index < cellCount; index++)
			{
				cellStates[index] = index <= 0 ? "available" : "none";
			}
			isCompleteAll = false;
			completeState = "none";
		}

		public void Select(int index)
		{
			if ("none" != cellStates[index])
			{
				selectedIndex = index;
				state = "spelling";
				isSelectNow = true;
			}
		}

		public void Answer(int count)
		{
			if (cellStates[selectedIndex] != "complete")
			{
				cellStates[selectedIndex] = "answer_" + count;
			}
			UnlockAdjacent();
		}

		// If not enough words to fill next city, mark this city as final.
		// Test case:  2016-09-11 About 20 words in data.
		// Complete 2 cities of 9 each.  Jennifer Russ expects completed all.
		// Got 3rd city.  First word is unique.  Most others repeat.
		public void Complete()
		{
			cellStates[selectedIndex] = "complete";
			UnlockAdjacent();
			isFinalSession = !((sessionIndex + 2) * cellCount <= contentCount);
			isCompleteAll = true;
			for (int index = 0; index < cellCount; index++)
			{
				if (cellStates[index] != "complete")
				{
					isCompleteAll = false;
				}
			}
			completeState = isCompleteAll 
				?  (isFinalSession ? "all" : "begin") 
				: "none";
		}

		public void UnlockCell(int index)
		{
			if (0 <= index && index < DataUtil.Length(cellStates))
			{
				if ("none" == cellStates[index])
				{
					cellStates[index] = "available";
				}
			}
		}

		public void UnlockAdjacent()
		{
			int column = selectedIndex % columnCount;
			if (1 <= column)
			{
				UnlockCell(selectedIndex - 1);
			}
			if (column + 1 < columnCount)
			{
				UnlockCell(selectedIndex + 1);
			}
			UnlockCell(selectedIndex - columnCount);
			UnlockCell(selectedIndex + columnCount);
		}

		public int GetContentIndex()
		{
			int level = sessionIndex * cellCount + selectedIndex;
			for (int lower = sessionIndex; contentCount <= level && lower >= 0; lower--)
			{
				level = lower * cellCount + selectedIndex;
			}
			return level;
		}

		public void CompleteSession()
		{
			Setup();
			completeState = "end";
			sessionIndex++;
		}
	}
}
