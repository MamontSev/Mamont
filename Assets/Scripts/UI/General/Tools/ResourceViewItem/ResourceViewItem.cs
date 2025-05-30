using System;

using Mamont.Data.DataConfig.UI;
using Mamont.Data.DataControl.Resources;
using Mamont.Events;
using Mamont.Events.Signals;

using Manmont.Tools;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

using Zenject;

namespace Mamont.UI.General.Tools
{
	public class ResourceViewItem:MonoBehaviour
	{
		private IGameResourceService _gameResourceService;
		private IEventBusService _eventBusService;
		private IGameResourcesIconConfig _gameResIconConfigh;
		[Inject]
		private void Construct
		(
			IGameResourceService _gameResourceService,
			IEventBusService _eventBusService,
			IGameResourcesIconConfig _gameResIconConfigh
		)
		{
			this._gameResourceService = _gameResourceService;
			this._eventBusService = _eventBusService;
			this._gameResIconConfigh = _gameResIconConfigh;
		}

		private void OnEnable()
		{
			_eventBusService.Subscribe<ResourceCountChangedSignal>(OnResourceCountChangedSignal);
		}

		private void OnDisable()
		{
			_eventBusService.Unsubscribe<ResourceCountChangedSignal>(OnResourceCountChangedSignal);
		}
		private void OnResourceCountChangedSignal( ResourceCountChangedSignal signal )
		{
			if( signal.resType != _resType )
			{
				return;
			}
			SetCount();
		}


		[SerializeField]
		private TextMeshProUGUI CountText;
		[SerializeField]
		private Image ResImage;
		[SerializeField]
		private Button GetResButton;

		private GameResourceType _resType;
		public void Init( GameResourceType _resType, Action<GameResourceType> Onpressed )
		{
			this._resType = _resType;
			GetResButton.onClick.RemoveAllListeners();
			GetResButton.onClick.AddListener(() => Onpressed(_resType));
			SetResImage();
			SetCount();
		}

		private void SetResImage()
		{
			ResImage.sprite = _gameResIconConfigh.ResourceSpriteSmall(_resType);
		}
		private void SetCount()
		{
			CountText.text = _gameResourceService.GetCount(_resType).DigitToString();
		}

	}
}
