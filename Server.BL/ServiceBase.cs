using Microsoft.Extensions.Caching.Memory;
using NLog;
using Server.DAL.Repository;
using System.Runtime.CompilerServices;

namespace Server.BL
{
    public static class CacheKey
    {
        public static string AllEmployees => "AllEmployees";
        public static string AllDepartments => "AllDepartments";
        public static string AllPositions => "AllPositions";
        public static string AllReports => "AllReports";
        public static string LeavesByEmployee(int employeeId) => $"LeavesByEmployee:{employeeId}";

    }

    public abstract class ServiceBase
    {
        public RepositoryManager RepositoryManager = new();
        public IMemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
        private static readonly SemaphoreSlim _cacheLock = new(1, 1); // Одновременно 1 п
                                                                      // оток
        protected async Task<bool> ExecuteWithLoggingAsync(
            Func<Task<bool>> action,
            Logger logger,
            string successMessage,
            string errorMessage)
        {
            try
            {
                var result = await action();
                if (result)
                    logger.Info(successMessage);
                else
                    logger.Warn(errorMessage);
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"{errorMessage}: {ex.Message}");
                return false;
            }
        }
        protected async Task<IEnumerable<T>> ExecuteWithLoggingAsync<T>(

            Func<Task<IEnumerable<T>>> action,
            Logger logger,
            string successMessage,
            string errorMessage)
        {
            try
            {
                var result = await action();
                if (result.Any())
                    logger.Info(successMessage);
                else
                    logger.Warn($"Полученные данные пусты. {errorMessage}");
                return result ?? [];
            }
            catch (Exception ex)
            {
                logger.Error($"{errorMessage}: {ex.Message}");
                return [];
            }
        }
        //TODO FIXME разобраться почему метод вызвается дважды
        protected async Task<IEnumerable<T>> ExecuteWithLoggingAsync<T>(
            Func<Task<IEnumerable<T>>> action, // Действие для получения данных
            Logger logger,
            string successMessage,
            string errorMessage,
            string cacheKey, // Ключ для кэша
            int cacheTime = 2,
            [CallerMemberName] string callerMethod = "") // Время хранения данных в кэше
        {
            logger.Warn($"Метод вызван из: {callerMethod}");

            // Пытаемся получить данные из кэша
            if (Cache.TryGetValue(cacheKey, out IEnumerable<T>? cachedData))
            {
                if (cachedData != null)
                {
                    logger.Info("Данные взяты из кэша.");
                    return cachedData;
                }
            }

            // Данных нет в кэше, выполняем запрос
            try
            {
                
                var result = await action();
                if (result.Any())
                {
                    logger.Info(successMessage);

                    // Сохраняем результат в кэш
                    var options = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(cacheTime));
                    Cache.Set(cacheKey, result, options);
                    logger.Info($"Данные в кэш записаны под ключом: {cacheKey}");
                }
                else
                {
                    logger.Warn($"Полученные данные пусты. {errorMessage}");
                }
                return result ?? [];
            }
            catch (Exception ex)
            {
                logger.Error($"{errorMessage}: {ex.Message}");
                return [];
            }
        }

        protected async Task<T> ExecuteWithLoggingAsync<T>(
            Func<Task<T>> action,
            Logger logger,
            string successMessage,
            string errorMessage)
        {
            try
            {
                var result = await action();
                if (result != null)
                    logger.Info(successMessage);
                else
                    logger.Warn($"Полученный объект пуст. {errorMessage}");
                return result;
            }
            catch (Exception ex)
            {
                logger.Error($"{errorMessage}: {ex.Message}");
                return default;
            }
        }

        protected void ClearCache(string cacheKey,Logger logger)
        {
            logger.Info($"Очистка кэша по ключу {cacheKey}");
            Cache.Remove(cacheKey);
        }

    }
}
