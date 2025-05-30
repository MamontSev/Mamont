using UnityEngine;

namespace Mamont.Gameplay.UI.Health
{
	public interface IHealthSliderFactory
	{
		HealthSlider Get( Transform _parentTransform );
		void Return( HealthSlider obj );
	}
}
