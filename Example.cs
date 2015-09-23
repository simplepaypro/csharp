using System;
using System.Collections.Generic;

namespace SimplePay
{
	class SimplePayExample
	{
		public static void Main (string[] args)
		{
			// 1. Создаем массив с параметрами SP
			Dictionary<string, string> my = new Dictionary<string, string>();
			my["sp_amount"] = "100";
			my["sp_description"] = "Описание товара/услуги";
			my["sp_user_name"] = "Иван Иванов";
			my["sp_user_contact_email"] = "info@simplepay.pro";

			// 2. Инициализируем инстанс класса SP - быстрый путь
			SP_Signature sign_machine = new SP_Signature ("payment", "mysecret");

			// Получаем строку конкатенеции и подпись
			Console.WriteLine("Concatation string: "+sign_machine.make_concat_string(my));
			Console.WriteLine("Default (MD5) signature: "+sign_machine.make_signature_string(my));
			Console.WriteLine("SHA256 signature: "+sign_machine.make_signature_string(my, "sha256"));
			Console.WriteLine("SHA512 signature: "+sign_machine.make_signature_string(my, "sha512"));
		}
			
	}
}
