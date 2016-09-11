namespace Finegamedesign.Utils
{
	public sealed class MenuModel
	{
		public int itemCount = 0;
		public int unlockedIndex = -2;
		public int selectedIndex = -1;

		public void Select(int index)
		{
			selectedIndex = index;
		}
	}
}
