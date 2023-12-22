public struct EntityRef<T> where T : BaseEntity
{
	private EntityRef entityRef;

	public bool IsSet => entityRef.IsSet ();

	public NetworkableId uid {
		get {
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return entityRef.uid;
		}
		set {
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			entityRef.uid = value;
		}
	}

	public EntityRef (NetworkableId uid)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
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
		if (baseEntity == null) {
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
		return entity != null;
	}
}
