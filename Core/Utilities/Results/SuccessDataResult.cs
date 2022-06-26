using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
	public class SuccessDataResult<T>:DataResult<T>		
	{
		private List<global::Entities.Concrete.Category> categories;
		private string productListed;

		public SuccessDataResult(T data, string message) : base(data, true, message)
		{

		}
		public SuccessDataResult(T data):base(data, true)
		{

		}
		public SuccessDataResult(string message):base(default,true,message)
		{

		}
		public SuccessDataResult():base(default,true)
		{

		}

		public SuccessDataResult(List<global::Entities.Concrete.Category> categories, string productListed)
		{
			this.categories = categories;
			this.productListed = productListed;
		}
	}
}
