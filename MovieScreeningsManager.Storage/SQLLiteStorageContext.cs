using Microsoft.Maui.Storage;
using MovieScreeningsManager.DBModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieScreeningsManager.Storage
{
    public class SQLLiteStorageContext : IStorageContext
    {
        private const string DatabaseFileName = "cinemaHall_manager.db3";
        private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "DB Storage 1", DatabaseFileName);
        private SQLiteAsyncConnection _databaseConnection;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private async Task Init()
        {
            await _semaphore.WaitAsync();

            try
            {
                if (_databaseConnection is not null)
                    return;

                bool isFirstLaunch = !File.Exists(DatabasePath);

                if (isFirstLaunch)
                    await CreateMockStorage();
                else
                    _databaseConnection = new SQLiteAsyncConnection(DatabasePath);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task CreateMockStorage()
        {
            var dir = Path.GetDirectoryName(DatabasePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            
            _databaseConnection = new SQLiteAsyncConnection(DatabasePath);
            var inMemoryStorage = new InMemoryStorageContext();

            await _databaseConnection.CreateTableAsync<CinemaHallDBModel>();
            await _databaseConnection.CreateTableAsync<ScreeningDBModel>();

            await foreach (var cinemaHall in inMemoryStorage.GetCinemaHallsAsync())
            {
                await _databaseConnection.InsertAsync(cinemaHall);
                await _databaseConnection.InsertAllAsync(await inMemoryStorage.GetScreeningsByCinemaHallAsync(cinemaHall.Id));
            }

           
        }

        public async Task<CinemaHallDBModel> GetCinemaHallAsync(Guid id)
        {
            await Init();
            return await _databaseConnection.Table<CinemaHallDBModel>().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async IAsyncEnumerable<CinemaHallDBModel> GetCinemaHallsAsync()
        {
            await Init();
            foreach (var cinemaHall in await _databaseConnection.Table<CinemaHallDBModel>().ToListAsync())
            {
                yield return cinemaHall;
            }
        }

        public async Task<ScreeningDBModel> GetScreeningAsync(Guid id)
        {
            await Init();
            return await _databaseConnection.Table<ScreeningDBModel>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<ScreeningDBModel>> GetScreeningsByCinemaHallAsync(Guid cinemaHallId)
        {
            await Init();
            return await _databaseConnection.Table<ScreeningDBModel>().Where(s => s.CinemaHallId == cinemaHallId).ToListAsync();
        }

        public async Task<int> GetScreeningsCountByCinemaHallAsync(Guid cinemaHallId)
        {
            await Init();
            return await _databaseConnection.Table<ScreeningDBModel>().CountAsync(s => s.CinemaHallId == cinemaHallId);
        }

        public async Task SaveScreeningAsync(ScreeningDBModel screening)
        {
            await _databaseConnection.InsertOrReplaceAsync(screening);
        }

        public async Task DeleteScreeningAsync(Guid screeningId)
        {
            await _databaseConnection.DeleteAsync<ScreeningDBModel>(screeningId);
        }
    }
}
