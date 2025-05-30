using Mamont.Events;
using Mamont.Events.Signals;

using UnityEngine;

namespace Mamont.Gameplay.Control.LevelSpeed
{
	public class LevelSpeedControl:ILevelSpeedControl
	{
		private readonly IEventBusService _busService;


		public LevelSpeedControl( IEventBusService _busService )
		{
			this._busService = _busService;
			Subscribe();
		}

		private void Subscribe()
		{
			_busService.Subscribe<LevelPauseSignal>(OnlevelPause);
			_busService.Subscribe<LevelStateExitSignal>(OnLevelExitSignal);
		}
		private void UnSubscribe()
		{
			_busService.Unsubscribe<LevelPauseSignal>(OnlevelPause);
			_busService.Unsubscribe<LevelStateExitSignal>(OnLevelExitSignal);
		}

		private void OnLevelExitSignal( LevelStateExitSignal signal )
		{
			UnSubscribe();
		}
		private void OnlevelPause( LevelPauseSignal signal )
		{
			_isPause = signal.IsPuase;
			if( _isPause == true )
			{
				Time.timeScale = 0.0f;
			}
			else
			{
				Time.timeScale = CurrTimeScale;
			}
		}
		private bool _isPause = false;
		public void SwitchGameSpeed()
		{
			if( CurrGameSpeed == GameSpeedType.X1 )
			{
				CurrGameSpeed = GameSpeedType.X2;
			}
			else
			{
				CurrGameSpeed = GameSpeedType.X1;
			}
			_busService.Invoke(new GameSpeedChangedSignal());
			if( _isPause == false )
			{
				Time.timeScale = CurrTimeScale;
			}
		}

		public float CurrTimeScale
		{
			get
			{
				if( _isPause == true )
					return 0.0f;
				else
					return CurrGameSpeed switch
					{
						GameSpeedType.X1 => 1.0f,
						GameSpeedType.X2 => 2.0f,
						_ => 1.0f
					};
			}
		}
		public GameSpeedType CurrGameSpeed { private set; get; } = GameSpeedType.X1;


	}
}
