using Dapper.FluentMap;
using Dapper.FluentMap.Mapping;
using Microsoft.Data.Sqlite;
using Microsoft.SqlServer.Server;
using SBC_2D.Domain.Servicies;
using SBC_2D.Infrastructures;
using SBC_2D.Infrastructures.Ini;
using SBC_2D.Infrastructures.Recipe;
using SBC_2D.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace SBC_2D.Servicies
{
    public class RecipeService
    {
        private RecipeDao _recipeDao;
        private IniService _iniService;
        public RecipeService(IniService iniService, RecipeDao recipeDao)
        {
            _recipeDao = recipeDao;
            _iniService = iniService;
        }

        public List<string> GetAllNames()
        {
            try
            {
                List<string> names = _recipeDao.GetAllNames();
                return names;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Recipe Get(string name)
        {
            try
            {
                Recipe recipe = _recipeDao.GetByName(name);
                return recipe;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Save(Recipe recipe, out string message)
        {
            bool isUpdated = false;
            message = string.Empty;
            try
            {
                if (recipe == null)
                {
                    message = $"Can not saved recipe because it is null.";
                    return false;
                }
                isUpdated = _recipeDao.Update(recipe);
                if (!isUpdated)
                {
                    message = $"Failed to save recipe {recipe.Name}.";
                }
                else
                {
                    message = $"Succeed to save recipe {recipe.Name}.";
                }
            }
            catch (Exception ex)
            {
                message = $"Failed to save recipe {recipe?.Name ?? ""}";
            }
            return isUpdated;
        }

        public bool Create(Recipe recipe, out string message)
        {
            message = "";
            string name = recipe.Name;
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    message = $"Recipe of new name can't enpty.";
                    return false;
                }
                if(GetAllNames().Contains(name))
                {
                    message = $"Recipe name {name} has already exist.";
                    return false;
                }
                bool isCreated = _recipeDao.Create(recipe);
                if (!isCreated)
                {
                    message = $"Failed to create model {name}";
                }
                else
                {
                    message = $"Succeed to created recipe {name}";
                }
                return isCreated;
            }
            catch (Exception ex)
            {
                message = $"Failed to create model {name}";
                return false;
            }
        }

        public bool Delete(string name, out string message)
        {
            message = "";
            try
            {
                bool isDelete = _recipeDao.Delete(name);
                if (!isDelete)
                {
                    message = $"Failed to delete recipe {name}";
                }
                else
                {
                    message = $"Succeed to deleted recipe {name}";
                }
                return isDelete;
            }
            catch (Exception ex)
            {
                message = $"Failed to delete recipe {name}";
                return false;
            }
        }
    }
}

//public bool Edit(string propertyName, object value, out string message)
//{
//    message = string.Empty;
//    string recipeName = "Edit";

//    IPropertyMap map = FluentMapper.EntityMaps
//        .FirstOrDefault(m => m.Key == typeof(Recipe))
//        .Value.PropertyMaps
//        .FirstOrDefault(p => p.PropertyInfo.Name == propertyName);

//    if (map == null)
//    {
//        message = $"No column mapping found for property '{propertyName}'.";
//        return false;
//    }

//    try
//    {
//        object typedValue = Helper.ConvertValue(map.PropertyInfo.PropertyType, value);
//        bool isUpdated;
//        try
//        {
//            isUpdated = _recipeDao.Update(recipeName, map.ColumnName, value);
//        }
//        catch (Exception ex)
//        {
//            message = $"Failed to update '{propertyName}' in database.";
//            return false;
//        }

//        if (!isUpdated)
//        {
//            message = $"Recipe '{recipeName}' not found in database.";
//            return false;
//        }

//        map.PropertyInfo.SetValue(_store.EditedRecipe, typedValue);
//        return true;
//    }
//    catch (Exception ex)
//    {
//        // ConvertValue 失敗，DB 還沒動，不需要補救，只差UI要改回來
//        message = $"Invalid value type for '{propertyName}'.";
//        return false;
//    }
//}