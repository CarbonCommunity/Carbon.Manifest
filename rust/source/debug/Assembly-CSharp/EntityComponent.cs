using System;
using UnityEngine;

public class EntityComponent<T> : EntityComponentBase where T : BaseEntity
{
	[NonSerialized]
	private T _baseEntity = null;

	protected T baseEntity {
		get {
			if (_baseEntity == null) {
				UpdateBaseEntity ();
			}
			return _baseEntity;
		}
	}

	protected void UpdateBaseEntity ()
	{
		if ((bool)this && (bool)base.gameObject) {
			_baseEntity = base.gameObject.ToBaseEntity () as T;
		}
	}

	protected override BaseEntity GetBaseEntity ()
	{
		return baseEntity;
	}
}
