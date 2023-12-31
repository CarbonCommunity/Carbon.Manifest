public struct EntityRef<T> where T : BaseEntity
{
	private EntityRef entityRef;

	public bool IsSet => entityRef.IsSet ();

	public NetworkableId uid {
		get {
			return entityRef.uid;
		}
		set {
			entityRef.uid = value;
		}
	}

	public EntityRef (NetworkableId uid)
	{
		entityRef = new EntityRef {
			uid = uid
		};
	}

	public bool IsValid (bool serverside)
	{
		return Get (serverside).IsValid ();
	}

	public void Set (T entity)
	{
		entityRef.Set (entity);
	}

	public T Get (bool serverside)
	{
		BaseEntity baseEntity = entityRef.Get (serverside);
		if ((object)baseEntity == null) {
			return null;
		}
		if (!(baseEntity is T result)) {
			Set (null);
			return null;
		}
		return result;
	}

	public bool TryGet (bool serverside, out T entity)
	{
		entity = Get (serverside);
		return (object)entity != null;
	}
}
