using DG.Tweening;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

namespace Mamont.UI.General.Tools
{
	public class LevelRewardItem:MonoBehaviour
	{
		[SerializeField]
		private Image RewardImage;
		[SerializeField]
		private TextMeshProUGUI RewardText;

		public YieldInstruction Init( Sprite rewardSprite , string rewardText )
		{
			RewardImage.sprite = rewardSprite;
			RewardText.text = rewardText;
			return transform.
					DOScale(1.0f , 0.5f).
					SetEase(Ease.InOutBounce).
					From(0).
					SetUpdate(true).
					WaitForCompletion();
		}
	}
}
