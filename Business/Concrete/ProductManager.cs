﻿using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
	public class ProductManager : IProductService
	{
		IProductDal _productDal;
		ICategoryService _categoryService;
		

		public ProductManager(IProductDal productDal, ICategoryService categoryService)
		{
			_productDal = productDal;
			_categoryService = categoryService;

			
		}

		//cleim - İdda etmek,
		[SecuredOperation("product.add, admin")]
		[ValidationAspect(typeof( ProductValidator))]
		public IResult Add(Product product)
		{
			//aynı isimde ürün eklenemez.
			/// Eğer kategory ile ilgili bir kural oluşturmak istersek;
			/// eğer mevcut kategory sayısı 15 i geçti ise sisteme yeni bir ürün eklenemez.

			//business codes

			IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
				CheckIfProductCountOfCategoryCorrect(product.CategoryId),
				CheckIfCategoryLimitExceded() 
				);

			if (result!=null)
			{
				return result;
			}

					_productDal.Add(product);
					return new SuccessResult(Messages.ProductAdded);
		
			
			
		}
		 
		public IDataResult<List<Product>> GetALL()
		{
			if (DateTime.Now.Hour == 20)
			{
				return new ErrorDataResult<List<Product>>(Messages.MainTenanceTime);
			}
			return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);

			
		}

		public IDataResult<List<Product>> GetAllByCategoryId(int id)
		{
			 return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId ==id));
		}

		public IDataResult<Product> GetById(int productId)
		{
			return new SuccessDataResult<Product>( _productDal.Get(p => p.ProductId == productId));
		}

		public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
		{
			return new SuccessDataResult<List<Product>> (_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
		}

		public IDataResult<List<ProductDetailDto>> GetProductDetails()
		{
			return new SuccessDataResult<List<ProductDetailDto>> ( _productDal.GetProductDetails());
		}

		[ValidationAspect(typeof(ProductValidator))]
		public IResult Update(Product product)
		{
			if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
			{
				if (CheckIfProductNameExists(product.ProductName).Success)
				{
					_productDal.Add(product);
					return new SuccessResult(Messages.ProductAdded);
				}

			}
			return new ErrorResult();
		}

		private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
		{
			// select count(*) from products where categoryId= 1
			var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
			if (result >= 15)
			{
				return new ErrorResult(Messages.ProductCountOfCategoryEror);
			}
			return new SuccessResult();

		}
		private IResult CheckIfProductNameExists(string productName)
		{
			var result = _productDal.GetAll(p => p.ProductName == productName).Any();
			if (result ==true)
			{
				return new ErrorResult(Messages.ProductNameAlreadyExists);
			}
			return new SuccessResult();

		}


		//Aslında bunu yazmaktaki amaç product'ın kategory servisinin nasıl yorumlanıyor onun için yazıyoruz.
		// Eger bu kuralı kategoryManager'ı yazsaydık tek başına bir servis demekti. ama bu tamamen o kategory..
		//.. servisini kullanan bir ürünün onu nasıl ele aldığı ile alakalıdır.

		private IResult CheckIfCategoryLimitExceded()
		{
			var result = _categoryService.GetAll();
			if (result.Data.Count>15)
			{
				return new ErrorResult(Messages.CategoryLimitExceded);
			}

			return new SuccessResult();
		}


	}
}
