using System;
using SQLite.Net.Attributes;

namespace Test.DemoApp.Common.Entities
{
	public abstract class EntityBase
	{
		[PrimaryKey]
		public virtual string EntityKey { get; set; }

		public DateTime ClientCreatedOnUtc { get; set; }

		public DateTime ClientModifiedOnUtc { get; set; }

		public bool IsSyncPending { get; set; }

		public DateTime? ServerCreatedOnUtc { get; set; }

		public int ServerCreatedBy { get; set; }

		public DateTime? ServerModifiedOnUtc { get; set; }

		public int ServerModifiedBy { get; set; }
	}
}
