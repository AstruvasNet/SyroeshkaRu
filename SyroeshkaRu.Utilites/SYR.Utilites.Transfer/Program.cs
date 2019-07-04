namespace SYR.Utilites.Transfer
{
	internal class Program
	{
		private static void Main()
		{
			//	Console.WriteLine(new string('/', Console.WindowWidth));
			//	Console.WriteLine("\r\nSyroeshkaRu Transfer v1.0\r\n");
			//	Console.WriteLine(new string('\\', Console.WindowWidth));
			//	Console.WriteLine("\r\n");
			//	Console.BackgroundColor = ConsoleColor.Black;
			//	Console.ForegroundColor = ConsoleColor.White;
			//	Console.WriteLine("Идет обработка данных...\r\n");

			//	try
			//	{
			//		using (var db = new MySqlContext())
			//		{
			//			try
			//			{
			//				int countProducts = 0;
			//				int countSqlErrors = 0;
			//				int exceptions = 0;
			//				int addProducts = 0;
			//				int emptyItem = 0;
			//				foreach (var item in db.Node.Where(item => item.Type == "product"))
			//				{
			//					countProducts++;
			//					Console.ForegroundColor = ConsoleColor.White;
			//					Console.Write($"Id: {item.Id} - Name: {item.Title} (SystemId = {item.SystemId}): ");
			//					Console.ForegroundColor = ConsoleColor.DarkGreen;
			//					//Console.WriteLine(system);
			//					using (var system = new ModelContext())
			//					{
			//						try
			//						{
			//							item.Title = item.Title.Replace("'", "\"");
			//							IEdit compare = new EditService(system);
			//							var result = compare.EditProducts(new ProductsViewModel
			//							{
			//								Name = item.Title,
			//								Description = item.Title,
			//								Keywords = item.Title,
			//								Content = item.Title,
			//								IsNew = Convert.ToBoolean(item.IsNew)
			//							});
			//							if (result.ToString().Contains("Distinct"))
			//							{
			//								exceptions++;
			//								Console.ForegroundColor = ConsoleColor.DarkYellow;
			//							}
			//							else if (result.ToString().Contains("Empty"))
			//							{
			//								emptyItem++;
			//								Console.ForegroundColor = ConsoleColor.Red;
			//							}
			//							else
			//							{
			//								Console.BackgroundColor = ConsoleColor.Black;
			//								addProducts++;
			//							}

			//							Console.WriteLine(result);
			//						}
			//						catch (SqlException ex)
			//						{
			//							countSqlErrors++;
			//							Console.BackgroundColor = ConsoleColor.DarkRed;
			//							Console.Write("SQL Error: ");
			//							Console.BackgroundColor = ConsoleColor.Black;
			//							Console.ForegroundColor = ConsoleColor.DarkRed;
			//							Console.WriteLine(ex);
			//						}
			//					}
			//				}
			//				Console.WriteLine("\r\n");
			//				Console.ForegroundColor = ConsoleColor.White;
			//				Console.WriteLine(new string('-', Console.WindowWidth));
			//				Console.WriteLine($"Обработано строк: {countProducts}");
			//				Console.ForegroundColor = ConsoleColor.DarkGreen;
			//				Console.WriteLine($"Новых товаров: {addProducts}");
			//				Console.ForegroundColor = ConsoleColor.DarkYellow;
			//				Console.WriteLine($"Исключений: {exceptions}");
			//				Console.ForegroundColor = ConsoleColor.Red;
			//				Console.WriteLine($"Пустых полей: {emptyItem}");
			//				Console.BackgroundColor = ConsoleColor.Black;
			//				Console.ForegroundColor = ConsoleColor.DarkRed;
			//				Console.WriteLine($"Ошибок MSSQL: {countSqlErrors}");
			//			}
			//			catch (MySqlException ex)
			//			{
			//				Console.BackgroundColor = ConsoleColor.DarkRed;
			//				Console.Write("Sql Error: ");
			//				Console.BackgroundColor = ConsoleColor.Black;
			//				Console.ForegroundColor = ConsoleColor.DarkRed;
			//				Console.WriteLine(ex);
			//			}
			//		}
			//	}
			//	catch (Exception ex)
			//	{
			//		Console.BackgroundColor = ConsoleColor.DarkRed;
			//		Console.Write("System Error: ");
			//		Console.BackgroundColor = ConsoleColor.Black;
			//		Console.ForegroundColor = ConsoleColor.DarkRed;
			//		Console.WriteLine(ex);
			//	}

			//	Console.ReadLine();
		}
	}
}