using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using SYR.Utilites.Core.DomainModel.Model;

namespace SYR.Utilites.Core.DomainModel
{
	public sealed class MySqlContext : DbContext
	{
		private SshClient _ssh = new SshClient("5.187.6.42", "root", "7M1j5B7h");
		private MySqlConnectionStringBuilder _connection = new MySqlConnectionStringBuilder();

		public DbSet<Node> Node { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			_connection.Server = "localhost";
			_connection.Port = 22;
			_connection.UserID = "syroeshka_yar";
			_connection.Password = "6T0v9I2g";
			_connection.Database = "mg23sx";

			if (!_ssh.IsConnected)
			{
				_ssh.Connect();
				ForwardedPortLocal port = new ForwardedPortLocal("127.0.0.1", 22, "localhost", 3306);
				_ssh.AddForwardedPort(port);
				port.Start();
			}

			optionsBuilder.UseMySQL(_connection.ConnectionString);
		}
	}
}
