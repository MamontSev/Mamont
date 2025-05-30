using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.General.Tools
{
	[RequireComponent(typeof(Animator))]
	public class ToggleAnimator:MonoBehaviour
	{
		private void OnEnable()
		{
			button.onClick.AddListener(OnPressed);
		}
		private void OnDisable()
		{
			button.onClick.RemoveAllListeners();
		}

		private const string AnimParName = "intPar";
		private Animator animator => GetComponent<Animator>();

		[SerializeField]
		private Button button;
		private bool CurrState = true;

		private Action<bool> OnChange;
		public void Init(bool state, Action<bool> OnChange)
		{
			this.OnChange = OnChange;
			CurrState = state;
			if( CurrState )
			{
				PlayAnim(AnimState.FastOn);
			}
			else
			{
				PlayAnim(AnimState.FastOff);
			}
		}

		private bool mayPress = true;
		private void OnPressed()
		{
			if( mayPress == false )
			{
				return;
			}
			mayPress = false;

			if( CurrState )
			{
				PlayAnim(AnimState.Off);
			}
			else
			{
				PlayAnim(AnimState.On);
			}

			CurrState = !CurrState;
			OnChange?.Invoke(CurrState);

			delay();
			async void delay ()
			{
				await Task.Delay(400);
				mayPress = true;
			}
		}


		private void PlayAnim( AnimState state)
		{
			animator.SetInteger(AnimParName , (int)state);
		}

		private enum AnimState
		{
			On = 0,
			Off = 1,
			FastOn = 2,
			FastOff = 3
		}
	}
}
