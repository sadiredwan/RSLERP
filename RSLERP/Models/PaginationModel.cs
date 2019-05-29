using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RSLERP.Models
{
    public class PaginationModel
    {
        private int _pageIndex;
        public int PageIndex
        {
            get { return (_pageIndex == 0 ? 1 : _pageIndex); }
            set { _pageIndex = value; }
        }

        private int _pageSize;
        public int PageSize
        {
            get { return (_pageSize == 0 ? Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]) : _pageSize); }
            set { _pageSize = value; }
        }

        public int TotalRow { get; set; }
    }
}