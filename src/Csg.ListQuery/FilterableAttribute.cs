﻿namespace Csg.ListQuery
{
    public class FilterableAttribute : System.Attribute
    {
        public FilterableAttribute(bool filterable = true)
        {
            this.IsFilterable = filterable;
        }

        public bool IsFilterable { get; private set; }

    }
}