using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Areas.Back.Models
{
    public class PagingViewModel<T>
    {
        public int Skip { set; get; }

        public int Length { set; get; }

        public int Total { set; get; }

        public List<T> Data { set; get; }
    }
}