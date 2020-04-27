using Shengtai.Web.Telerik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shengtai
{
    public static partial class Extensions
    {
        public static void SetDataCollection<TKey, TViewModel, TEntity>(this IDataSourceResponse<TViewModel> response, IQueryable<TEntity> responseData, Action<TViewModel, TEntity> decorator = null)
            where TKey : IComparable<TKey>, IEquatable<TKey>//IComparable, IConvertible
            where TViewModel : ViewModel<TKey, TViewModel, TEntity>
        {
            var dataCollection = responseData.ToList();
            foreach (var data in dataCollection)
            {
                var viewModel = ViewModel<TKey, TViewModel, TEntity>.NewInstance(data);
                decorator?.Invoke(viewModel, data);

                response.DataCollection.Add(viewModel);
            }
        }

        public static TViewModel ToViewModel<TViewModel, TEntity>(TEntity entity, params object[] args)
            where TViewModel : IViewModel<TViewModel, TEntity>
        {
            if (entity == null)
                return default(TViewModel);

            TViewModel viewModel = Activator.CreateInstance<TViewModel>();
            var viewModelProperties = typeof(TViewModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var entityType = entity.GetType();
            foreach (var property in entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyValue = property.GetValue(entity);
                var propertyType = property.PropertyType.ToString();

                var viewModelProperty = viewModelProperties.SingleOrDefault(x =>
                {
                    var innerProperty = x.PropertyType.ToString();
                    bool sameType = propertyType == innerProperty;

                    if (!sameType)
                    {
                        if (propertyType.Contains(innerProperty) && propertyType.StartsWith("System.Nullable"))
                            sameType = true;
                        else if (innerProperty.Contains(propertyType) && innerProperty.StartsWith("System.Nullable"))
                            sameType = true;
                    }

                    return x.Name == property.Name && sameType;
                });
                if (viewModelProperty != null)
                    viewModelProperty.SetValue(viewModel, propertyValue);
            }

            return (viewModel as IViewModel<TViewModel, TEntity>).Build(entity, args);
        }

        public static string IntToString32(long value)
        {
            IList<char> result = new List<char>();
            string characters = "0123456789abcdefghijklmnopqrstuv";

            while (value > 0)
            {
                result.Insert(0, characters[Convert.ToInt32(value % 32)]);
                value = Math.Abs(value / 32);
            }

            return string.Join("", result.ToArray());
        }

        public static long String32ToInt(string value)
        {
            long result = 0;
            string characters = "0123456789abcdefghijklmnopqrstuv";

            int y = 0;
            foreach (var c in new string(value.ToCharArray().Reverse().ToArray()))
            {
                if (characters.Contains(c))
                {
                    result += characters.IndexOf(c) * ((long)Math.Pow(32, y));
                    y++;
                }
            }

            return result;
        }
    }
}