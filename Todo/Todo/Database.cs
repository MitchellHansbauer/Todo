using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Todo
{
    public class Database
    {
        private readonly SQLiteAsyncConnection database;
        public Database(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoTask>();
        }

        public Task<List<TodoTask>> GetToDoAsync()
        {
            return database.Table<TodoTask>().ToListAsync();
        }

        public Task<int> SaveTodoTaskAsync(TodoTask todoTask)
        {
            return database.InsertAsync(todoTask);
        }


    }
}
