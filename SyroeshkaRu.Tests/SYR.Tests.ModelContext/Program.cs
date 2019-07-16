using System;

namespace SYR.Tests.ModelContext
{
	internal static class Program
	{
		private static void Main()
		{
			try
			{
				using (var db = new Core.DomainModel.ModelContext())
				{
					try
					{
						Console.WriteLine(db.Database.ProviderName);
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