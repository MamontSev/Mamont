using UnityEngine;

namespace Mamont.UI.General.Tools
{
	public class StepLayOutVertical
	{
		private readonly Transform _container;
		private readonly float _stepVertical;
		private readonly float _stepHorizontal;
		public StepLayOutVertical( Transform _container, float _stepVertical , float _stepHorizzontal )
		{
			this._container = _container;
			this._stepVertical = _stepVertical;
			this._stepHorizontal = _stepHorizzontal;
			_itemCount = 0;
		}


		private int _horizontalDirection = -1;
		private int _itemCount;
		public void AddItem( Transform item)
		{
			_itemCount++;
			float vertStep = (_stepVertical * _itemCount) + 100.0f;

			Vector3 localPos = item.GetComponent<RectTransform>().localPosition;
			localPos.x += _stepHorizontal * _horizontalDirection;
			localPos.y += vertStep;
			item.GetComponent<RectTransform>().localPosition = localPos;

			Vector2 containerSize = _container.GetComponent<RectTransform>().sizeDelta;
			containerSize.y = vertStep + 200.0f;
			_container.GetComponent<RectTransform>().sizeDelta = containerSize;

			_horizontalDirection *= -1;

		}
	}
}
