namespace Mamont.Data.DataControl.Resources
{
	public interface IResourcesStorageObject
	{
		float GetResCount( GameResourceType type );
		void SetResCount( GameResourceType type , float value );
	}
}