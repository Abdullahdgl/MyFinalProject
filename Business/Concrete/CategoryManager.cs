using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DataAccess.Abstract;
using Core.Utilities.Results;
using Business.Constants;

namespace Business.Concrete
{
	public class CategoryManager : ICategoryService
	{
		ICategoryDal _categoryDal;

		public CategoryManager(ICategoryDal categoryDal)
		{
			_categoryDal = categoryDal;
		}

		public IDataResult<List<Category>> GetAll()
		{
			//İş kodları
			if (DateTime.Now.Hour==18)
			{

				return new ErrorDataResult<List<Category>>(Messages.MainTenanceTime);
			}

			return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.ProductListed);



		}

		public IDataResult<Category> GetById(int categoryId)
		{

			return new SuccessDataResult<Category> ( _categoryDal.Get(c => c.CategoryId == categoryId));
		}
	}
}
