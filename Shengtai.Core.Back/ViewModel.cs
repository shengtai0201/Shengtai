using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Shengtai
{
    // 無 entity 可對應的 viewmodel
    [Serializable]
    public abstract class ViewModel<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public TKey GetKey()
        {
            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                foreach (object attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is KeyAttribute keyAttribute)
                        return (TKey)property.GetValue(this);
                }
            }

            return default(TKey);
        }

        public static TViewModel ToViewModel<TViewModel, TEntity>(TEntity entity, params object[] args)
             where TViewModel : ViewModel<TKey>, IViewModel<TViewModel, TEntity>
        {
            return Extensions.ToViewModel<TViewModel, TEntity>(entity, args);
        }
    }

    // 無 entity 可對應的 viewmodel
    [Serializable]
    public abstract class ViewModel<TKey, TViewModel> : ViewModel<TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
        where TViewModel : ViewModel<TKey, TViewModel>
    {
        public abstract TViewModel Build(params object[] args);
    }

    public interface IViewModel<TViewModel, TEntity> where TViewModel : IViewModel<TViewModel, TEntity>
    {
        TViewModel Build(TEntity entity, params object[] args);
    }

    // 僅支援淺層複製
    [Serializable]
    public abstract class ViewModel<TKey, TViewModel, TEntity> : ViewModel<TKey>, IViewModel<TViewModel, TEntity>
        where TKey : IComparable<TKey>, IEquatable<TKey>
        where TViewModel : ViewModel<TKey, TViewModel, TEntity>
    {
        /// <summary>
        /// 自行撰寫設定其值的演算法
        /// </summary>
        /// <param name="entity">來自資料庫的 entity</param>
        /// <returns>view model</returns>
        protected virtual TViewModel Build(TEntity entity, params object[] args)
        {
            return this as TViewModel;
        }

        /// <summary>
        /// 透過 Reflection 於執行期設定其值
        /// </summary>
        /// <param name="entity">來自資料庫的 entity</param>
        /// <returns>view model</returns>
        public static TViewModel NewInstance(TEntity entity, params object[] args)
        {
            return Extensions.ToViewModel<TViewModel, TEntity>(entity, args);
        }

        TViewModel IViewModel<TViewModel, TEntity>.Build(TEntity entity, params object[] args)
        {
            return this.Build(entity, args);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public static TViewModel Deserialize(string value)
        {
            TViewModel result = null;

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    result = JsonConvert.DeserializeObject<TViewModel>(value);
                }
                catch { }
            }

            return result;
        }

        public virtual TEntity ToEntity()
        {
            TEntity entity = Activator.CreateInstance<TEntity>();
            var entityProperties = typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var viewModelType = typeof(TViewModel);
            foreach (var property in viewModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var propertyValue = property.GetValue(this);
                var propertyType = property.PropertyType.ToString();

                var entityProperty = entityProperties.SingleOrDefault(x =>
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
                if (entityProperty != null)
                    entityProperty.SetValue(entity, propertyValue);
            }

            return entity;
        }
    }
}