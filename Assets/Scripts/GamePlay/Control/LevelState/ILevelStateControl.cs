namespace Mamont.Gameplay.Control.LevelState
{
	public interface ILevelStateControl
	{
		void Start();
		void Pause();
		void UnPause();
		bool IsPlaying
		{
			get;
		}

	}
}
