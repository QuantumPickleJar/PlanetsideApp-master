using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsApp.Pages
{

    public class SearchPageMenuItem
    {
        public SearchPageMenuItem()
        {
            TargetType = typeof(SearchPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}