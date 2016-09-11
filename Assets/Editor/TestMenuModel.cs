using NUnit.Framework;
using Finegamedesign.Utils;

namespace Finegamedesign.Utils
{
	public sealed class TestMenuModel
	{
		[Test]
		public void Select()
		{
			var menu = new MenuModel();
			menu.itemCount = 3;
			menu.Select(1);
			Assert.AreEqual(1, menu.selectedIndex);
		}
	}
}
