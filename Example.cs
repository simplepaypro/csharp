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
			my["key"] = "value";
			my["key4"] = "value4";
			my["key2"] = "value2";
			my["key0"] = "value0";

			// 2. Инициализируем инстанс класса SP - быстрый путь
			SP_Signature sign_machine = new SP_Signature ("result", "mysecret");

			// Получаем строку конкатенеции и подпись
			Console.WriteLine("Concatation string: "+sign_machine.make_concat_string(my));
			Console.WriteLine("Default (MD5) signature: "+sign_machine.make_signature_string(my));
			Console.WriteLine("SHA256 signature: "+sign_machine.make_signature_string(my, "sha256"));
			Console.WriteLine("SHA512 signature: "+sign_machine.make_signature_string(my, "sha512"));

		}
			
	}
}