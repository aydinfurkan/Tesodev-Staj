using System.Data.SQLite;

namespace WorkerService.Repository.Interfaces
{
    public interface ISqLiteContext
    {
        public SQLiteConnection GetConnection();
    }
}