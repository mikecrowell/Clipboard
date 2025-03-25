using FieldTool.Entity;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace FieldTool.ClipboardLookup.Helpers
{
    public static class DataHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T, object>> selector)
        {
            MemberExpression body = selector.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)selector.Body;
                body = ubody.Operand as MemberExpression;

                if (body == null)
                {
                    throw new ArgumentException("Could not get property name from selector", "selector");
                }
            }

            return body.Member.Name;
        }

        public static TProperty GetPropertyIfNotNull<TEntity, TProperty>(TEntity entity, Func<TEntity, TProperty> property, TProperty defaultValue)
        {
            if (entity == null)
            {
                return defaultValue;
            }

            return property(entity);
        }

        public static int GetBuildingTypeId(IClipBoardUpload context, string buildingCategory, string buildingType)
        {
            BuildingType bt = context.BuildingTypes.Where(x => x.BuildingCategory == buildingCategory && x.BuildingType_ == buildingType).FirstOrDefault();
            return bt != null ? bt.BuildingTypeId : 0;
        }
    }
}