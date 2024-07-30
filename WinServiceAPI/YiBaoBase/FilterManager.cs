using System.Reflection;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data.Filtering;

namespace YiBaoBase
{
    /// <summary>
    /// 过滤类型枚举
    /// </summary>
    public enum FilterStrType
    {
        /// <summary>
        /// 无模糊
        /// </summary>
        None,
        /// <summary>
        /// 左模糊
        /// </summary>
        OnlyLeft,
        /// <summary>
        /// 右模糊
        /// </summary>
        OnlyRight,
        /// <summary>
        /// 前后模糊
        /// </summary>
        Both
    }

    /// <summary>
    /// 过滤字段信息类
    /// </summary>
    public class FilterFieldInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="filterStrType">过滤类型</param>
        public FilterFieldInfo(string name,FilterStrType filterStrType)
        {
            this._name = name;
            this._filterStrType = filterStrType;
        }

        private string _name;

        public string Name
        {
          get { return _name; }
          set { _name = value; }
        }

        private FilterStrType _filterStrType;

        public FilterStrType FilterStrType
        {
          get { return _filterStrType; }
          set { _filterStrType = value; }
        }
    }

    /// <summary>
    /// 过滤辅助类
    /// </summary>
    public static class FilterManager
    {
        /// <summary>
        /// 根据过滤条件字符串过滤
        /// </summary>
        /// <param name="edit">GridLookUpEditBase：过滤控件</param>
        /// <param name="filterCondition">过滤条件字符串</param>
        public static void FilterGridLookup(GridLookUpEditBase edit, string filterCondition)
        {
            GridView gridView = edit.Properties.View;
            FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(gridView, filterCondition);

            MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }

        public static void FilterGridLookup(GridLookUpEditBase edit, params FilterFieldInfo[] filterFields)
        {
            GridView gridView = edit.Properties.View;
            FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);

            string filterCondition = CreateFilterCondition(edit.AutoSearchText, filterFields);
            fi.SetValue(gridView, filterCondition);

            MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }

        public static void FilterGridLookup(GridLookUpEditBase edit, params string[] filterFieldNames)
        {
            GridView gridView = edit.Properties.View;
            FieldInfo fi = gridView.GetType().GetField("extraFilter", BindingFlags.NonPublic | BindingFlags.Instance);

            string filterCondition = CreateFilterCondition(edit.AutoSearchText, filterFieldNames);
            fi.SetValue(gridView, filterCondition);

            MethodInfo mi = gridView.GetType().GetMethod("ApplyColumnsFilterEx", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(gridView, null);
        }

        public static string CreateFilterCondition(string searchText, params FilterFieldInfo[] filterFields)
        {
            int count = filterFields.Length;
            string searchStr = searchText;
            CriteriaOperator[] operands = new CriteriaOperator[count];
            for (int i = 0; i < count; i++)
            {
                searchText = GetFilterSearchText(searchStr, filterFields[i].FilterStrType);
                BinaryOperator binaryOperator = new BinaryOperator(filterFields[i].Name, searchText, BinaryOperatorType.Like);
                operands[i] = binaryOperator;
            }

            return new GroupOperator(GroupOperatorType.Or, operands).ToString();
        }

        public static string CreateFilterCondition(string searchText, params string[] filterFieldNames)
        {
            int count = filterFieldNames.Length;
            //searchText = "%" + searchText + "%";
             searchText = searchText + "%";
            CriteriaOperator[] operands = new CriteriaOperator[count];
            for (int i = 0; i < count; i++)
            {
                BinaryOperator binaryOperator = new BinaryOperator(filterFieldNames[i], searchText, BinaryOperatorType.Like);
                operands[i] = binaryOperator;
            }

            return new GroupOperator(GroupOperatorType.Or, operands).ToString();
        }

        public static string GetFilterSearchText(string searchText, FilterStrType filterStrType)
        {
            switch (filterStrType)
            { 
                case FilterStrType.None:
                    return searchText;
                case FilterStrType.OnlyLeft:
                    return "%" + searchText;
                case FilterStrType.OnlyRight:
                    return searchText + "%";
                default:
                    return  "%"+ searchText + "%";
            }
        }
        
    }
}
