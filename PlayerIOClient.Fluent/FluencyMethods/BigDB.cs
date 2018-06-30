namespace PlayerIOClient.Fluent {
	public static class FluentBigDBFluency {
		public static FluentBigDBWrapper CreateObject(this FluentBigDBWrapper db, string table, string key, DatabaseObject obj, out DatabaseObject dbo) {
			dbo = db.BigDB.CreateObject(table, key, obj); return db;
		}

		public static FluentBigDBWrapper DeleteKeys(this FluentBigDBWrapper db, string table, params string[] keys) {
			db.BigDB.DeleteKeys(table, keys); return db;
		}

		public static FluentBigDBWrapper DeleteRange(this FluentBigDBWrapper db, string table, string index, object[] indexPath, object start, object stop) {
			db.BigDB.DeleteRange(table, index, indexPath, start, stop); return db;
		}

		public static FluentBigDBWrapper Load(this FluentBigDBWrapper db, string table, string key, out DatabaseObject dbo) {
			dbo = db.BigDB.Load(table, key); return db;
		}

		public static FluentBigDBWrapper LoadKeys(this FluentBigDBWrapper db, string table, string[] keys, out DatabaseObject[] dbos) {
			dbos = db.BigDB.LoadKeys(table, keys); return db;
		}

		public static FluentBigDBWrapper LoadKeysOrCreate(this FluentBigDBWrapper db, string table, string[] keys, out DatabaseObject[] dbos) {
			dbos = db.BigDB.LoadKeysOrCreate(table, keys); return db;
		}

		public static FluentBigDBWrapper LoadMyPlayerObject(this FluentBigDBWrapper db, out DatabaseObject dbo) {
			dbo = db.BigDB.LoadMyPlayerObject(); return db;
		}

		public static FluentBigDBWrapper LoadOrCreate(this FluentBigDBWrapper db, string table, string key, out DatabaseObject dbo) {
			dbo = db.BigDB.LoadOrCreate(table, key); return db;
		}

		public static FluentBigDBWrapper LoadRange(this FluentBigDBWrapper db, string table, string index, object[] indexPath, object start, object stop, int limit, out DatabaseObject[] dbos) {
			dbos = db.BigDB.LoadRange(table, index, indexPath, start, stop, limit); return db;
		}

		public static FluentBigDBWrapper LoadSingle(this FluentBigDBWrapper db, string table, string index, object[] indexValue, out DatabaseObject dbo) {
			dbo = db.BigDB.LoadSingle(table, index, indexValue); return db;
		}

		public static FluentBigDBWrapper SaveChanges(this FluentBigDBWrapper db, bool useOptimisticLocks, bool fullOverwrite, params DatabaseObject[] objects) {
			db.BigDB.SaveChanges(useOptimisticLocks, fullOverwrite, objects); return db;
		}
	}
}
