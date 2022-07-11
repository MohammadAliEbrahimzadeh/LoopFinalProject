using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopMainProject.Common.ViewModels
{
    public class FilterPostViewModel
    {
        public PostOrder PostOrder { get; set; }
    }

    public enum PostOrder
    {
        All,
        Newest,
        ThisMonth,
        MostCommentCount
    }
}
