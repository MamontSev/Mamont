namespace Mamont.Data.DataControl.Resources
{
	public interface IGameResourceService
	{
		float GetCount( GameResourceType? type );
		bool EnoughResource( GameResourceType? type, float count );
		void AddResource( GameResourceType? type , float count );
		void DecResource( GameResourceType? type , float count );
	}
}

