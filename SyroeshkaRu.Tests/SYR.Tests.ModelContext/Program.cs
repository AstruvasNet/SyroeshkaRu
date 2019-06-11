using System;
using Microsoft.EntityFrameworkCore;

namespace SYR.Tests.ModelContext
{
	class Program
	{
		static void Main(string[] args)
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
