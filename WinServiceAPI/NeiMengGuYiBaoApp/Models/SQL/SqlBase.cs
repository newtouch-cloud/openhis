using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.SQL
{
   public class SqlBase
    {

        #region 获得实体的添加语句

        /// <summary>
        /// 将实体转换为用于增加记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为"insert into {0}({1}) values({2})"
        /// 注意：该方法返回的sql语句中用到的表名为该实体的类名，用到的字段名为该类的公共属性组合。
        /// </remarks>
        /// <returns>返回向数据表中插入一条记录的sql语句</returns>
        public virtual string ToAddSql()
        {
            return AddSql(false, null);
        }
        /// <summary>
        /// 将实体转换为用于增加记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为"insert into {0}({1}) values({2})"
        /// 注意：该方法返回的sql语句中用到的表名为该实体的类名，用到的字段名为该类的公共属性组合。
        /// </remarks>
        /// <returns>返回向数据表中插入一条记录的sql语句</returns>
        public virtual string ToAddBaseSql()
        {
            return AddSql(true, null);
        }
        /// <summary>
        /// 根据指定的属性数组转换为用于增加记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为"insert into {0}({1}) values({2})"
        /// 注意：该方法返回的sql语句中用到的表名为该实体的类名，用到的字段名为指定的该类的公共属性组合。
        /// </remarks>
        /// <param name="fields">需要插入的属性名数组（传入的字符串必须和属性名相同）</param>
        /// <returns>返回向数据表中插入一条记录的sql语句</returns>
        public virtual string ToAddSql(params string[] fields)
        {
            return AddSql(false, fields);
        }
        /// <summary>
        /// 根据指定的属性数组转换为用于增加记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为"insert into {0}({1}) values({2})"
        /// 注意：该方法返回的sql语句中用到的表名为该实体的类名，用到的字段名为指定的该类的公共属性组合。
        /// </remarks>
        /// <param name="fields">需要插入的属性名数组（传入的字符串必须和属性名相同）</param>
        /// <returns>返回向数据表中插入一条记录的sql语句</returns>
        public virtual string ToAddBaseSql(params string[] fields)
        {
            return AddSql(true, fields);
        }
        /// <summary>
        /// 私有方法，用于获取增加用sql语句
        /// </summary>
        /// <param name="isBase"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        private string AddSql(bool isBase, params string[] fields)
        {
            Type type = isBase ? this.GetType().BaseType : this.GetType();
            string tableName = type.Name;
            PropertyInfo[] pros = type.GetProperties();
            if (pros == null || pros.Length == 0)
                return "";

            StringBuilder fieldSql = new StringBuilder();
            StringBuilder valueSql = new StringBuilder();
            if (fields == null)
                PropertiesToFieldAndValue(pros, fieldSql, valueSql, 3);
            else PropertiesToFieldAndValue(type, fieldSql, valueSql, fields);
            return string.Format("insert into {0}({1}) values({2})", tableName, fieldSql, valueSql);
        }
        /// <summary>
        /// 根据实体的属性数组填充字段字符串和值字符串，以便于组合增加操作的sql语句
        /// </summary>
        /// <param name="pros">实体的属性数组</param>
        /// <param name="fieldSql">字段字符串</param>
        /// <param name="valueSql">值字符串</param>
        /// <param name="fieldValueAll">1=只返回字段,2=只返回值,3=都返回</param>
        private void PropertiesToFieldAndValue(PropertyInfo[] pros, StringBuilder fieldSql, StringBuilder valueSql, int fieldValueAll)
        {
            //循环属性数组，将属性名称和值添加到指定的字符串 数组中。
            for (int i = 0; i < pros.Length; i++)
            {
                string field = "";
                string value = "";
                try
                {
                    field = pros[i].Name.ToLower();
                    value = ValueToString(pros[i].GetValue(this, null), pros[i].PropertyType);
                }
                catch
                {
                    //如果有属性的值提取异常（比如为空的情况），跳过
                    continue;
                }

                if (value == null)
                    continue;

                //如果是第一个属性，不需要添加逗号
                if (i == 0)
                {
                    fieldSql.Append(field);
                    valueSql.Append(value);
                }
                else
                {
                    fieldSql.Append("," + field);
                    valueSql.Append("," + value);
                }

                //if (i == pros.Length - 1)
                //{
                //    fieldSql.Append(field);
                //    valueSql.Append(value);
                //}
                //else
                //{
                //    fieldSql.Append(field + ",");
                //    valueSql.Append(value + ",");
                //}
            }
        }

        /// <summary>
        /// 根据实体的属性数组填充字段字符串和值字符串，以便于组合增加操作的sql语句
        /// </summary>
        /// <param name="type">实体类类型</param>
        /// <param name="fieldSql">字段字符串</param>
        /// <param name="valueSql">值字符串</param>
        /// <param name="fields">需要插入的属性名数组（传入的字符串必须和属性名相同）</param>
        private void PropertiesToFieldAndValue(Type type, StringBuilder fieldSql, StringBuilder valueSql, params string[] fields)
        {
            //循环属性数组，将属性名称和值添加到指定的字符串 数组中。
            for (int i = 0; i < fields.Length; i++)
            {
                string field = fields[i];

                //根据传入的属性名获取属性对象（反射）
                PropertyInfo pro = type.GetProperty(UpFirstChar(fields[i]));
                // 蒋属性的值转换为字符型
                string value = ValueToString(pro.GetValue(this, null), pro.GetType());
                if (value == null)
                    continue;


                //如果是第一个属性，不需要添加逗号
                if (i == 0)
                {
                    fieldSql.Append(field);
                    valueSql.Append(value);
                }
                else
                {
                    fieldSql.Append("," + field);
                    valueSql.Append("," + value);
                }
                ////如果是最后一个属性，不需要添加逗号
                //if (i == fields.Length - 1)
                //{
                //    fieldSql.Append(field);
                //    valueSql.Append(value);
                //}
                //else
                //{
                //    fieldSql.Append(field + ",");
                //    valueSql.Append(value + ",");
                //}
            }
        }
        #endregion

        #region 获得实体的删除语句
        /// <summary>
        /// 根据指定的属性名数组转换删除记录的sql语句
        /// </summary>
        /// <remarks>sql语句格式："delete {0} where {1}"</remarks>
        /// <param name="fieldNames">指定的属性名数组，作为删除记录的条件.条件中的逻辑运算为“and”。</param>
        /// <returns>返回一条删除部分记录的sql语句。如果参数传入null或者空数组，将返回 “”</returns>
        public virtual string ToDeleteSql(params string[] fieldNames)
        {
            return DeleteSql(false, fieldNames);
        }
        /// <summary>
        /// 根据指定的属性名数组转换删除记录的sql语句
        /// </summary>
        /// <remarks>sql语句格式："delete {0} where {1}"</remarks>
        /// <param name="fieldNames">指定的属性名数组，作为删除记录的条件.条件中的逻辑运算为“and”。</param>
        /// <returns>返回一条删除部分记录的sql语句。如果参数传入null或者空数组，将返回 “”</returns>
        public virtual string ToDeleteBaseSql(params string[] fieldNames)
        {
            return DeleteSql(true, fieldNames);
        }

        /// <summary>
        /// 转换为删除整个记录表的sql语句
        /// </summary>
        /// <remarks>sql语句格式："delete {0}"</remarks>
        /// <returns>返回一条删除整个表的sql语句</returns>
        public virtual string ToDeleteSql()
        {
            return DeleteSql(false, null);
        }
        /// <summary>
        /// 转换为删除整个记录表的sql语句
        /// </summary>
        /// <remarks>sql语句格式："delete {0}"</remarks>
        /// <returns>返回一条删除整个表的sql语句</returns>
        public virtual string ToDeleteBaseSql()
        {
            return DeleteSql(true, null);
        }
        /// <summary>
        /// 根据指定的属性名数组转换删除记录的sql语句
        /// </summary>
        /// <remarks>sql语句格式："delete {0} where {1}"</remarks>
        /// <param name="fieldNames">指定的属性名数组，作为删除记录的条件.条件中的逻辑运算为“and”。</param>
        /// <returns>返回一条删除部分记录的sql语句。如果参数传入null或者空数组，将返回 “”</returns>
        private string DeleteSql(bool isBase, params string[] fieldNames)
        {
            Type type = isBase ? this.GetType().BaseType : this.GetType();
            string tableName = type.Name;
            string sql = "";

            if (fieldNames == null)
                sql = string.Format("delete {0}", tableName);
            else
            {
                if (fieldNames == null || fieldNames.Length == 0)
                    return sql;
                StringBuilder whereString = FieldsToString(type, true, true, fieldNames);
                if (whereString == null)
                    return sql;

                sql = string.Format("delete {0} where {1}", tableName, whereString);
            }
            return sql;
        }

        #endregion

        #region  获得实体的修改语句
        /// <summary>
        /// 根据指定的属性名数组转换为修改记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为："update {0} set {1} where {2}"
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <param name="setFieldNames">作为修改项的属性名数组，如果传入null或者空数组，返回sql语句为""</param>
        /// <param name="whereFieldNames">
        /// 作为条件的属性名数组，如果传入null或者空数组，返回sql语句为""。条件中的逻辑运算为“and”。
        /// </param>
        /// <returns>返回一条根据指定条件的修改记录的sql语句</returns>
        public virtual string ToUpdateSql(string[] setFieldNames, params string[] whereFieldNames)
        {
            return UpdateSql(false, setFieldNames, whereFieldNames);
        }

        /// <summary>
        /// 根据指定的属性数组转换为修改所有字段的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为："update {0} set {1} where {2}"
        /// 修改语句中的修改项会自动除去作为条件的属性。
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <param name="whereFieldNames">作为条件的属性名数组。如果传入null或者空数组，返回sql语句为""。</param>
        /// <returns>返回一条修改所有字段的sql语句</returns>
        public virtual string ToUpdateSql(params string[] whereFieldNames)
        {
            return UpdateSql(false, null, whereFieldNames);
        }
        /// <summary>
        /// 根据指定的属性数组转换修改整表记录的sql语句 
        /// </summary>
        /// <remarks>
        /// sql语句格式为："update {0} set {1}"
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <param name="setFieldNames">属性名数组，如果传入null或者空数组，返回sql语句为""。</param>
        /// <returns>修改整表记录的sql语句</returns>
        public virtual string ToUpdateTableSql(params string[] setFieldNames)
        {
            return UpdateSql(false, setFieldNames, null);
        }


        /// <summary>
        /// 根据指定的属性名数组转换为修改记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为："update {0} set {1} where {2}"
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <param name="setFieldNames">作为修改项的属性名数组，如果传入null或者空数组，返回sql语句为""</param>
        /// <param name="whereFieldNames">
        /// 作为条件的属性名数组，如果传入null或者空数组，返回sql语句为""。条件中的逻辑运算为“and”。
        /// </param>
        /// <returns>返回一条根据指定条件的修改记录的sql语句</returns>
        public virtual string ToUpdateBaseSql(string[] setFieldNames, params string[] whereFieldNames)
        {
            return UpdateSql(true, setFieldNames, whereFieldNames);
        }

        /// <summary>
        /// 根据指定的属性数组转换为修改所有字段的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为："update {0} set {1} where {2}"
        /// 修改语句中的修改项会自动除去作为条件的属性。
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <param name="whereFieldNames">作为条件的属性名数组。如果传入null或者空数组，返回sql语句为""。</param>
        /// <returns>返回一条修改所有字段的sql语句</returns>
        public virtual string ToUpdateBaseSql(params string[] whereFieldNames)
        {
            return UpdateSql(true, null, whereFieldNames);
        }
        /// <summary>
        /// 根据指定的属性数组转换修改整表记录的sql语句 
        /// </summary>
        /// <remarks>
        /// sql语句格式为："update {0} set {1}"
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <param name="setFieldNames">属性名数组，如果传入null或者空数组，返回sql语句为""。</param>
        /// <returns>修改整表记录的sql语句</returns>
        public virtual string ToUpdateBaseTableSql(params string[] setFieldNames)
        {
            return UpdateSql(true, setFieldNames, null);
        }
        /// <summary>
        /// 根据指定的属性名数组转换为修改记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为："update {0} set {1} where {2}"
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <param name="setFieldNames">作为修改项的属性名数组，如果传入null或者空数组，返回sql语句为""</param>
        /// <param name="whereFieldNames">
        /// 作为条件的属性名数组，如果传入null或者空数组，返回sql语句为""。条件中的逻辑运算为“and”。
        /// </param>
        /// <returns>返回一条根据指定条件的修改记录的sql语句</returns>
        private string UpdateSql(bool isBase, string[] setFieldNames, params string[] whereFieldNames)
        {
            Type type = isBase ? this.GetType().BaseType : this.GetType();
            string tableName = type.Name;
            string sql = "";
            PropertyInfo[] pros = type.GetProperties();

            if (setFieldNames == null)
            {
                if (whereFieldNames == null || whereFieldNames.Length == 0)
                    return sql;

                if (pros.Length == 0)
                    return sql;

                StringBuilder whereString = FieldsToString(type, true, true, whereFieldNames);
                if (whereString == null)
                    return sql;

                StringBuilder setString = PropertiesToSetString(type, pros, whereFieldNames);
                if (setString == null)
                    return sql;

                sql = string.Format("update {0} set {1} where {2}", tableName, setString, whereString);
            }
            else if (whereFieldNames == null)
            {
                if (setFieldNames == null || setFieldNames.Length == 0)
                    return sql;

                StringBuilder setString = FieldsToString(type, false, true, setFieldNames);
                if (setString == null)
                    return sql;

                sql = string.Format("update {0} set {1}", tableName, setString);
            }
            else
            {
                if (setFieldNames == null || whereFieldNames == null || setFieldNames.Length == 0 || whereFieldNames.Length == 0)
                    return sql;

                if (pros.Length == 0)
                    return sql;

                StringBuilder whereString = FieldsToString(type, true, true, whereFieldNames);
                if (whereString == null)
                    return sql;

                StringBuilder setString = FieldsToString(type, false, true, setFieldNames);
                if (setString == null)
                    return sql;

                sql = string.Format("update {0} set {1} where {2}", tableName, setString, whereString);
            }

            return sql;
        }
        /// <summary>
        /// 根据实体的所有公开属性转换为修改语句中的设置表达式
        /// </summary>
        /// <param name="type">实体类类型</param>
        /// <param name="pros">实体类属性数组</param>
        /// <param name="wheres">作为条件的属性名数组（作为设置语句的排除项）</param>
        /// <returns>修改语句的设置表达式</returns>
        private StringBuilder PropertiesToSetString(Type type, PropertyInfo[] pros, params string[] wheres)
        {
            List<string> whereList = new List<string>();
            whereList.AddRange(wheres);
            List<string> setList = new List<string>();
            for (int i = 0; i < pros.Length; i++)
            {
                if (!whereList.Contains(pros[i].Name))
                    setList.Add(pros[i].Name);
            }

            return FieldsToString(type, false, true, setList);
        }

        #endregion

        #region 获得实体的查询语句
        /// <summary>
        /// 获取查询语句
        /// 实体类对应的表中包含所有字段的所有记录的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为："select * from {0}"
        /// sql语句操作的表名为实体类的名称
        /// </remarks>
        /// <returns>sql语句字符串</returns>
        public virtual string ToSelectSql()
        {
            Type type = this.GetType();
            string tableName = type.Name;

            return string.Format("select * from {0}", tableName);
        }

        /// <summary>
        /// 获取查询语句
        /// 实体类对应的表中包含指定字段的所有记录的sql语句
        /// </summary>
        /// <param name="fieldNames">作为字段的属性名称数组</param>
        /// <returns>sql语句字符串</returns>
        public virtual string ToSelectSql(params string[] fieldNames)
        {
            Type type = this.GetType();
            string tableName = type.Name;

            string sql = "";
            if (fieldNames == null || fieldNames.Length == 0)
                return sql;

            string fieldsString = string.Join(", ", fieldNames);
            if (fieldsString == null)
                return sql;

            return string.Format("select {0} from {1}", fieldsString, tableName);
        }

        /// <summary>
        /// 获取查询语句(Top)
        /// </summary>
        /// <param name="topNum">提取行数</param>
        /// <param name="fieldNames">作为字段的属性名称数组</param>
        /// <param name="isAnd">条件中是否采用与逻辑（and）：是=and，否=or</param>
        /// <param name="wheres">作为条件的属性名称数组</param>
        /// <returns>sql语句字符串</returns>
        public virtual string ToSelectSql(int topNum, string[] fieldNames, bool isAnd, params string[] wheres)
        {
            Type type = this.GetType();
            string tableName = type.Name;

            string sql = "";
            bool isField, isWheres;
            isField = (fieldNames == null || fieldNames.Length == 0);
            isWheres = (wheres == null || wheres.Length == 0);

            string fieldsString = null;
            StringBuilder whereString = null;
            if (!isField)
            {
                fieldsString = string.Join(", ", fieldNames); ;
                if (fieldsString == null)
                    return sql;
            }
            if (!isWheres)
            {
                whereString = FieldsToString(type, true, isAnd, wheres);
                if (whereString == null)
                    return sql;
            }

            if (isField && isWheres)
                return string.Format("select top {0} * from {1}", topNum.ToString(), tableName);
            else if (isField && !isWheres)
                return string.Format("select top {0} * from {1} where {2}", topNum.ToString(), tableName, whereString);
            else if (!isField && isWheres)
                return string.Format("select top {0} {1} from {2}", topNum, fieldsString, tableName);
            else
                return string.Format("select top {0} {1} from {2} where {3}", topNum, fieldsString, tableName, whereString);

        }


        /// <summary>
        /// 获取查询语句(Top)
        /// </summary>
        /// <param name="topNum">提取行数</param>
        /// <param name="fieldNames">作为字段的属性名称数组</param>
        /// <param name="isAnd">条件中是否采用与逻辑（and）：是=and，否=or</param>
        /// <param name="wheres">作为条件的属性名称数组</param>
        /// <returns>sql语句字符串</returns>
        public virtual string ToSelectDistinctSql(bool isAnd, string[] fieldNames, params string[] wheres)
        {
            Type type = this.GetType();
            string tableName = type.Name;

            string sql = "";
            bool isField, isWheres;
            isField = (fieldNames == null || fieldNames.Length == 0);
            isWheres = (wheres == null || wheres.Length == 0);

            string fieldsString = null;
            StringBuilder whereString = null;
            if (!isField)
            {
                fieldsString = string.Join(", ", fieldNames); ;
                if (fieldsString == null)
                    return sql;
            }
            if (!isWheres)
            {
                whereString = FieldsToString(type, true, isAnd, wheres);
                if (whereString == null)
                    return sql;
            }

            if (isField && isWheres)
                return string.Format("select Distinct * from {0}", tableName);
            else if (isField && !isWheres)
                return string.Format("select Distinct * from {0} where {1}", tableName, whereString);
            else if (!isField && isWheres)
                return string.Format("select Distinct {0} from {1}", fieldsString, tableName);
            else
                return string.Format("select Distinct {0} from {1} where {2}", fieldsString, tableName, whereString);

        }

        /// <summary>
        /// 获取查询语句
        /// 根据指定查询条件获取所有字段的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式为："select * from {0} where {1}"
        /// sql语句操作的表名为实体类的名称 
        /// </remarks>
        /// <param name="isAnd">条件中是否采用与逻辑（and）：是=and，否=or</param>
        /// <param name="wheres">作为条件的属性名称数组</param>
        /// <returns>带条件查询的sql语句</returns>
        public virtual string ToSelectSql(bool isAnd, params string[] wheres)
        {
            Type type = this.GetType();
            string tableName = type.Name.ToLower();

            string sql = "";
            if (wheres == null || wheres.Length == 0)
                return sql;

            StringBuilder wheresString = FieldsToString(type, true, isAnd, wheres);
            sql = string.Format("select * from {0} where {1}", tableName, wheresString);
            return sql;
        }

        /// <summary>
        /// 获取查询语句
        /// 根据指定条件查询指定字段的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式："select {0} from {1} where {2}"
        /// </remarks>
        /// <param name="isAnd">条件中是否采用与逻辑（and）：是=and，否=or</param>
        /// <param name="fieldNames">作为字段的属性名称数组</param>
        /// <param name="wheres">作为条件的属性名称数组</param>
        /// <returns>查询sql语句</returns>
        public virtual string ToSelectSql(bool isAnd, string[] fieldNames, params string[] wheres)
        {
            Type type = this.GetType();
            string tableName = type.Name.ToLower();

            string sql = "";
            if (fieldNames == null || fieldNames.Length == 0 || wheres == null || wheres.Length == 0)
                return sql;

            string fieldsString = string.Join(", ", fieldNames);

            StringBuilder whereString = FieldsToString(type, true, isAnd, wheres);
            if (whereString == null)
                return sql;
            sql = string.Format("select {0} from {1} where {2}", fieldsString, tableName, whereString);
            return sql;
        }
        /// <summary>
        /// 获取查询语句
        /// 根据指定条件查询指定字段的sql语句
        /// </summary>
        /// <remarks>
        /// sql语句格式："select Max({0}) from {1} where {2}"
        /// </remarks>
        /// <param name="isAnd">条件中是否采用与逻辑（and）：是=and，否=or</param>
        /// <param name="fieldNames">作为字段的属性名称数组</param>
        /// <param name="wheres">作为条件的属性名称数组</param>
        /// <returns>查询sql语句</returns>
        public virtual string ToSelectMaxSql(bool isAnd, string fieldName, params string[] wheres)
        {
            Type type = this.GetType();
            string tableName = type.Name.ToLower();

            string sql = "";
            if (fieldName == "" || wheres == null)
                return sql;

            string fieldsString = "max(" + fieldName + ")";
            if (wheres.Length == 0)
                return string.Format("select {0} from {1}", fieldsString, tableName);
            StringBuilder whereString = FieldsToString(type, true, isAnd, wheres);
            if (whereString == null)
                return sql;
            sql = string.Format("select {0} from {1} where {2}", fieldsString, tableName, whereString);
            return sql;
        }

        /// <summary>
        /// 获取查询语句
        /// 根据指定条件查询影响行数
        /// </summary>
        /// <remarks>
        /// sql语句格式："select Count(1) from {1} where {2}"
        /// </remarks>
        /// <param name="isAnd">条件中是否采用与逻辑（and）：是=and，否=or</param>
        /// <param name="fieldNames">作为字段的属性名称数组</param>
        /// <param name="wheres">作为条件的属性名称数组</param>
        /// <returns>查询sql语句</returns>
        public virtual string ToSelectCountSql(bool isAnd, params string[] wheres)
        {
            Type type = this.GetType();
            string tableName = type.Name.ToLower();

            string sql = "";
            if (wheres == null)
                return sql;

            string fieldsString = "count(1)";
            if (wheres.Length == 0)
                return string.Format("select {0} from {1}", fieldsString, tableName);
            StringBuilder whereString = FieldsToString(type, true, isAnd, wheres);
            if (whereString == null)
                return sql;
            sql = string.Format("select {0} from {1} where {2}", fieldsString, tableName, whereString);
            return sql;
        }

        #endregion

        #region 公用方法
        /// <summary>
        /// 将属性的值转换为sql字符串类型
        /// </summary>
        /// <param name="value">要转换的值</param>
        /// <param name="vType">值的类型</param>
        /// <returns>sql语句中的字符串类型</returns>
        /// <remarks>支持类型：<c>int</c>,<c>decimal</c>,<c>double</c>,<c>float</c>,<c>bool</c>,<c>string</c>,</remarks>
        private string ValueToString(Object value, Type vType)
        {
            if (vType == typeof(int) || vType == typeof(decimal) || vType == typeof(double) || vType == typeof(float))
                return value.ToString();//数字类型直接转换为字符串
            else if (vType == typeof(bool))
                return Convert.ToInt32(value).ToString();//bool类型先转换为整形(1或0)，再转换为字符串
            else if (vType == typeof(DateTime))
            {
                DateTime dt = Convert.ToDateTime(value);
                if (dt.Year == 1)
                    return null;
                else
                    return "'" + dt.ToString() + "'";
            }
            else if (vType == typeof(string) && value == null)
                return "''";
            else
                return "'" + value.ToString() + "'";//其他类型在转换为字符串后两端加单引号
        }

        /// <summary>
        /// 根据提供的属性名，获取删除、修改、查询语句中的条件语句
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="fieldNames">作为条件的属性名称（注意区分大小写）</param>
        /// <returns>StringBuilder的字符串</returns>
        private StringBuilder FieldsToString(Type type, bool isWhere, bool isAnd, params string[] fieldNames)
        {
            StringBuilder fields = new StringBuilder();
            string logicStr = isAnd ? " and " : " or ";
            try
            {
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    PropertyInfo pro = type.GetProperty(UpFirstChar(fieldNames[i]));
                    string value = ValueToString(pro.GetValue(this, null), pro.PropertyType);
                    if (value == null)
                        continue;
                    if (i == fieldNames.Length - 1)
                    {
                        fields.Append(pro.Name.ToLower() + "=" + value);
                    }
                    else
                    {
                        if (isWhere)
                            fields.Append(pro.Name.ToLower() + "=" + value + logicStr);
                        else
                            fields.Append(pro.Name.ToLower() + "=" + value + ", ");
                    }
                }
            }
            catch
            {
                return null;
            }

            return fields;
        }

        /// <summary>
        /// 根据提供的属性名，获取删除、修改、查询语句中的条件语句
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="fieldNames">作为条件的属性名称集合（注意区分大小写）</param>
        /// <returns>StringBuilder的字符串</returns>
        private StringBuilder FieldsToString(Type type, bool isWhere, bool isAnd, List<string> fieldNames)
        {
            StringBuilder fields = new StringBuilder();
            string logicStr = isAnd ? ", and " : ", or ";
            try
            {
                for (int i = 0; i < fieldNames.Count; i++)
                {
                    PropertyInfo pro = type.GetProperty(UpFirstChar(fieldNames[i]));
                    string value;
                    try
                    {
                        value = ValueToString(pro.GetValue(this, null), pro.PropertyType);
                    }
                    catch (Exception)
                    {
                        continue;
                    }

                    if (value == null)
                        continue;

                    if (i == fieldNames.Count - 1)
                    {
                        fields.Append(pro.Name.ToLower() + "=" + value);
                    }
                    else
                    {
                        if (isWhere)
                            fields.Append(pro.Name.ToLower() + "=" + value + logicStr);
                        else
                            fields.Append(pro.Name.ToLower() + "=" + value + ", ");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                Console.ReadLine();
                return null;
            }

            return fields;
        }

        private static string UpFirstChar(string str)
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(str.Substring(0, 1).ToUpper());
            strb.Append(str.Substring(1, str.Length - 1));
            return strb.ToString();
        }
        #endregion

        #region 【转化父类公共属性到子类】
        /// <summary>
        /// 将父类所有公共属性转换到子类
        /// </summary>
        /// <typeparam name="B"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="baseModle"></param>
        /// <param name="childModle"></param>
        /// <returns>是否转换成功</returns>
        public bool BaseToChild<B, C>(B baseModle, C childModle)
        {
            Type baseType = baseModle.GetType();
            Type childType = childModle.GetType();
            if (baseType.FullName != childType.BaseType.FullName)
                return false;
            if (!baseType.IsClass || !childType.IsClass)
                return false;

            PropertyInfo[] files = baseType.GetProperties();
            foreach (PropertyInfo one in files)
            {
                PropertyInfo file = childType.GetProperty(one.Name);
                file.SetValue(childModle, one.GetValue(baseModle, null), null);
            }
            return true;
        }
        #endregion
    }
}
