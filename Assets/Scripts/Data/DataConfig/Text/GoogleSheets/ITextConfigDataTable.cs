
#if UNITY_EDITOR
#endif

using UnityEngine;

namespace Mamont.Data.DataConfig.Text
{
	public interface ITextConfigDataTable
	{
		TextAsset GetTextAsset( TextDataType type );
	}
}
