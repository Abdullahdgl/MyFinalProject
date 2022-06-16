using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
	//generic constraint - cenerik kısıt demek
	// class : Referans tip
	// IEntity : IEntity olabilir veya IEntiy implemente eden bir nesne olabilir.
	//new() : new'lenebilir olmalı,
		public interface IEntityRepository<T> where T : class , IEntity, new()
	{

		List<T> GetAll(Expression<Func<T, bool>> filter =null); // bir listelemenin hepsini getirebilir. tümünü getir
		T Get(Expression<Func<T, bool>> filter); // burada filter yapmak zorunda
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);

		// List<T> GetAllByCategory(int categoryId); 
		// buradaki koda asla ihtiyacımız olmayacak Expression methodunun filter ı ekledikten sonra

	}
}
