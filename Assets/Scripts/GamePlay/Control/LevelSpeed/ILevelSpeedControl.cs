namespace Mamont.Gameplay.Control.LevelSpeed
{
	public interface ILevelSpeedControl
	{
		void SwitchGameSpeed();
		float CurrTimeScale
		{
			get;
		}
		GameSpeedType CurrGameSpeed
		{
			get;
		}
	}
}
