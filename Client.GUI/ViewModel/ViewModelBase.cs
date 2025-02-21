using System.Collections;
using System.Windows;
using ReactiveUI;
using System.Windows.Controls;
using Client.HTTP;
using Microsoft.Extensions.DependencyInjection;

namespace Client.GUI.ViewModel
{
    public interface IContentChanger
    {
        event Action<UserControl, string> ContentChanged;
    }

    public abstract class ViewModelBase : ReactiveObject, IContentChanger
    {
        //DI провайдер
        protected static IServiceProvider ServiceProvider { get; private set; } = null!;
        protected static ManagerHttp ManagerHttp { get; private set; } = null!;
        public static void Initialize(IServiceProvider serviceProvider, ManagerHttp managerHttp)
        {
            ServiceProvider = serviceProvider;
            ManagerHttp = managerHttp;
        }

        #region IContentChanger

        //Событие для отображения нужной страницы
        public event Action<UserControl, string>? ContentChanged;

        protected void RaiseContentChanged(UserControl newContent, string title)
        {
            ContentChanged?.Invoke(newContent, title);
        }


        //Подписка на обновление данных
        protected void SubscribeToContentChanged<TViewModel>(TViewModel viewModel, Action<UserControl, string> updateContent)
            where TViewModel : class
        {
            if (viewModel is IContentChanger contentChanger)
            {
                contentChanger.ContentChanged += updateContent;
            }
        }
        #endregion


        //Получение выбранной страницы
        protected T GetPage<T>() where T : notnull =>
            ServiceProvider.GetRequiredService<T>();

        //Закрытие приложения 
        protected static void ExecExit()
        {
            Application.Current.Shutdown();
        }

        protected void ForeachAddCollection<T>(ICollection<T> outCollection,IEnumerable<T> inCollection )
        {
            foreach (var item in inCollection)
            {
                outCollection.Add(item);
            }
        }
    }
}
