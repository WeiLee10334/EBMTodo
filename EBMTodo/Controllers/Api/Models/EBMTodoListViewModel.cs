﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBMTodo.Controllers.Api.Models
{
    public class EBMTodoListViewModel
    {

        public string PTLID { set; get; }

        public DateTime ApplyDateTime { set; get; }

        public string ApplyName { set; get; }

        public string title { set; get; }

        public string Description { set; get; }

        public int CompleteRate { set; get; }

        public string PMID { set; get; }

        public string MemberTitle { set; get; }

        public string Tag { set; get; }

        public string Memo { set; get; }
    }
}