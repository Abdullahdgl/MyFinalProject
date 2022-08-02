using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
	public static class Messages
	{
		public static string ProductAdded = "Ürün Eklendi";
		public static string ProductNameInvalid = "Ürün ismi geçersiz.";
		public static string MainTenanceTime = "Sistem Bakımda";
		public static string ProductListed = "Ürünler Listelendi.";
		public static string ProductCountOfCategoryEror = "Bir Kategoride en fazla 10 ürün olabilir.";
		public static string ProductNameAlreadyExists = "Bu İsimde zaten başka bir ürün var";

		public static string CategoryLimitExceded = "Kategory Limiti aşıldığı için yeni ürün eklenemiyor.";
		public static string AuthorizationDenied = "Yetkiniz yok.!";
		public static string UserRegistered ="Kayıt oldu";
		public static string	 UserNotFound = "Kullanıcı bulunamadı.";
		public static string PasswordError = "Parola Hatası";
		public static string SuccessfulLogin = "Başarılı Giriş";
		public static string UserAlreadyExists = "Kullanıcı Mevcut";
		public static string AccessTokenCreated = "Token oluşturuldu. ";
	}
}
