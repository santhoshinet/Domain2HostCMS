using System.Data.SqlClient;

namespace Domain2HostCMSConsole
{
    public class Program
    {
        private static void Main()
        {
            using (var connection = new SqlConnection("Data Source=208.91.198.196;Initial Catalog=admin_hopestrack;Persist Security Info=True;User ID=hopestrack;Password=password@123"))
            {
                connection.Open();
                ExecuteQry(@" drop table [content_page]", connection);
                ExecuteQry(@"CREATE TABLE [content_page] (
    [content_page_id] INT NOT NULL,
    [<_content>k___backing_field] VARCHAR(MAX) NULL,
    [<_id>k___backing_field] VARCHAR(255) NULL,
    [<_name>k___backing_field] VARCHAR(255) NULL,
    [voa_version] SMALLINT NOT NULL,
    CONSTRAINT [pk_content_page] PRIMARY KEY ([content_page_id])
            )", connection);
                ExecuteQry("delete from mnu", connection);
                connection.Close();
            }
        }

        private static void ExecuteQry(string qry, SqlConnection connection)
        {
            var command = new SqlCommand(qry, connection);
            command.ExecuteNonQuery();
        }
    }
}