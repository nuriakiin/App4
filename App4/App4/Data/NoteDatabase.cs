using App4.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App4.Data
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public NoteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
        }

        public Task<List<Item>> GetNotesAsync()
        {
            return _database.Table<Item>().ToListAsync();
        }

        public Task<Item> GetNoteAsync(int id)
        {
            return _database.Table<Item>()
                            .Where(i => int.Parse(i.Id) == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Item note)
        {
            if (int.Parse(note.Id) != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Item note)
        {
            return _database.DeleteAsync(note);
        }
    }
}
