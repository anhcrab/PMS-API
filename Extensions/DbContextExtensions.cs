using api.Databases;

namespace api.Extensions
{
	public static class DbContextExtensions
	{
		public static void TryUpdateManyToMany<T, TKey>(this ApplicationDbContext db, IEnumerable<T> currentItems, IEnumerable<T> newItems, Func<T, TKey> getKey) where T : class
		{
			db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
			db.Set<T>().AddRange(newItems.Except(currentItems, getKey));
		}

		public static IEnumerable<TEntity> Except<TEntity, TKey>(
				this IEnumerable<TEntity> left,
				IEnumerable<TEntity> right,
				Func<TEntity, TKey> keyRetrievalFunction)
		{
			var leftSet = left.ToList();
			var rightSet = right.ToList();

			var leftSetKeys = leftSet.Select(keyRetrievalFunction);
			var rightSetKeys = rightSet.Select(keyRetrievalFunction);

			var deltaKeys = leftSetKeys.Except(rightSetKeys);
			var leftComplementRightSet = leftSet.Where(i => deltaKeys.Contains(keyRetrievalFunction.Invoke(i)));
			return leftComplementRightSet;
		}
	}
}