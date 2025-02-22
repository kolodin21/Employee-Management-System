using NLog;
using Server.DAL.Repository;

namespace Server.BL
{
    public abstract class ServiceBase
    {
        public static RepositoryManager RepositoryManager = new();

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
    }
}
