using Mamont.Events;
using Mamont.Events.Signals;

namespace Mamont.Gameplay.Control.LevelState
{
	public class LevelStateControl:ILevelStateControl
	{
		private readonly IEventBusService _eventBusService;

		public LevelStateControl( IEventBusService _eventBusService )
		{
			this._eventBusService = _eventBusService;
		}

		private LevelState _currState = LevelState.waitForStart;

		public void Start()
		{
			_currState = LevelState.playing;
			_eventBusService.Invoke(new LevelInitCompletedSignal());
		}

		private int _pauseCounter = 0;
		public void Pause()
		{
			_pauseCounter++;
			if( IsPlaying == true )
			{
				_currState = LevelState.paused;
				_eventBusService.Invoke(new LevelPauseSignal(true));
			}
			
		}
		public void UnPause()
		{
			_pauseCounter--;
			if( _pauseCounter == 0 )
			{
				_currState = LevelState.playing;
				_eventBusService.Invoke(new LevelPauseSignal(false));
			}
		}

		public bool IsPlaying => _currState == LevelState.playing;

		private enum LevelState
		{
			waitForStart,
			playing,
			paused
		}


	


	}
}
