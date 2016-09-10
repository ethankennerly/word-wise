using UnityEngine;

namespace Finegamedesign.CityOfWords
{
	public sealed class MainView : MonoBehaviour
	{
		public MainController controller = new MainController();
		public GameObject state;

		public void Setup()
		{
			if (null == state)
			{
				state = gameObject;
			}
		}

		public void Start()
		{
			Setup();
			controller.view = this;
			controller.Setup();
		}

		public void Update()
		{
			controller.Update();
		}
	}
}
