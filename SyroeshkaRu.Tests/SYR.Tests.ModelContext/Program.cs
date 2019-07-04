using System;

namespace SYR.Tests.ModelContext
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				using (var _db = new Core.DomainModel.ModelContext())
				{
					try
					{
						Console.WriteLine(_db.Database);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

			Console.ReadLine();
		}
	}
}