/*
 * (C) 2015 SimplePay LLC
 * https://simplepay.pro
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SimplePay
{
	/**
	 * Класс для формирования подписи SimplePay.
	 */ 
	public class SP_Signature
	{
		string script_name{ get; set; }
		string secret_key{ get; set; }

		/**
		 * Конструктор принимает два аргумента - имя скрипта и секретный ключ
		 */ 
		public SP_Signature (string script_name, string secret_key)
		{
			this.script_name = script_name;
			this.secret_key = secret_key;
		}
			
		/**
		 * Статический метод для генерации SHA256 хеша на основе строки в кодировке UTF-8
		 */
		static string sha256(string phrase)
		{
			SHA256Managed crypt = new SHA256Managed();
			byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(phrase));
			
			StringBuilder hash = new System.Text.StringBuilder();
			foreach (byte theByte in crypto)
			{
				hash.Append(theByte.ToString("x2"));
			}

			return hash.ToString();
		}

		/**
		 * Статический метод для генерации SHA512 хеша на основе строки в кодировке UTF-8
		 */
		static string sha512(string phrase)
		{
			SHA512Managed crypt = new SHA512Managed();
			byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(phrase));
			
			StringBuilder hash = new System.Text.StringBuilder();
			foreach (byte theByte in crypto)
			{
				hash.Append(theByte.ToString("x2"));
			}

			return hash.ToString();
		}

		/**
		 * Статический метод для генерации MD5 хеша на основе строки в кодировке UTF-8
		 */ 
		static string md5(string phrase)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] crypto = md5.ComputeHash(Encoding.UTF8.GetBytes(phrase));

			StringBuilder hash = new System.Text.StringBuilder();
			foreach (byte theByte in crypto)
			{
				hash.Append(theByte.ToString("x2"));
			}

			return hash.ToString();
		}

		/**
		 * Метод для создания строки конкатенации из коллекции (словаря Dictionary<string, string>).
		 */ 
		public string make_concat_string(Dictionary<string, string> ParametersArray){
			
			IEnumerable<KeyValuePair<string, string>> scoreQuery = 
				from mykey in ParametersArray 
				orderby mykey.Key ascending 
				select mykey;

			string script_name = this.script_name;
			string secret_key = this.secret_key;

			StringBuilder for_sign = new System.Text.StringBuilder(script_name+";");

			foreach (KeyValuePair<string, string> testScore in scoreQuery)
			{
				for_sign.Append(testScore.Value+";");
			}

			// Добавляем в конец секретный ключ
			for_sign.Append(secret_key);

			return for_sign.ToString();
		}

		/**
		 * Метод для формирования конечной подписи (с указанием алгоритма).
		 */ 
		public string make_signature_string(Dictionary<string, string> ParametersArray, string hash_algo){
			switch (hash_algo) {
				case "sha256": return SP_Signature.sha256 (this.make_concat_string (ParametersArray));
				case "sha512": return SP_Signature.sha512 (this.make_concat_string (ParametersArray));
				default: return SP_Signature.md5 (this.make_concat_string (ParametersArray));
			}
		}

		/**
		 * Метод для формирования конечной подписи (md5).
		 */ 
		public string make_signature_string(Dictionary<string, string> ParametersArray){
			return this.make_signature_string(ParametersArray, "md5");
		}
	}
}
